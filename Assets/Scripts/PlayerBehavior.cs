using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float movementSpeed = 10;
    public float turningSpeed = 60;

    AudioSource playerAudio;

    // Use this for initialization
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Updates player color
    void UpdatePlayerColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (Input.GetKey(KeyCode.Space))
        {
            renderer.material.color = Color.yellow;
        }
        else
        {
            renderer.material.color = Color.green;
        }
    }

    void HandlePlayerTranslation()
    {
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 15.0f;
        transform.Translate(0, 0, z);

        if (Mathf.Abs(z) >= 0.001f)
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
    //Handles Player Inputs
    void HandleInputs()
    {
        HandlePlayerRotation();

        HandlePlayerTranslation();
    }

    void HandleAudio()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerColor();

        HandleInputs();

        HandleAudio();
    }
}
