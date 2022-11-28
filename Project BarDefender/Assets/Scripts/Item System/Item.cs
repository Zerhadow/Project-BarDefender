using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] 
public class Item : ScriptableObject 
{ 
    new public string name = "New Item"; 
    public Sprite icon = null; 
    public bool isDefaultItem = false;      
    public string rarity;   

    public string description; 

    public int maxHP = 0; 
    public int ATK =0 ;
    public float _moveSpeed =0;
    public float _jumpPower =0;
    public float fireCooldown =0;
    public float _jumpCooldown =0;
    public float atkRange = 0.5f;
    public int _maxJumps =0;
    public float _rebound =0; //how much you bounce enemies
    public int evasion = 0;
} 

