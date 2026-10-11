using JetBrains.Annotations;
using UnityEngine;

//CONTAINS ALL OF THE FUNCTIONS THAT CONNECT THE GAME STATE WITH SHADERS, PARTICLE EFFECTS, ...
public class CommunicationManager : MonoBehaviour
{
    public static CommunicationManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void PassTheFog(float amount)
    {
        //todo: passati float shaderu, 1 za punu maglu, 0 bez magle
        //ako je 0 mozda se moze iskljuciti magla u sumi ako smo na tome
        Debug.Log(amount);
        Fog.Instance.SetFog(amount);
    }


    public void makeInteractablesObvious(GameObject gameObject)
    {
        //dobije se objekt s sprite rendererom, iinteractable i colliderom
        //ideja je da je objekt nekako vise ocit/vidjiv nakon sto se aktivira
        //no pressure, we can do this
    }

    public void PassTheSaturation(float amount)
    {
        //todo: passati float shaderu, 0 za full grayscale, 1 za normalni saturation (ovo je za generalnu scenu)
    }

    public void PassTheNoise(float amount)
    {
        //todo: smanjiti zvuk na mikseru
        //ako imamo muhe/pcele/whatever mozemo ubiti jednu svakim pozivom
    }





}
