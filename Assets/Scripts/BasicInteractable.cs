using UnityEngine;

public class BasicInteractable : MonoBehaviour, IInteractable
{
    public Sense sense;

    public int GetIndex()
    {
        return 0;
    }

    public Sense getSense()
    {
        return sense;
    }

    public bool Interact()
    {
        Debug.Log("Clicked!");
        return true;
    }

    public bool IsInteractable()
    {
        return true;
    }

    public void SetInteractable(bool value)
    {
        return;
    }

    void IInteractable.Interact()
    {
        Debug.Log("What are you doing here?");
    }
}
