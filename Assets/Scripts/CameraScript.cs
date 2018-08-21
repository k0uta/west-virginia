using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;       //Public variable to store a reference to the player game object

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        Vector3 currentPlayerPos = player.transform.position;

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = currentPlayerPos;

        transform.Translate(Vector3.forward * -34.2f);
        transform.Translate(Vector3.up * 1);
    }
}