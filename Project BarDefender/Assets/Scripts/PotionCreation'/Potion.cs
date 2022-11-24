using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Potion : MonoBehaviour, IDropHandler
{
    public int itemCount; 
    public void OnDrop(PointerEventData eventData){
        Debug.Log("OnDrop potion");
        //item effects added to potion
    }

    public void Awake(){
        itemCount = 0;
    }
}
