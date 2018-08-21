﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour, IInteraction
{

    public void StartInteraction(GameObject targetObject, bool isDashing)
    {
        if (isDashing)
        {
            Physics.IgnoreCollision(targetObject.GetComponent<CharacterController>(), GetComponent<Collider>());
        }
    }

    public void UpdateInteraction(GameObject targetObject, bool isDashing)
    {
        if (isDashing)
        {
            Physics.IgnoreCollision(targetObject.GetComponent<CharacterController>(), GetComponent<Collider>());
            Destroy(gameObject);
        }
        else
        {
            targetObject.GetComponent<PlayerBehavior>().Hit();
        }
    }

    public void FinishInteraction(GameObject targetObject, bool isDashing)
    {
        if (isDashing)
        {
            Physics.IgnoreCollision(targetObject.GetComponent<CharacterController>(), GetComponent<Collider>(), false);
        }
    }
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
