using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class Inventory : MonoBehaviour 
{ 
    public static Inventory instance; 

    #region Singleton 

    void Awake() { 
        if(instance != null) { 
            Debug.LogWarning("More than one instance of Inventory found!"); 
            return; 
        } 
        instance = this; 

        count = 0; 
    } 

    #endregion 

    public int space = 20; 
    private int count; 

    public List<Item> items = new List<Item>(); 

    public bool Add(Item item) { 
        if(!item.isDefaultItem) { 
            if(count >= space) { 
                Debug.Log("Not enough room."); 
                return false; 
            } else { 
                count++; 
                // Debug.Log("Count: " + count); 
                items.Add(item); 
            } 

        } 

        return true; 
    } 

    public void Remove(Item item) { 
        items.Remove(item); 
        count--; 
    }

    
} 

