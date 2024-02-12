using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;

    public int maxArmor;
    private int armor;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        armor = maxArmor; //for test purposes
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DamagePlayer(30);
            
        }
    }

    public void DamagePlayer(int damage)
    {
        if(armor > 0)
        {
           if(armor >= damage)
           {
               armor -= damage;
           }
           else if(armor < damage)
           {
               int remainingDamage;
               
               remainingDamage = damage - armor;

               armor = 0;

               health -= remainingDamage;
           }

        }
        else
        {
            health -= damage;
        }
    }
}
