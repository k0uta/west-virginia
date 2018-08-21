using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitInteraction : MonoBehaviour, IInteraction
{

    public void StartInteraction(GameObject targetObject, bool isDashing)
    {

    }

    public void UpdateInteraction(GameObject targetObject, bool isDashing)
    {
        targetObject.GetComponent<PlayerBehavior>().Hit();
    }

    public void FinishInteraction(GameObject targetObject, bool isDashing)
    {

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
