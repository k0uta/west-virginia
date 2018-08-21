using UnityEngine;

public interface IInteraction
{
    void StartInteraction(GameObject targetObject, bool isDashing);
    void UpdateInteraction(GameObject targetObject, bool isDashing);
    void FinishInteraction(GameObject targetObject, bool isDashing);
}