using UnityEngine;

public class AppleInteractable : MonoBehaviour, IInteractable
{
    public Sense sense = Sense.TASTE;
    public bool isInteractable = false;

    public Sprite bittenApple;
    public AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            this.gameObject.GetComponent<SpriteRenderer>().sprite = bittenApple;
            if(audioClip!= null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
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
