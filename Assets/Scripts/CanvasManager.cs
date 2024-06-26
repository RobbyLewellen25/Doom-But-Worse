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
        public Sprite yellFace;
        public Sprite angryFace;
        public Sprite angryRightFace;
        public Sprite angryLeftFace;
    }

    public Sprite happyExitedSoMuchFunFace;
    public Sprite thisWhatGodFeelLike;

    public HealthSprites[] healthSprites;

    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI bullets;
    public TextMeshProUGUI shells;
    public TextMeshProUGUI rockets;
    public TextMeshProUGUI cells;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI gunTwo;
    public TextMeshProUGUI gunThree;
    public TextMeshProUGUI gunFour;
    public TextMeshProUGUI gunFive;
    public TextMeshProUGUI gunSix;
    public TextMeshProUGUI gunSeven;

    public TMP_FontAsset yellowFont; 
    public TMP_FontAsset grayFont; 

    public GameObject player;
    private GunSwap gunSwap;

    public Image healthIndicator;

    private static CanvasManager _instance;
    public static CanvasManager Instance { get { return _instance; } }
    

    public int faceUpdateWait = 2;
    private int faceUpdateCounter = 0;
    private int previousHealth;   

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
        gunSwap = player.GetComponent<GunSwap>();
        UpdateAmmo(0);
    }

    private void FixedUpdate ()
    {
        UpdateHealthIndicator(int.Parse(health.text.Replace("%", "")));
    }

    public void UpdateHealth(int healthValue)
    {
        int healthLost = previousHealth - healthValue;
        int healthLevel = Mathf.Clamp(healthValue / (100 / (healthSprites.Length - 1)), 0, healthSprites.Length - 1);

        if (healthLost > 0) // Only update face if health was lost
        {
            if (healthLost < 20)
            {
                StartCoroutine(ShowAngryFaceForSeconds(0.5f, healthLevel));
            }
            else
            {
                StartCoroutine(ShowYellFaceForSeconds(0.5f, healthLevel));
            }
        }

        previousHealth = healthValue; // Update previousHealth for the next call
        health.text = healthValue.ToString() + "%";
        UpdateHealthIndicator(healthValue);
    }

    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString() + "%";
    } 

    public void UpdateAmmo(int ammoValue)
    {
        if (ammoValue == -5) {ammo.text = " ";}
        else {ammo.text = ammoValue.ToString();}
        bullets.text = gunSwap.GetBullets().ToString();
        shells.text = gunSwap.GetShells().ToString();
        rockets.text = gunSwap.GetRockets().ToString();
        cells.text = gunSwap.GetCells().ToString();
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
                faceSprite = happyExitedSoMuchFunFace;
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
            return happyExitedSoMuchFunFace;
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

    private IEnumerator ShowAngryFaceForSeconds(float seconds, int healthLevel)
    {
        // Set face to angryFace
        healthIndicator.sprite = healthSprites[healthLevel].angryFace;

        // Wait for seconds
        yield return new WaitForSeconds(seconds);

        // Reset face to normalFace
        healthIndicator.sprite = healthSprites[healthLevel].normalFace;
    }

    private IEnumerator ShowYellFaceForSeconds(float seconds, int healthLevel)
    {
        // Set face to yellFace
        healthIndicator.sprite = healthSprites[healthLevel].yellFace;

        // Wait for seconds
        yield return new WaitForSeconds(seconds);

        // Reset face to normalFace
        healthIndicator.sprite = healthSprites[healthLevel].normalFace;
    }

    public void UpdateArmStatus(List<bool> inInventory)
{
    List<TextMeshProUGUI> guns = new List<TextMeshProUGUI> { gunTwo, gunThree, gunFour, gunFive, gunSix, gunSeven };

    if (inInventory.Count != guns.Count)
    {
        Debug.LogError("inInventory list does not have the same number of elements as guns list");
        return;
    }

    for (int i = 0; i < inInventory.Count; i++)
    {
        guns[i].font = inInventory[i] ? yellowFont : grayFont;
        Debug.Log("Gun " + i + " is " + (inInventory[i] ? "in" : "not in") + " inventory");
    }
}

    public void UpdateKeys()
    {
        // Update keys logic here
    }

    //currently, the health value of the sprite array is only changing when the face direction is also changing. Can we make the face instead update to angryFace for .5 seconds whenever UpdateHealth() is called and the player loses less than 20 health, and make the face update to yellFace when they lose over 20 health?
}