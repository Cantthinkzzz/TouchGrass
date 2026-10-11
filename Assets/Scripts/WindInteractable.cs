using UnityEngine;

public class WindInteractable : MonoBehaviour, IInteractable
{
    public Sense sense = Sense.SMELL;
    public bool isInteractable = true;
    public int index;
    public AudioClip clip;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

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
            GameStateManager.instance.SmellTheWind();
            if(clip!= null)
            {
                source.clip = clip;
                source.Play();
            }
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
