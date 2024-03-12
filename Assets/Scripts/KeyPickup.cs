using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public bool isRedKey, isBlueKey, isYellowKey;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
                if (isRedKey) {other.GetComponent<PlayerInventory>().hasRed = true;}
                if (isBlueKey) {other.GetComponent<PlayerInventory>().hasBlue = true;}
                if (isYellowKey) {other.GetComponent<PlayerInventory>().hasYellow = true;}
                Destroy(gameObject);
            }
    }
}
