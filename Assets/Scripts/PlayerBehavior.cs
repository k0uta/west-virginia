using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        var interaction = other.gameObject.GetComponent<IInteraction>();
        if (interaction != null)
        {
            interaction.StartInteraction(gameObject, IsDashing());
        }
    }
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        var interaction = other.gameObject.GetComponent<IInteraction>();
        if (interaction != null)
        {
            interaction.UpdateInteraction(gameObject, IsDashing());
        }
    }
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        var interaction = other.gameObject.GetComponent<IInteraction>();
        if (interaction != null)
        {
            interaction.FinishInteraction(gameObject, IsDashing());
        }
    }

    public void Hit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        currentAnimatorStateInfo = playerStateMachine.GetCurrentAnimatorStateInfo(0);

        HandleInputs();
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.blue);
    }
}
