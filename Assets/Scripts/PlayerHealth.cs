using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 200;
    private int health;

    public int maxArmor;
    private int armor;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        armor = 0; //for test purposes
        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmor(armor);
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
        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmor(armor);
    }

    public void GiveHealth(int amount, GameObject pickup, bool isTwo)
    {
        if(health < 100 && !isTwo)
        {
            health += amount;
            Destroy(pickup);
            if(health > 100)
            {
                health = 100;
            }
        }
        else if(health < maxHealth && isTwo)
        {
            health += amount;
            Destroy(pickup);
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

        CanvasManager.Instance.UpdateHealth(health);
    }
    public void GiveArmor(int amount, GameObject pickup, bool isTwo)
    {
        if(armor < 100 && !isTwo)
        {
            armor += amount;
            Destroy(pickup);
            if(armor > 100)
            {
                armor = 100;
            }
        }
        else if(armor < maxArmor && isTwo)
        {
            armor += amount;
            Destroy(pickup);
            if(armor > maxArmor)
            {
                armor = maxArmor;
            }
        }
        CanvasManager.Instance.UpdateArmor(armor);
    }
}

