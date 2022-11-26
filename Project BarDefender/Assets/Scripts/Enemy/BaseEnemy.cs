using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Units
{
    public GameObject itemDrop;
    public Rigidbody2D rb;
    public Inventory inv;
    public Interactable interactable;

    //possible item drops
    private GameObject[] itemDropList;
    //common drops
    public GameObject itemDropTestObj;

    //rare drops


    //legendary drops


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        itemDropList = {itemDropTestObj};
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

    private int DetermineDropRNG() {
        int randNum = Random.Range(1, 101); //random number from 1 to 100

        if(randNum >= 25) {
            //found common
            return 1;
        } else if(randNum >= 10 && randNum < 25) {
            //looks for rare
            return 2;
        } else {
            //looks for legendary
            return 3;
        }
    }

    GameObject DetermineItemDrop(int rarityTier) {
        int randNum = Random.Range(1, 4); //random number from 1 to 9

        if(rarityTier == 1) {
            randNum = Random.Range(1, 4); //random number from 1 to 3
            return itemDropList[randNum];
        } else if(rarityTier == 2) {
            randNum = Random.Range(1, 4); //random number from 1 to 3
            return itemDropList[randNum];
        } else {
            randNum = Random.Range(1, 4); //random number from 1 to 3
            return itemDropList[randNum];
        }

        return itemDropTestObj;
    }

    private void ItemDropTest() {
        GameObject itemFound = DetermineItemDrop(DetermineDropRNG);

        Instantiate(itemFound, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }
}
