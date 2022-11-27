using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ShelfCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI count; 


    public void updateShelfCount(int cnt)
    {
        count.text = cnt.ToString();
    }
}
