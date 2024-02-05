using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 10f;
    public float momentumDampening = 5f;

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
        CheckForHeadBob();

        camAnim.SetBool("isWalking", isWalking);
    }

    void GetInput()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);

        inputVector = vector3.Lerp(inputVector, Vector3.zero, momentumDampening * time.deltaTime)

        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }


    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }
    
    void CheckForHeadBob() 
    {
        if(myCC.velocity.magnitude>0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}
