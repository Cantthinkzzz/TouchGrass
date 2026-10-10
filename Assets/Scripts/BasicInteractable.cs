using UnityEngine;

public class BasicInteractable : MonoBehaviour, IInteractable
{
    public bool Interact()
    {
        Debug.Log("Clicked!");
        return true;
    }
}
