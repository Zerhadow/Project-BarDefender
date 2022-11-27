using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : Interactable
{
    Rigidbody2D rb;
    public float dropForce = 5f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //looks on its self for rb
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
    }

}
