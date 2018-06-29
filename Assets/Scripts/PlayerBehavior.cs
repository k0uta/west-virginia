using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float movementSpeed = 10;
    public float turningSpeed = 60;
    Animator playerStateMachine;
    float speed;
    float defaultSpeed = 15.0f;
    float dashingSpeed = 50.0f;
    AnimatorStateInfo currentAnimatorStateInfo;
    AudioSource playerAudio;

    // Use this for initialization
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        speed = defaultSpeed;
        playerStateMachine = GetComponent<Animator>();
    }

    bool IsDashing()
    {
        return currentAnimatorStateInfo.IsName("Dashing");
    }

    void HandlePlayerTranslation()
    {
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        // transform.Translate(0, 0, z);

        if (Mathf.Abs(z) >= 0.001f)
        {
            transform.Translate(transform.forward * Time.deltaTime * speed);
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

        //To make mousePos relative to transform
        mousePos += transform.position;
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
    //Handles Player Inputs
    void HandleInputs()
    {
        if (!IsDashing())
        {
            HandlePlayerRotation();

            HandlePlayerTranslation();

            HandleDashInput();
        }
    }

    // void HandleDash()
    // {
    //     if (IsDashing())
    //     {
    //         speed = dashingSpeed;
    //     }
    //     else
    //     {

    //         speed = defaultSpeed;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        currentAnimatorStateInfo = playerStateMachine.GetCurrentAnimatorStateInfo(0);

        HandleInputs();

        Debug.DrawRay(transform.position, transform.TransformDirection(transform.forward) * 10, Color.blue);
        // HandleDash();
    }
}
