using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed = 8;
    public static float forwardSpeeds;
    public float Speed;
    public float maxSpeed;


    private int desiredLane = 1; // 0 for left; 1 for middle and 2 for right
    public float laneDistance = 3; // Distance between the lane
    
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight = 2;
    public float Gravity = -12f;
    private Vector3 velocity;
    public Animator animator;
    private bool isSliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerManager.isGameStarted)
            return;
                

        // increasing speed
        if(forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f*Time.deltaTime;
            forwardSpeeds = forwardSpeed;

        }
        

        animator.SetBool("isGameStarted", true);
        if(TileManager.speeds == false)
        {
            direction.z = Speed;
        }else{
            direction.z = forwardSpeeds;
        }
        
        
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("IsGrounded" , isGrounded);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        if(isGrounded)
        {
            if( Input.GetKeyDown(KeyCode.UpArrow))
                jump();

            if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
                StartCoroutine(Slide());
        }
        else
        {
            velocity.y += Gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
            {
                StartCoroutine(Slide());
                velocity.y = -10;
            }                
        }
        controller.Move(velocity * Time.deltaTime);
        //player lane location
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if(desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        // calculating the current position
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        if(transform.position != targetPosition);
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 5 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
            //controller.center = controller.center;
        }
        

        controller.Move(direction*Time.deltaTime);
    }



    private void jump()
    {
        StopCoroutine(Slide());
        animator.SetBool("isSliding", false);
        FindObjectOfType<AudiioManager>().PlaySound("Jump");
        animator.SetTrigger("jump");
        controller.center = Vector3.zero;
        controller.height = 2;
        isSliding = false;

        velocity.y = Mathf.Sqrt(jumpHeight * 2 * -Gravity);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if(hit.transform.tag =="Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }   
    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("IsSliding", true);
        controller.center = new Vector3(0, -0.5f,0);
        controller.height = 1;
        yield return new WaitForSeconds(1f);
        controller.center = new Vector3(0,0,0);
        controller.height = 2;
        animator.SetBool("IsSliding", false);
        isSliding = false;
    }
}

