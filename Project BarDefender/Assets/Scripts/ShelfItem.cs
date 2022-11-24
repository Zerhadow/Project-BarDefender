using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShelfItem : Interactable, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private RectTransform rectTransform;

    [SerializeField]
    private Canvas canvas; 

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("Start Drag");
    }

    public void OnDrag(PointerEventData eventData){
        Debug.Log("On Drag");
        rectTransform.anchoredPosition += eventData.delta/ canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("End Drag");
    }

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("On Pointer down");
    }

}
