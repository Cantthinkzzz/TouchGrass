using UnityEngine;

public class HearingInteractable : MonoBehaviour, IInteractable
{
    public Sense sense = Sense.HEARING;
    public bool isInteractable = true;
    public int index;

    public int GetIndex()
    {
        return index;
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
            GameStateManager.instance.ReceiveHearing();
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
