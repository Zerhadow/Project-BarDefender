using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Potion : MonoBehaviour
{
    public int itemCount;
    Stack<Item> itemsAdded = new Stack<Item>();
    public bool potionDrop;
    

    

    

    public void Awake(){
        itemCount = 0;
        potionDrop = false;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Triggered");
        potionDrop = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Not Triggered");
        potionDrop = false;
    }

    public void AddItem(Item x){
        itemCount ++;
        itemsAdded.Push(x);
    }

     public Item RemoveItem(){
        if(itemsAdded.Count == 0){
            Debug.Log("no items in potion");
            return null;
        }
        itemCount --;
        return itemsAdded.Pop();
    }


}
