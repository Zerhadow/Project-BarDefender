using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShelfItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    


    [SerializeField]
    private Canvas canvas; 
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Item item;
    public int count;
    public Potion potion; 

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("Start Drag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData){
        Debug.Log("On Drag");
        rectTransform.anchoredPosition += eventData.delta/ canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (potion.potionDrop)
        {
            Debug.Log("Dropped on potion");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer down");
    }


    public void OnDrop(PointerEventData eventData){
        Debug.Log("Drop");
        

        //item count -1 
        //dragged item disappears 

        
    }


}