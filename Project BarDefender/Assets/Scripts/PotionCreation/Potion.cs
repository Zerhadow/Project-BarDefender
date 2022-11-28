using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Potion : MonoBehaviour
{

    public static Potion Instance;
    #region Singleton

    public void Awake(){
        itemCount = 0;

         if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;
            DontDestroyOnLoad(this.gameObject); 
        } 
        
    }
    #endregion

    public int maxHP = 0; 
    public int ATK =0 ;
    public float _moveSpeed =0;
    public float _jumpPower =0;
    public float fireCooldown =0;
    public float _jumpCooldown =0;
    public float atkRange = 0f;
    public int _maxJumps =0;
    public float _rebound =0; //how much you bounce enemies
    public int evasion = 0;

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
