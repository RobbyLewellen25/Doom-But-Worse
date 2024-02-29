using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 10f;
    public float momentumDampening = 2f;

    private CharacterController myCC;
    public Animator camAnim;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private float myGravity = -10f;

    void Start()
    {
        myCC = GetComponent<CharacterController>();
    }


    void Update()
    {
        GetInput();
        MovePlayer();

        camAnim.SetBool("isWalking", isWalking);
    }

    void GetInput()
    {
        if(Input.GetKey(KeyCode.W) ||
           Input.GetKey(KeyCode.S) ||
           Input.GetKey(KeyCode.A) ||
           Input.GetKey(KeyCode.D))
           {
                inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                inputVector.Normalize();
                inputVector = transform.TransformDirection(inputVector);

            isWalking = true;
           }
        else 
        {
            float speed = inputVector.magnitude;
            float modifiedMomentumDampening = momentumDampening * speed;

            inputVector = Vector3.Lerp(inputVector, Vector3.zero, modifiedMomentumDampening * Time.deltaTime);
            isWalking = false;
        }
        
        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }


    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }
    
}
