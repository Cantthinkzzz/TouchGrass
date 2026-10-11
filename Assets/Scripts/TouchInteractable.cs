using UnityEngine;

public class TouchInteractable : MonoBehaviour, IInteractable
{
    public Sense sense = Sense.TOUCH;
    public bool isInteractable = true;
    public Sense getSense()
    {
        return sense;
    }

    public void Interact()
    {
        if(isInteractable) {//TODO: dodati zvuk, mby efekt
            SetInteractable(false);
            GameStateManager.instance.ReceiveTouch();
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
