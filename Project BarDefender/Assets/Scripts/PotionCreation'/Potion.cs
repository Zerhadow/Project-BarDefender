using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Potion : MonoBehaviour
{
    public int itemCount;
    public bool potionDrop;

    

    

    public void Awake(){
        itemCount = 0;
        potionDrop = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        potionDrop = true;
    }
}
