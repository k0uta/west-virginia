using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : StateMachineBehaviour
{

    [SerializeField]
    float dashSpeed = 80.0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject playerGameObject = animator.gameObject;

        Renderer renderer = playerGameObject.GetComponent<Renderer>();
        renderer.material.color = Color.yellow;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterController characterController = animator.gameObject.GetComponent<CharacterController>();
        Transform playerTransform = animator.gameObject.transform;

        Vector3 forwardMove = playerTransform.TransformDirection(Vector3.forward) * Time.deltaTime * dashSpeed;
        characterController.Move(forwardMove);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject playerGameObject = animator.gameObject;
        Renderer renderer = playerGameObject.GetComponent<Renderer>();
        renderer.material.color = Color.white;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
