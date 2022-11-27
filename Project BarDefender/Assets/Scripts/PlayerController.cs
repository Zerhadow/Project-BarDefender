using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Units
{
    private Rigidbody2D rb;

    #region Stats & Cooldowns
    public int ATK;
    public float _moveSpeed = 5f;
    public float _jumpPower = 10f;
    public float fireCooldown = 0.3f;
    public float _jumpCooldown = 0.3f;
    public float _atkrange = 0.5f;
    public int _maxJumps = 2;
    public float _rebound = 2;
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
    public LayerMask _groundLayer;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = new Vector2(1,0);

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction melee;

    public GameObject projectilePrefab;
    public Transform atkPt;
    public float atkRange = 0.5f;
    public LayerMask enemyLayers; 
    // Interactable interactable = hit.collider.GetComponent<Interactable>(); 
    private bool isGrounded = false;


    void Awake() {
        playerControls = new PlayerInputActions();
        ATK = dmg;

        _animAttackComboStepParamHash = Animator.StringToHash("AttackComboStep");
        _comboHitStep = -1;
        _comboAttackResetCoroutine = null;
    }

    void Start() {
        //for now only the player has a healthbar so only he will call the set health function

        rb = GetComponent<Rigidbody2D>();
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

    }

    private void OnDisable() {
        move.Disable();
        fire.Disable();
        jump.Disable();
        melee.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        _playerAnimator.SetFloat("xVelocity", moveDirection.x, 0.1f, 0.1f);
        _playerAnimator.SetFloat("yVelocity", rb.velocity.y, 0.1f, 0.1f);
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
        } else if (lookDirection.x > 0)
        {
            _playerSprite.flipX = false;
        }


    }

    private void FixedUpdate() {

        //if (moveDirection.y * _jumpPower < 0) {
        //    rb.velocity = new Vector2(moveDirection.x * moveSpd, 0);
        //}
        Move();
        atkPt.position = this.transform.position + new Vector3(lookDirection.x * (atkRange+0.5f), lookDirection.y * (atkRange + 1), 0);
        isGrounded = Physics2D.OverlapCircle(_footPos.position, 1f, _groundLayer);

    }

    private void Fire(InputAction.CallbackContext context) {
        Debug.Log("We Fired");

        if(canFire) {
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
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(
            _playerAnimator.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
            _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        _comboHitStep = -1;
        _playerAnimator.SetInteger(
            _animAttackComboStepParamHash, _comboHitStep);

    }


    private void Move()
    {
        transform.position += transform.right * moveDirection.x * _moveSpeed * Time.deltaTime;
        

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

    public void IncreaseATK_DecreaseHP(string rarity) { 
        ATK += 1;
        maxHP += 20;
        // currHP += 20 maybe won't be needed
        Debug.Log("New ATK: " + ATK);
        Debug.Log("New Max HP: " + maxHP); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _currentJumps = 0;   
        }
    }
}
