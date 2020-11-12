using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //Movement speed
    [SerializeField]
    float movementSpeed = 1.4f;

    //Movement vector
    Vector2 movement = new Vector2();

    //Defining the rigidbody
    Rigidbody2D rb2d;

    //Defining the Animator for switching between animations
    Animator animator;

    //Name of the animation
    string animationState = "AnimationState";

    //Facing right check
    bool facingRight;

    //Defining various character states
    enum CharStates
    {
        run = 1,
        jump = 2,
        idle = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        Flip();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        print(movement);

        rb2d.velocity = movement * movementSpeed;

    }

    void UpdateState()
    {
        if(movement.x > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.run);
        }
        else if(movement.x < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.run);
        }
        else
        {
            animator.SetInteger(animationState, (int)CharStates.idle);
        }
    }
    void Flip()
    {
        float horizontal = movement.x;
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
