using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [System.Serializable]
    public class HealthSprites
    {
        public Sprite normalFace;
        public Sprite leftFace;
        public Sprite rightFace;
        public Sprite evilFace;
        public Sprite painFace;
        public Sprite angryFace;
        public Sprite angryRightFace;
        public Sprite angryLeftFace;
    }

    public Sprite deadFace;
    public Sprite thisWhatGodFeelLike;

    public HealthSprites[] healthSprites;

    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI ammo;
    public Image healthIndicator;

    private static CanvasManager _instance;
    public static CanvasManager Instance { get { return _instance; } }
    
    // Changeable wait value
    public int faceUpdateWait = 2;

    // Counter for face update ticks
    private int faceUpdateCounter = 0;   

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void FixedUpdate ()
    {
        UpdateHealthIndicator(int.Parse(health.text.Replace("%", "")));
    }

    public void UpdateHealth(int healthValue)
    {
        if(healthValue <= 0) { healthValue = 0; }
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

    private void UpdateHealthIndicator(int healthValue)
    {
        // Increment the counter
        faceUpdateCounter++;
        int healthLevel = Mathf.Clamp(healthValue / (100 / (healthSprites.Length - 1)), 0, healthSprites.Length - 1);

        // If the counter has reached the wait value, update the face direction
        if (faceUpdateCounter >= faceUpdateWait)
        {
            Sprite faceSprite = FaceIndicator(healthLevel);
            if(healthValue <= 0)
            {
                faceSprite = deadFace;
            }
            healthIndicator.sprite = faceSprite;

            // Reset the counter
            faceUpdateCounter = 0;
        }
    }

    public Sprite FaceIndicator(int healthLevel)
    {

        if(healthLevel < 0)
        {
            return deadFace;
        }

        // Randomly select a face direction
        int faceDirection = UnityEngine.Random.Range(0, 3);

        switch (faceDirection)
        {
            case 0: // Normal face
                return healthSprites[healthLevel].normalFace;
            case 1: // Left face
                return healthSprites[healthLevel].leftFace;
            case 2: // Right face
                return healthSprites[healthLevel].rightFace;
            default: // Default to normal face
                return healthSprites[healthLevel].normalFace;
        }
        
    }

    public void UpdateKeys()
    {
        // Update keys logic here
    }

    //currently, the health value of the sprite array is only changing when the face direction is also changing. Can we make the face instead update to angryFace for .5 seconds whenever UpdateHealth() is called and the player loses less than 20 health, and make the face update to painFace when they lose over 20 health?
}