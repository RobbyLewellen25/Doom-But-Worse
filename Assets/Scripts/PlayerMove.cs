using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 20f;
    public float playerRunSpeed = 30f;
    private float playerTempSpeed;
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
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    playerTempSpeed = playerRunSpeed;
                    inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                    inputVector.Normalize();
                    inputVector = transform.TransformDirection(inputVector);

                    isWalking = true;
                }
                else
                {
                    playerTempSpeed = playerSpeed;
                    inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                    inputVector.Normalize();
                    inputVector = transform.TransformDirection(inputVector);

                    isWalking = true;
                }
           }
        else 
        {

            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDampening * Time.deltaTime);
            isWalking = false;
        }
        
        movementVector = (inputVector * playerTempSpeed) + (Vector3.up * myGravity);
    }


    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }
    
}
