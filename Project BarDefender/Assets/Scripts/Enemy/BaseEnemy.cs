using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Units
{
    public Rigidbody2D rb;

    //possible item drops
    public List<GameObject> itemDropList = new List<GameObject>();
    //common drops
    public GameObject itemDropTestObj;
    

    //rare drops


    //legendary drops


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        //add all items to list
        itemDropList.Add(itemDropTestObj);
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
        GameObject itemFound = DetermineItemDrop(DetermineDropRNG());

        Instantiate(itemFound, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }
}
