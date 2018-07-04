using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    Animator playerStateMachine;
    AnimatorStateInfo currentAnimatorStateInfo;
    AudioSource playerAudio;
    CharacterController characterController;
    float speed = 15.0f;

    // Use this for initialization
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerStateMachine = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    bool IsDashing()
    {
        return currentAnimatorStateInfo.IsName("Dashing");
    }

    void HandlePlayerTranslation()
    {
        Vector3 forwardMove = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
        characterController.Move(forwardMove);

        if (Mathf.Abs(forwardMove.magnitude) >= 0.001f)
        {
            playerAudio.UnPause();
        }
        else
        {
            playerAudio.Pause();
        }
    }

    void HandlePlayerRotation()
    {
        Vector3 mousePos = Input.mousePosition;

        //To make mousePos relative to center of screen
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;

        var angle = Vector3.Angle(mousePos, Vector3.up);

        //For 360 degree angle
        if (mousePos.x > 0)
        {
            angle = 360 - angle;
        }

        transform.rotation = Quaternion.Euler(0, -angle, 0);
    }

    void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsDashing())
        {
            playerStateMachine.SetTrigger("Dash");
        }
    }

    void HandlePhysics()
    {
        characterController.SimpleMove(Physics.gravity);
    }

    //Handles Player Inputs
    void HandleInputs()
    {
        if (!IsDashing())
        {
            HandlePlayerRotation();

            HandlePlayerTranslation();

            HandleDashInput();

            HandlePhysics();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentAnimatorStateInfo = playerStateMachine.GetCurrentAnimatorStateInfo(0);

        HandleInputs();

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.blue);
    }
}
