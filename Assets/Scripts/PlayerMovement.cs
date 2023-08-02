using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float sprintSpeed;
    public float walkSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYscale;
    public float startYscale;

    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYscale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x,crouchYscale,transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(crouchKey)){
            transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
        }
    }

    private void StateHandler() {

        if (Input.GetKey(crouchKey)){
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
            Debug.Log("crouching");
        }


        if (grounded && Input.GetKey(sprintKey) && state != MovementState.crouching){
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
            Debug.Log("sprinting");
        }

        else if (grounded && state != MovementState.crouching)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            Debug.Log("walking");
        }
        else
        {
            state = MovementState.air;
            Debug.Log("air");
        }
    }

    private void MovePlayer()
    {
        moveDirection = Vector3.ProjectOnPlane(orientation.forward * verticalInput + orientation.right * horizontalInput, new Vector3(0f,1f,0f));



        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    
    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x,0f, rb.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
