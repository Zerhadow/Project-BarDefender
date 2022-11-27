using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShelfItem : MonoBehaviour, IPointerDownHandler//, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    


    [SerializeField]
    public Item item;
    public int count;
    public Potion potion;
    public ShelfCounter counterdisplay;
    public Image img;

    private void Awake(){
        counterdisplay.updateShelfCount(count);
        if(item.icon != null){
            img.sprite = item.icon;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("On Pointer down");
        if(count > 0){
            potion.AddItem(item);
            count--;
            counterdisplay.updateShelfCount(count);
        }
        
    }


}
