using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        armor = 0; //for test purposes
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

        if(health <= 0)
        {

            Debug.Log("Player is dead");

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if(health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void GiveArmor(int amount, GameObject pickup)
    {
        if(armor < maxArmor)
        {
            armor += amount;
            Destroy(pickup);
        }

        if(armor > maxArmor)
        {
            armor = maxArmor;
        }
    }
}

