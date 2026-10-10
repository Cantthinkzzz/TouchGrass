using UnityEngine;

public class BasicInteractable : MonoBehaviour, IInteractable
{
    public Sense sense;

    public Sense getSense()
    {
        return sense;
    }

    public bool Interact()
    {
        Debug.Log("Clicked!");
        return true;
    }
}
