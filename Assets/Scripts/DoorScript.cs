using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animator doorAnim;
    private bool isOpen = false;
    private bool inTrigger = false;
    private int count = 0;

    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    void Update()
    {

    }

   private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player" || other.tag == "Enemy")
       {
            isOpen = true;
            inTrigger = true;
            count++;
            doorAnim.SetBool("openSeasame", isOpen);
            doorAnim.SetBool("closeSeasame", !isOpen);
       }

   }

    private void OnTriggerExit(Collider other)
    {
        count--;
        if(other.tag == "Player" || other.tag == "Enemy" && count == 0)
        {
            inTrigger = false;
            isOpen = false;
            doorAnim.SetBool("closeSeasame", !isOpen);
            doorAnim.SetBool("openSeasame", isOpen);
        }
    }

 //   void OnTriggerStay(Collider other)
 //   {
 //       if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
 //       {
 //           inTrigger = true;
 //       }
 //       else
 //       {
 //           inTrigger = false;
//        }
 //   }

    
}
