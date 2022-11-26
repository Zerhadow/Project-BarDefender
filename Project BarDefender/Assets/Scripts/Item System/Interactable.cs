using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Interactable : MonoBehaviour 
{ 
    public float radius = 3f; 
    private Transform interactionTransform; 
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

    void start() {
        interactionTransform = GetComponent<Transform>();
    }

    private void Update() { 
        // player = playerPrefab.GetComponent<PlayerController>();
        interactionTransform = this.GetComponent<Transform>();
    } 

    void PickUp() { 
        Debug.Log("Picking up " + item.name); 
        bool wasPickedUp = Inventory.instance.Add(item); 
        findSkill(item.name); 

        if(wasPickedUp) {Destroy(gameObject);} 

    } 

    void findSkill(string name) { 
        switch (name) { 
            case "Attack Mushroom": 
                //call function 
                // IncreaseATK_DecreaseHP("common"); 
                break;
            case "Blueberry": 
                //call function 
                //IncreaseATK_DecreaseHP("common"); 
            break;
                case "Frog Leg": 
                //call function 
                //IncreaseATK_DecreaseHP("common"); 
                break;
                case "Hemp Thread": 
                //call function 
                //IncreaseATK_DecreaseHP("common"); 
                break;
            default: 
                break; 
        }

    } 
} 
