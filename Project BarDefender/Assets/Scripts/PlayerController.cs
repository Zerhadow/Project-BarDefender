using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Units
{
    public Rigidbody2D rb;

    #region Stats & Cooldowns
    public int ATK;
    public float _moveSpeed = 5f;
    public float _jumpPower = 10f;
    public float fireCooldown = 0.3f;
    public float _jumpCooldown = 0.3f;
    public float atkRange = 0.5f;
    public int _maxJumps = 2;
    public float _rebound = 2;
    #endregion


    bool canFire = true, canJump = true;
    public PlayerInputActions playerControls;
    [SerializeField] private int _currentJumps = 0;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] SpriteRenderer _playerSprite;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = new Vector2(1,0);

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction melee;

    public GameObject projectilePrefab;
    public Transform atkPt;
    public LayerMask enemyLayers;
    private bool isGrounded = false;

    // Interactable interactable = hit.collider.GetComponent<Interactable>();


    void Awake() {
        playerControls = new PlayerInputActions();
        ATK = dmg;
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
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        if(!Mathf.Approximately(moveDirection.x, 0.0f) || !Mathf.Approximately(moveDirection.y, 0.0f))
        {
            lookDirection.Set(moveDirection.x, moveDirection.y);
            lookDirection.Normalize();
        }
        _playerAnimator.SetFloat("yVelocity", rb.velocity.y, 0.1f, 0.1f);
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
        //animator.SetTrigger("Attack");

        //Detect enemies in range of atk
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atkPt.position, atkRange, enemyLayers);

        //dmg them
        foreach (Collider2D enemy in hitEnemies) {
            // Debug.Log("We hit " + enemy.name);
            Units enemyStat = enemy.gameObject.GetComponent<Units>();
            enemyStat.TakeDmg(ATK);
            rb.AddForce(new Vector2(-lookDirection.x - (moveDirection.x/2) * _rebound, -lookDirection.x - moveDirection.y * _rebound), ForceMode2D.Impulse); //long jump?
            Debug.Log("Enemy HP: " + enemyStat.currHP);        
        }

    } 

    private void Move()
    {
        transform.position += transform.right * moveDirection.x * _moveSpeed * Time.deltaTime;
        _playerAnimator.SetFloat("xVelocity", moveDirection.x);
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
        Debug.Log("New ATK: " + ATK);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
            _currentJumps = 0;
            _playerAnimator.SetBool("Jump", !isGrounded);
        }

    }
}
