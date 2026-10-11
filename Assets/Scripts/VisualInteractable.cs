using UnityEngine;

public class VisualInteractable : MonoBehaviour, IInteractable
{
    public Sense sense = Sense.SIGHT;
    public bool isInteractable = true;
    public Sense getSense()
    {
        return sense;
    }

    public void Interact()
    {
        if(isInteractable) {//TODO: dodati zvuk, mby efekt
            SetInteractable(false);
            GameStateManager.instance.ReceiveSight();
        }
    }

    public bool IsInteractable()
    {
        return isInteractable;
    }

    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }

}
