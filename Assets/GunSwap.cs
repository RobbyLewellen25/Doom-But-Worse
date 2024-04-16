using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwap : MonoBehaviour
{
    public GameObject[] guns;
    private int selectedGun = 0;
    
    [Header("Bullets")]
    private int bullets;
    private int shells;
    private int rockets;
    private int cells;
    public int maxBullets;
    public int maxShells;
    public int maxRockets;
    public int maxCells;

    void Start()
    {
        guns[0].GetComponent<Gun>().isInInvintory = true;
        Activate(0);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
               Activate(i-1);
               Debug.Log("Switching to gun " + (i-1));
            }
        }
    }

    void AddFireArm(int gunNum)
    {
        if(!guns[gunNum].GetComponent<Gun>().isInInvintory)
        {
            guns[gunNum].GetComponent<Gun>().isInInvintory = true;
            guns[gunNum].GetComponent<Gun>().isActive = true;
        }
    }
    private void Activate(int chosen)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].GetComponent<Gun>().isActive = false;
        }
        guns[chosen].GetComponent<Gun>().isActive = true;
    }
}
