using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Interactable : MonoBehaviour 
{ 
    public float radius = 3f; 
    public Transform interactionTransform; 
    public Item item; 
    GameObject playerPrefab; 
    PlayerController player; 

    void OnDrawGizmosSelected() { 
        Gizmos.color = Color.yellow; 
        Gizmos.DrawWireSphere(transform.position, radius); 
    } 

    void OnTriggerEnter2D(Collider2D other) { 
        if(other.tag == "Player") { 
            Debug.Log("Item was interacted with " + other.name); 
            PickUp(); 
        } 
    } 

    private void Update() { 
        // player = playerPrefab.GetComponent<PlayerController>(); 
    } 

    void PickUp() { 
        Debug.Log("Picking up " + item.name); 
        bool wasPickedUp = Inventory.instance.Add(item); 
        findSkill(item.name); 

        if(wasPickedUp) {Destroy(gameObject);} 

    } 

    void findSkill(string name) { 
        switch (name) { 
            case "Attack Mushroom S": 
                //call function 
                //IncreaseATK_DecreaseHP("common"); 
                break; 
            default: 
                break; 
        } 

    } 
} 
