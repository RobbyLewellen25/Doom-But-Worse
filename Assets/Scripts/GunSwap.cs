using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwap : MonoBehaviour
{
    public GameObject[] guns;
    private int selectedGun = 1;
    
    [Header("Bullets")]
    private int bullets;
    private int shells;
    private int rockets;
    private int cells;
    public int maxBullets;
    public int maxShells;
    public int maxRockets;
    public int maxCells;
    private List<bool> inventory;

    void Start()
    {
        guns[0].GetComponent<Gun>().isInInventory = true;
        Activate(selectedGun);
        UpdateUIAmmoCounter(selectedGun);
        UpdateGunInventory();
        SendArms();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
               Activate(i-1);
               // Debug.Log("Switching to gun " + (i-1));
            }
        }
        UpdateUIAmmoCounter(selectedGun);
    }

    void AddFireArm(int gunNum)
    {
        if(!guns[gunNum].GetComponent<Gun>().isInInventory)
        {
            guns[gunNum].GetComponent<Gun>().isInInventory = true;
            guns[gunNum].GetComponent<Gun>().isActive = true;
            UpdateGunInventory();
            SendArms();
        }
    }
    private void Activate(int chosen)
    {
        selectedGun = chosen;
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].GetComponent<Gun>().isActive = false;
        }
        guns[chosen].GetComponent<Gun>().isActive = true;
        if (guns[chosen].GetComponent<Gun>().isChainsaw == true)
            {
                guns[chosen].GetComponent<Gun>().chainsawState = Gun.ChainsawState.Selected;
            }
    }
    public void GiveAmmo(int bul, int she, int roc, int cel, GameObject pickup)
    {
        if (bullets < maxBullets) 
            {
                bullets += bul;
                Destroy(pickup);
            }
        if (shells < maxShells) 
            {
                shells += she;
                Destroy(pickup);
            }
        if (rockets < maxRockets) 
            {
                rockets += roc;
                Destroy(pickup);
            }
        if (cells < maxCells) 
            {
                cells += cel;
                Destroy(pickup);
            }
        if (bullets > maxBullets) bullets = maxBullets;
        if (shells > maxShells) shells = maxShells;
        if (rockets > maxRockets) rockets = maxRockets;
        if (cells > maxCells) cells = maxCells;
        Debug.Log("Ammo picked up");
        UpdateUIAmmoCounter(selectedGun);
    }
    public void TakeAmmo(int bul, int she, int roc, int cel)
    {
        bullets -= bul;
        shells -= she;
        rockets -= roc;
        cells -= cel;
        if (bullets < 0) bullets = 0;
        if (shells < 0) shells = 0;
        if (rockets < 0) rockets = 0;
        if (cells < 0) cells = 0;
    }
    public bool HasBullets() {
            if (bullets > 0) return true;
            else return false;
    }
    public bool HasShells() {
            if (shells > 0) return true;
            else return false;
    }
    public bool HasRockets() {
            if (rockets > 0) return true;
            else return false;
    }
    public bool HasCells() {
            if (cells > 0) return true;
            else return false;
    }

    public int GetBullets() {
            return bullets;
    }
    public int GetShells() {
            return shells;
    }
    public int GetRockets() {
            return rockets;
    }
    public int GetCells() {
            return cells;
    }

    private void UpdateUIAmmoCounter(int j)
    {
        switch (guns[j].GetComponent<Gun>().ammoType)
        {
            case Gun.AmmoType.Bullets:
                CanvasManager.Instance.UpdateAmmo(bullets);
                break;
            case Gun.AmmoType.Shells:
                CanvasManager.Instance.UpdateAmmo(shells);
                break;
            case Gun.AmmoType.Rockets:
                CanvasManager.Instance.UpdateAmmo(rockets);
                break;
            case Gun.AmmoType.Cells:
                CanvasManager.Instance.UpdateAmmo(cells);
                break;
            case Gun.AmmoType.None:
                CanvasManager.Instance.UpdateAmmo(-5);
                break;
        }
    }

    private void UpdateGunInventory()
    {
        List<bool> inInventory = new List<bool>();

        for (int i = 0; i < guns.Length; i++)
        {
            bool isInInventory = guns[i].GetComponent<Gun>().isInInventory;
            inInventory.Add(isInInventory);
        }

        inventory = inInventory;
    }

    private void SendArms()
    {
        CanvasManager.Instance.UpdateArmStatus(inventory);
    }

}