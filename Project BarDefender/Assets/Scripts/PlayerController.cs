using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Units
{
    public Rigidbody2D rb;

    #region Stats & Cooldowns
    public int ATK;
    public float moveSpd = 5f;
    public float jumpPwr = 10f;
    public float fireCooldown = 0.3f;
    public float jumpCooldown = 0.3f;
    #endregion


    bool canFire = true, canJump = true;
    public PlayerInputActions playerControls;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = new Vector2(1,0);

    private InputAction move;
    private InputAction fire;
    private InputAction jump;

    public GameObject projectilePrefab;


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
    }

    private void OnDisable() {
        move.Disable();
        fire.Disable();
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
        if(moveDirection.y * jumpPwr > 0) {
            if(canJump) {
                StartCoroutine(jumpTimer(jumpCooldown));         
                rb.velocity = new Vector2(moveDirection.x * moveSpd, moveDirection.y * jumpPwr);
            }
        } else {
            rb.velocity = new Vector2(moveDirection.x * moveSpd, 0);
        }
        //rb.velocity = new Vector2(moveDirection.x * moveSpd, moveDirection.y * jumpPwr);
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

    IEnumerator fireTimer(float timer){
        canFire = false;
        while(timer > 0){
            yield return null;
            timer -= Time.deltaTime;
        }
        canFire = true;
    }

    IEnumerator jumpTimer(float timer){
        canJump = false;
        while(timer > 0){
            yield return null;
            timer -= Time.deltaTime;
        }
        canJump = true;
    }
}
