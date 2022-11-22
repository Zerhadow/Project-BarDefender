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
    #endregion


    bool canFire = true, canJump = true;
    public PlayerInputActions playerControls;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = new Vector2(1,0);

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction melee;

    public GameObject projectilePrefab;
    public Transform atkPt;
    public LayerMask enemyLayers;

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
    }

    private void FixedUpdate() {

        //if (moveDirection.y * _jumpPower < 0) {
        //    rb.velocity = new Vector2(moveDirection.x * moveSpd, 0);
        //}
        Move();
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
    
        if (canJump)
        {
            Debug.Log("Jumped!");
            StartCoroutine(_jumpTimer(_jumpCooldown));
            rb.AddForce(new Vector2(0f, _jumpPower), ForceMode2D.Impulse);
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
            Debug.Log("Enemy HP: " + enemyStat.currHP);        
        }

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
        Debug.Log("New ATK: " + ATK);
    }
}
