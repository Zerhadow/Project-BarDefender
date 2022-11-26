using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Units
{
    public GameObject itemDrop;
    public Rigidbody2D rb;
    public Inventory inv;
    public Interactable interactable;
    public GameObject itemDrops; //item we want to drop

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die() {
        //Die in some way
        //This method is meant to be overwritten
        //drop item
        // ItemDropTest();
        Destroy(gameObject);
        Debug.Log(transform.name + " died");
    }

    void dropIngredient() {
        int listNum = inv.items.Count;
        int randNum = Random.Range(1, 101);
        Item drop = null;

        //find all assets in item folder
        // string[] items2 = AssetDatabase.FindAssets("1:Items", null);
        // assetDB not working

        //depening on rarity, drop a random ingredient

        //roll for rarity: for 1 to 100
        if(randNum >= 25) {
            //looks for common

            do {
                int itemIdx = Random.Range(0, listNum);
                Debug.Log("Item rolled: " + inv.items[itemIdx].name);
                drop = inv.items[itemIdx];
            } while (drop.rarity != "common");

        } else if(randNum >= 10 && randNum < 25) {
            //looks for rare
        } else {
            //looks for legendary
        }

        //add item to gameobject
        itemDrop.AddComponent<Interactable>().radius = 3;
        itemDrop.AddComponent<Interactable>().item = drop;

        //spawn object
        itemDrop = Instantiate(itemDrop, rb.position + Vector2.up * 0.5f, Quaternion.identity);
    }

    // Item ListOfItems() {
    //     string item1 = "Attack Mushroom";
    // }

    /*
    Attack Mushroom
    Blueberry
    Frog Leg
    Hemp Thread
    */

    private void ItemDropTest() {
        Instantiate(itemDrops, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }
}
