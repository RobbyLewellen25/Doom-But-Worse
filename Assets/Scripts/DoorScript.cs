using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator doorAnim;
    private bool isOpen = false;
    private bool inTrigger = false;

    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    void Update()
    {
        doorAnim.SetBool("openSeasame", isOpen);
    }

   private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player" || other.tag == "Enemy")
       {
            isOpen = true;
       }
   }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Enemy" && inTrigger == false)
        {
            inTrigger = false;
            isOpen = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            inTrigger = true;
        }
        else
        {
            inTrigger = false;
        }
    }

    
}
