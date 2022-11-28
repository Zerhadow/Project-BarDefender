using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Potion : MonoBehaviour
{

    public static Potion instance;
    #region Singleton

    public void Awake(){
        itemCount = 0;

         if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
        
    }
    #endregion

    public int itemCount;
    Stack<Item> itemsAdded = new Stack<Item>();
    public Item temp;

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

    public void ApplytoPlayer(){
        // script that will pop all items in potion applying effects to player
        Item x = RemoveItem();
        do{
            findSkill(x.name);
        }while(x != null);
    }

    void findSkill(string name) { 
        switch (name) { 
            case "Attack Mushroom": 
                //call function 
                //IncreaseATK_DecreaseHP("common"); 
                Debug.Log("Attack Mushroom Aplied"); 
                break;
            case "Blueberry": 
                //call function 
                //IncreaseATK_DecreaseHP("common");
                Debug.Log("Blue Berry Aplied"); 
                break;
                case "Frog Leg": 
                //call function 
                //IncreaseATK_DecreaseHP("common"); 
                Debug.Log("Frog Leg Aplied");
                break;
                case "Hemp Thread": 
                //call function 
                //IncreaseATK_DecreaseHP("common"); 
                Debug.Log("Hemp Aplied");
                break;
            default: 
                Debug.Log("Hemp Aplied");
                break; 
        }

    }


}
