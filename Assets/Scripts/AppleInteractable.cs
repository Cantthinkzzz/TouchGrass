using UnityEngine;

public class AppleInteractable : MonoBehaviour, IInteractable
{
    public Sense sense = Sense.TASTE;
    public bool isInteractable = false;

    public int GetIndex()
    {
        return 0;
    }

    public Sense getSense()
    {
        return sense;
    }

    public void Interact()
    {
        if(isInteractable) {//TODO: dodati zvuk, mby efekt
            SetInteractable(false);
            GameStateManager.instance.CrossOff(GetIndex());
            GameStateManager.instance.EatApple();
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
