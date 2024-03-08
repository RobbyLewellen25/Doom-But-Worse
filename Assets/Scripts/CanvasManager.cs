using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
   public TextMeshProUGUI health;
   public TextMeshProUGUI armor;
   public TextMeshProUGUI ammo;

   public Image healthIndicator;

   public Sprite health1; // Healthy
   public Sprite health2;
   public Sprite health3;
   public Sprite health4;
   public Sprite health5; // Near death
   public Sprite health6; // Dead

   private static CanvasManager _instance;
   public static CanvasManager Instance{get{return _instance;}}

   private void Awake()
   {
    if(_instance != null && _instance != this)
    {
        Destroy(this.gameObject);
    }
    else
    {
        _instance = this;
    }
   }



    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue.ToString() + "%";
        UpdateHealthIndicator(healthValue);
    }
    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString() + "%";
    }
    public void UpdateAmmo(int ammoValue)
    {
        ammo.text = ammoValue.ToString();
    }

    public void UpdateHealthIndicator(int healthValue)
    {
        if(healthValue > 80)
        {
            healthIndicator.sprite = health1;
        }
        else if(healthValue > 60)
        {
            healthIndicator.sprite = health2;
        }
        else if(healthValue > 40)
        {
            healthIndicator.sprite = health3;
        }
        else if(healthValue > 20)
        {
            healthIndicator.sprite = health4;
        }
        else if(healthValue > 0)
        {
            healthIndicator.sprite = health5;
        }
        else
        {
            healthIndicator.sprite = health6;
        }
    }
    public void UpdateKeys()
    {

    }
}
