using UnityEngine;

public class FlowerInteractable : MonoBehaviour, IInteractable
{
    public Sense sense = Sense.SMELL;
    public bool isInteractable = true;

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
            GameStateManager.instance.SmellAFlower();
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
