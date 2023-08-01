using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    CharacterController callController;
    Vector3 moveSpeed;
    public float speed;

    void Start()
    {
        callController=GetComponent<CharacterController>();
    }
    void Update()
    {
        moveSpeed = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) ;
        callController.SimpleMove(moveSpeed*speed);
    }
}
