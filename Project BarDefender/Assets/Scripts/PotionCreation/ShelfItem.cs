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
    /*public void OnBeginDrag(PointerEventData eventData){
        //Debug.Log("Start Drag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData){
        //Debug.Log("On Drag");
        rectTransform.anchoredPosition += eventData.delta/ canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData){
        //Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (potion.potionDrop)
        {
            //Debug.Log("Dropped on potion");
            // events when dropped on potion
            count -= 1;
            counterdisplay.updateShelfCount(count);
            potion.itemCount += 1;

            if (count <= 0)
            {
                Destroy(this.gameObject);
            }
            else 
            {
                transform.position = startPos;
            }
            
        }
        else
        {
            transform.position = startPos;
        }
    }*/

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("On Pointer down");
        if(count > 0){
            potion.AddItem(item);
            count--;
            counterdisplay.updateShelfCount(count);
        }
    }


    /*public void OnDrop(PointerEventData eventData){
        //Debug.Log("Drop");
        

        //item count -1 
        //dragged item disappears 

        
    }*/


}
