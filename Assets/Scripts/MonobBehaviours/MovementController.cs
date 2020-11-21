using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //Movement speed
    [SerializeField]
    float movementSpeed = 1.4f;

    // Horizontal Movement
    float horizontalMove;

    //Defining the Animator for switching between animations
    Animator animator;

    //Name of the animation
    string animationState = "AnimationState";

    bool jump = false;
    bool crouch = false;

    [SerializeField]
    CharacterController2D controller;

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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        MoveCharacter();
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;

        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void MoveCharacter()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        print(horizontalMove);
    }

    void UpdateState()
    {
        if (horizontalMove != 0)
        {
            animator.SetInteger(animationState, (int)CharStates.run);
        }
        else if (jump)
        {
            animator.SetInteger(animationState, (int)CharStates.jump);
        }
        else
        {
            animator.SetInteger(animationState, (int)CharStates.idle);
        }
    }
}
