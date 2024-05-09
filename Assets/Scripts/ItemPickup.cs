using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    
    public bool isArmor;
    public bool isHealth;
    public bool isTwo;
    public bool isAmmo;

    public int amount;
    public int bullet;
    public int shell;
    public int rocket;
    public int cell;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            if(isHealth) 
            {
                other.GetComponent<PlayerHealth>().GiveHealth(amount, this.gameObject, isTwo);
            }
            else if(isArmor)
            {
                other.GetComponent<PlayerHealth>().GiveArmor(amount, this.gameObject, isTwo);
            }
            else if(isAmmo)
            {
                other.GetComponent<GunSwap>().GiveAmmo(bullet, shell, rocket, cell, this.gameObject);
            }

        }
    }
}
