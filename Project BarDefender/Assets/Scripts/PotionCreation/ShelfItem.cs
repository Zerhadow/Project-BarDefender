using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShelfItem : MonoBehaviour, IPointerDownHandler//, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    


    [SerializeField]
    public Item item;
    private int count;
    public Potion potion;
    public ShelfCounter counterdisplay;
    public TextMeshProUGUI description; 
    public TextMeshProUGUI dialog; 

    public Image img;

    private void Awake(){
        potion = Potion.Instance;
        count = Inventory.instance.Amount(item);
        counterdisplay.updateShelfCount(count);
        description.text = item.description;
        if(item.icon != null){
            img.sprite = item.icon;
        }



    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("On Pointer down");
        dialog.text  = item.phrase;
        if(count > 0){
            potion.AddItem(item);
            count--;
            counterdisplay.updateShelfCount(count);
        }
    }


}
