using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    
    public bool isArmor;
    public bool isHealth;
    public bool isAmmo;

    public int amount;


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
                other.GetComponent<PlayerHealth>().GiveHealth(amount, this.gameObject);
            }
            if(isArmor)
            {
                other.GetComponent<PlayerHealth>().GiveArmor(amount, this.gameObject);
            }
            if(isAmmo)
            {
                other.GetComponent<Gun>().GiveAmmo(amount, this.gameObject);
            }

        }
    }
}
