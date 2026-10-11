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
        Fog.Instance.SetFog(amount);

        if (amount == 0.0)
        {
            Fog.Instance.Enabled = false;
        }
    }


    public void makeInteractablesObvious(GameObject gameObject)
    {
        //dobije se objekt s sprite rendererom, iinteractable i colliderom
        //ideja je da je objekt nekako vise ocit/vidjiv nakon sto se aktivira
        //no pressure, we can do this
    }

    public void PassTheSaturation(float amount)
    {
        Monochrome.Instance.SetColor(amount);
    }

    public void PassTheNoise(float amount)
    {
        //todo: smanjiti zvuk na mikseru
        //ako imamo muhe/pcele/whatever mozemo ubiti jednu svakim pozivom
    }





}
