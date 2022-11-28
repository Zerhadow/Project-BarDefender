using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : Units
{
    private Rigidbody2D rb;

    #region Stats & Cooldowns
    public int ATK;
    public float _moveSpeed = 5f;
    public float _jumpPower = 10f;
    public float fireCooldown = 0.3f;
    public float _jumpCooldown = 0.3f;
    public float atkRange = 0.5f;
    public int _maxJumps = 2;
    public float _rebound = 2; //how much you bounce enemies
    public int evasion = 0;
    #endregion

    #region Variables: Attack
    private const float _COMBO_MIN_DELAY = 0.1f;
    private int _COMBO_MAX_STEP = 2;
    private int _comboHitStep;
    private Coroutine _comboAttackResetCoroutine;
    #endregion

    #region Variables: Animation
    private int _animRunningParamHash;
    private int _animAttackComboStepParamHash;
    #endregion

    bool canFire = true, canJump = true, inFirstAttack = false;
    public PlayerInputActions playerControls;
    [SerializeField] private int _currentJumps = 0;
    [SerializeField] UnityEngine.Animator _playerAnimator;
    [SerializeField] SpriteRenderer _playerSprite;
    [SerializeField] Transform _footPos;
    [SerializeField] public PauseMenuSM _pauseMenuSM;
    public LayerMask _groundLayer;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = new Vector2(1,0);


    #region #Input Actions
    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction melee;
    private InputAction pause;
    private InputAction flex;
    #endregion

    public GameObject projectilePrefab;
    public Transform atkPt;
    public LayerMask enemyLayers; 

    public bool isGrounded = false;
    private bool _paused = false;
    private bool isFlexing = false;
    private bool isAttacking = false;
    private bool canMove = true;
    bool enemyPoisoned = false;
    bool evaded = false;
    bool burnout = false;

    Potion potion;
    Scene sceneLoaded;


    void Awake() {
        
        playerControls = new PlayerInputActions();
        dmg = ATK;

        _animAttackComboStepParamHash = Animator.StringToHash("AttackComboStep");
        _comboHitStep = -1;
        _comboAttackResetCoroutine = null;

        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {

         if(sceneLoaded.Equals("BarScene")){
            maxHP = maxHP + potion.maxHP;
            ATK = ATK + potion.ATK;
            _moveSpeed = _moveSpeed + potion._moveSpeed;
            _jumpPower = _jumpPower + potion._jumpPower;
            fireCooldown = fireCooldown + potion.fireCooldown;
            _jumpCooldown = _jumpCooldown + potion._jumpCooldown;
            atkRange = atkRange + potion.atkRange;
            _maxJumps = _maxJumps + potion._maxJumps;
            _rebound = _rebound +potion._rebound; //how much you bounce enemies
            evasion = potion.evasion;
        }

        currHP = maxHP;
        // Debug.Log("currHP: " + currHP);        
        HPBar.SetMaxHealth(maxHP);
        //for now only the player has a healthbar so only he will call the set health function
        sceneLoaded=SceneManager.GetActiveScene();
       
    }

    private void OnEnable() {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

        melee = playerControls.Player.Melee;
        melee.Enable();
        melee.performed += Melee;

        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += Pause;

        flex = playerControls.Player.Flex;
        flex.Enable();
        flex.performed += Flex;

    }

    private void OnDisable() {
        move.Disable();
        fire.Disable();
        jump.Disable();
        melee.Disable();
        pause.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_paused)
        {
            moveDirection = move.ReadValue<Vector2>();

            _playerAnimator.SetFloat("xVelocity", moveDirection.x, 0.1f, 0.1f);
            _playerAnimator.SetFloat("yVelocity", rb.velocity.y, 0.1f, 0.1f);
            _playerAnimator.SetFloat("xLookDirection", lookDirection.x);
            _playerAnimator.SetFloat("yLookDirection", lookDirection.y);
            _playerAnimator.SetFloat("Health", currHP, 0.1f, 0.1f);
            _playerAnimator.SetBool("isGrounded", isGrounded);
            _playerAnimator.SetBool("Jump", !isGrounded);
            _playerAnimator.SetBool("isGrounded", isGrounded);

            if (!Mathf.Approximately(moveDirection.x, 0.0f) || !Mathf.Approximately(moveDirection.y, 0.0f))
            {
                lookDirection.Set(moveDirection.x, moveDirection.y);
                lookDirection.Normalize();
            }

            if (!isGrounded)
            {
                _playerAnimator.SetBool("Jump", true);
            }
            if (lookDirection.x < 0)
            {
                _playerSprite.flipX = true;
            }

            else if (lookDirection.x > 0)
            {
                _playerSprite.flipX = false;
            }
        }

        if(burnout) {
            currHP -= 10;
        }
    }

    private void FixedUpdate() {

        //if (moveDirection.y * _jumpPower < 0) {
        //    rb.velocity = new Vector2(moveDirection.x * moveSpd, 0);
        //}
        if (!_paused)
        {
            Move();
        }

        atkPt.position = this.transform.position + new Vector3(lookDirection.x * (atkRange+0.5f), lookDirection.y * (atkRange + 1), 0);
        Move();
        atkPt.position = this.transform.position + new Vector3(lookDirection.x * ((atkRange/2)+0.5f), lookDirection.y * ((atkRange/2) + 0.6f), 0);
        isGrounded = Physics2D.OverlapCircle(_footPos.position, 1f, _groundLayer);


    }

    private void Fire(InputAction.CallbackContext context) {
        Debug.Log("We Fired");

        if(canFire) {
            _playerAnimator.SetTrigger("Shoot");
            StartCoroutine(fireTimer(fireCooldown));
            GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);
            // animator.SetTrigger("Launch");
            // PlaySound(throwSound);
        }
    }   

    private void Jump(InputAction.CallbackContext context)
    {
    
        if (canJump && _currentJumps < _maxJumps)
        {
            _currentJumps++;
            StartCoroutine(_jumpTimer(_jumpCooldown));
            rb.AddForce(new Vector2(0f, _jumpPower), ForceMode2D.Impulse);
            isGrounded = false;
            canMove = true;
            //rb.AddForce(new Vector2(_jumpPower * moveDirection.x, 0), ForceMode2D.Impulse); //long jump?
        }
    }

    private void Melee(InputAction.CallbackContext context) {
        //Play an atk animation


        if (_comboHitStep == _COMBO_MAX_STEP)
            return;
        float t = _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (_comboHitStep == -1 || (t >= 0.1f && t <= 0.8f))
        {
            if (_comboAttackResetCoroutine != null)
                StopCoroutine(_comboAttackResetCoroutine);
            _comboHitStep++;
            _playerAnimator.SetTrigger("Attack");

            if (isGrounded)
                canMove = false;
            //isAttacking = true;

            //Detect enemies in range of atk
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atkPt.position, atkRange, enemyLayers);
            //dmg them
            foreach (Collider2D enemy in hitEnemies)
            {
                // Debug.Log("We hit " + enemy.name);
                Units enemyStat = enemy.gameObject.GetComponent<Units>();
                enemyStat.TakeDmg(ATK);
                rb.AddForce(new Vector2(-lookDirection.x - (moveDirection.x / 2) * _rebound, -lookDirection.x - moveDirection.y * _rebound), ForceMode2D.Impulse);
                Debug.Log("Enemy HP: " + enemyStat.currHP);
            }

            _playerAnimator.SetBool(_animRunningParamHash, false);
            _playerAnimator.SetInteger(
                _animAttackComboStepParamHash, _comboHitStep);
            _comboAttackResetCoroutine = StartCoroutine(_ResettingAttackCombo());
        }
    }

    private IEnumerator _ResettingAttackCombo()
    {
        _playerAnimator.SetFloat("yLookDirection", 0f);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(
            _playerAnimator.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
            _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        _comboHitStep = -1;
        isAttacking = false;
        canMove = true;
        _playerAnimator.SetInteger(
            _animAttackComboStepParamHash, _comboHitStep);

    }


    private void Move()
    {
        //if (!isFlexing && !isAttacking)
        //if (!isFlexing && !isAttacking)
        if (canMove)
        {
            transform.position += transform.right * moveDirection.x * _moveSpeed * Time.deltaTime;
            rb.AddForce(new Vector2(lookDirection.x - moveDirection.x, 0), ForceMode2D.Force);
        }


    }

    IEnumerator fireTimer(float timer){
        canFire = false;
        while(timer > 0){
            yield return null;
            timer -= Time.deltaTime;
        }
        canFire = true;
    }

    IEnumerator _jumpTimer(float timer){
        canJump = false;
        while(timer > 0){
            yield return null;
            timer -= Time.deltaTime;
        }
        canJump = true;
    }

    private void OnDrawGizmosSelected() {
        if(atkPt == null) {return; }

        Gizmos.DrawWireSphere(atkPt.position, atkRange);
    }

    #region Function Modifers

    public void IncreaseATK_DecreaseHP(string rarity) { 
        ATK += 1;
        maxHP -= 20;
        // currHP += 20 maybe won't be needed
        Debug.Log("New ATK: " + ATK);
        Debug.Log("New Max HP: " + maxHP); 
    }

    public void IncreaseHP_DecreaseATK(string rarity) { 
        ATK -= 1;
        maxHP += 20;
        // currHP += 20 maybe won't be needed
        Debug.Log("New ATK: " + ATK);
        Debug.Log("New Max HP: " + maxHP); 
    }

    public void IncreaseRunSpd_DecreaseJumpPwr(string rarity) { 
        _moveSpeed += 1f;
        _jumpPower -= 2f;

        Debug.Log("New Move Speed: " + _moveSpeed);
        Debug.Log("New Jump Power: " + _jumpPower); 
    }

    public void IncreaseJumpPwr_DecreaseRunSpd(string rarity) { 
        _moveSpeed -= 1f;
        _jumpPower += 2f;

        Debug.Log("New Move Speed: " + _moveSpeed);
        Debug.Log("New Jump Power: " + _jumpPower); 
    }

    public void IncreaseFireCooldown_DecreaseMeleeCooldown(string rarity) { 
        fireCooldown -= 0.1f;
        // melee atk? change the animator speeds

        Debug.Log("New Fire Cooldown: " + fireCooldown);
        // Debug.Log("New Jump Power: " + _jumpPower); 
    }

    public void IncreaseMeleeCooldown_DecreaseFireCooldown(string rarity) { 
        fireCooldown += 0.1f;
        // melee atk? change the animator speeds

        Debug.Log("New Fire Cooldown: " + fireCooldown);
        // Debug.Log("New Jump Power: " + _jumpPower); 
    }

    public void IncreaseSize_DecreaseRunSpd(string rarity) { // Growth Serum
        Vector3 scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
        transform.localScale += scaleChange;
        _moveSpeed -= 1f;

        Debug.Log("New Player Scale: " + transform.localScale); 
        Debug.Log("New Move Speed: " + _moveSpeed);
    }

    public void IncreaseRunSpd_DecreaseSize(string rarity) { // Shrink Serum
        Vector3 scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
        transform.localScale -= scaleChange;
        _moveSpeed += 1f;

        Debug.Log("New Player Scale: " + transform.localScale); 
        Debug.Log("New Move Speed: " + _moveSpeed);
    }

    public void PoisonTipped_LifeDrain(string rarity) { // for poison lotus
        // amp dmg on melee
        // loss life slowly; use boolean to turn on

        // Debug.Log("New Player Scale: " + _jumpPower); 
        // Debug.Log("New Move Speed: " + _moveSpeed);
    }

    public void IncreaseEnemySpd_SlowlyLossSight(string rarity) { //for Sharingan eye
        evasion += 10;
        ATK -= 1;
        currHP -= 10;

        Debug.Log("New Evasion: " + evasion + "%");
        Debug.Log("New ATK: " + ATK);
        Debug.Log("New Max HP: " + maxHP); 
    }

    public void SuperSaiyan_Burnout(string rarity) { // Saiyan Blood
        //get player scale/transform
        // _moveSpeed += 1f;

        evasion += 20;
        ATK *= 2;
        maxHP *= 10;
        _moveSpeed += 1f;
        _jumpPower += 2f;

        //slowly loss hp
        burnout = true;

        Debug.Log("New Evasion: " + evasion + "%");
        Debug.Log("New ATK: " + ATK);
        Debug.Log("New Max HP: " + maxHP);
        Debug.Log("New Move Speed: " + _moveSpeed);
        Debug.Log("New Jump Power: " + _jumpPower); 
    }

    public void Cripple(string rarity) { // Krillin
        //get player scale/transform
        // _moveSpeed += 1f;

        // Debug.Log("New Player Scale: " + _jumpPower); 
        // Debug.Log("New Move Speed: " + _moveSpeed);
    }

    bool evasionCheck(int evasion) {
        if(evasion != 0) {
            int randNum = Random.Range(1, 101); //random number from 1 to 100
            if(randNum <= evasion) {
                return true;
            }
        }

        return false;
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _currentJumps = 0;   
        }
    }

    private void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Paused");

        if (!_paused)
        {
            _pauseMenuSM.ChangeState<PauseState>();
            _paused = true;
        }

        else
        {
            _pauseMenuSM.ChangeState<PlayState>();
            _paused = false;
        }
    }

    private void Flex (InputAction.CallbackContext context)
    {

        if (isGrounded)
        {
            StartCoroutine(flexTimer());
            
        }
    }

    IEnumerator flexTimer()
    {
        _playerAnimator.SetTrigger("Flex");
        //isFlexing = true;
        canMove = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(
            _playerAnimator.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
            _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        canMove = true;
        //isFlexing = false;
    }
    
    public override void TakeDmg(int dmg) {
        
        if(!invincible){
            evaded = evasionCheck(evasion);
            if (!evaded) {
                float originalHP = currHP;
                dmg = Mathf.Clamp(dmg, 0, int.MaxValue);
                currHP -= dmg;
                HPBar.SetHealth(currHP, originalHP);
                

                if (currHP <= 0) {
                    Die();
                }

                StartCoroutine(Invincible(invcibilityDuration));

                evaded = false;
            } else {
                StartCoroutine(Invincible(invcibilityDuration));
            }

        }
    }

    public override void Die() {
        //Die in some way
        StartCoroutine(deathTimer());


    }

    IEnumerator deathTimer()
    {
        _playerAnimator.SetTrigger("Die");
        //isFlexing = true;
        canMove = false;
        Debug.Log(transform.name + " died");
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(
            _playerAnimator.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
            _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        Destroy(gameObject);
        //isFlexing = false;
    }
}
