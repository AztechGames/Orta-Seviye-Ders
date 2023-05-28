using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    
    public Vector3 velocity;
    
    public Transform groundCheck;
    
    public LayerMask groundMask;
    
    private CharacterController _controller;
    
    private bool _isGrounded;

    [HideInInspector]
    public Animator _anim;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }
    
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Input

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * (speed * Time.deltaTime));
        _controller.Move(velocity * Time.deltaTime);
        if(x != 0 || z != 0 ) _anim.SetBool("Run",true);
        else _anim.SetBool("Run",false);
        //Movement
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hit, 100))
        { 
            Vector3 lookPos = hit.point - transform.position;
            lookPos.y = 0;
            transform.GetChild(0).rotation = Quaternion.LookRotation(lookPos);
        }
        //Rotation
        
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(_isGrounded && velocity.y < 0) velocity.y = -2f;
        else velocity.y += gravity * 10 * Time.deltaTime;
        //Gravity
        
        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            _anim.SetTrigger("Jump");
        }
        //Jump
    }
    
}
