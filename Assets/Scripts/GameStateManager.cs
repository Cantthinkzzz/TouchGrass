using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private int totalSightInteractables = 5;
    private int sightCount = 0;

    [SerializeField]
    private int totalTouchInteractables = 4;
    private int touchCount = 0;
    [SerializeField]
    private int totalHearingInteractables = 3;
    private int hearingCount = 0;
    [SerializeField]
    private int totalSmellInteractables = 2;
    private int smellCount = 0;

    private bool smelledTheFlowers = false;
    private bool ozoneSmelled = false;

    //gameobject liste su samo za editor
    public List<GameObject> touchInteractableObjects;
    private List<IInteractable> touchInteractables;

    public List<GameObject> hearingInteractableObjects;
    private List<IInteractable> hearingInteractables;

    public GameObject flowerInteractableObject;
    private IInteractable flowerInteractable;
    public GameObject windInteractableObject;
    private IInteractable windInteractable;

    public GameObject appleInteractableObject;
    private IInteractable appleInteractable;



    public GameObject flowerPrefab;

    private Vector3 startingFlowerPosition;
    private Quaternion flowerRotation;
    private float flowerSpawnTime = 0.5f;

    public List<Image> images;
    public List<Sprite> touchSprites;
    public List<Sprite> hearingSprites;
    public List<Sprite> smellSprites;
    public List<Sprite> tasteSprites;
    public List<GameObject> crosses;



    public static GameStateManager instance;
    public bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        startingFlowerPosition = flowerInteractableObject.transform.position;
        flowerRotation = flowerInteractableObject.transform.rotation;

        //pretvorbe radimo na pocetku i onda ignoriramo game objecte (ne mozemo serializirati interface)
        touchInteractables = new List<IInteractable>();
        foreach (GameObject interactable in touchInteractableObjects)
        {
            touchInteractables.Add(interactable.GetComponent<IInteractable>());
        }

        hearingInteractables = new List<IInteractable>();
        foreach (GameObject interactable in hearingInteractableObjects)
        {
            hearingInteractables.Add(interactable.GetComponent<IInteractable>());
        }

        flowerInteractable = flowerInteractableObject.GetComponent<IInteractable>();
        windInteractable = windInteractableObject.GetComponent<IInteractable>();
        appleInteractable = appleInteractableObject.GetComponent<IInteractable>();



    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            UIManager.Instance.OnUnpause();
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            UIManager.Instance.OnPause();
        }
    }
    public void UnPauseTime()
    {
        Time.timeScale = 1;
        isPaused = false;
    }


    public void ReceiveSight()
    {
        sightCount += 1;
        CommunicationManager.instance.PassTheFog(GetFogValue());
        if (sightCount == totalSightInteractables)
        {
            foreach (IInteractable interactable in touchInteractables)
            {
                interactable.SetInteractable(true);
            }
            for (int i = 0; i < 4; i++)
            {
                images[i].sprite = touchSprites[i];
                crosses[i].SetActive(false);
            }
            Destroy(images[4].transform.parent.gameObject);
            //TODO: UPDATE UI, MOZDA JOS NES SA PRIJELAZOM ILI MAGLOM

            Monochrome.Instance.MakeSensoryColorful();
        }
    }

    //količina magle
    public float GetFogValue()
    {
        //kasnije možemo dodati custom vrijednosti npr 1, 0.8, 0.5, 0.4, 0.3, 0.0, zasad samo gledamo omjer
        Debug.Log(sightCount);
        Debug.Log(1.0f - sightCount / totalSightInteractables);
        return 1.0f - ((float)sightCount) / totalSightInteractables;
    }

    public void ReceiveTouch()
    {
        touchCount += 1;
        //grayscale vrijednost se prenosi preko communication managera!!
        CommunicationManager.instance.PassTheSaturation(GetSaturationValue());

        if (touchCount == totalTouchInteractables)
        {
            foreach (IInteractable interactable in hearingInteractables)
            {
                interactable.SetInteractable(true);
            }
            //swappanje spriteova
            for (int i = 0; i < 3; i++)
            {
                images[i].sprite = hearingSprites[i];
                crosses[i].SetActive(false);
            }
            Destroy(images[3].transform.parent.gameObject);
            //mislim da ne treba nista drugo napraviti nakon što su svi touchable objekti dodirnuti
        }
    }

    public float GetSaturationValue()
    {
        //kasnije možemo dodati custom vrijednosti npr 0, 0.3, 0.4, 0.5, 0.8, 1.0, zasad samo gledamo omjer
        return ((float)touchCount) / totalTouchInteractables;
    }

    public void ReceiveHearing()
    {
        hearingCount += 1;
        CommunicationManager.instance.PassTheNoise(GetNoiseValue());
        if (hearingCount == totalHearingInteractables)
        {
            flowerInteractable.SetInteractable(true);
            images[0].sprite = smellSprites[0];
            images[1].sprite = smellSprites[1];
            crosses[0].SetActive(false);
            crosses[1].SetActive(false);
            Destroy(images[2].transform.parent.gameObject);
            //također mislim da ne treba ništa drugo nakon što je sve poslušano
        }
    }

    public float GetNoiseValue()
    {
        //kasnije može biti custom, npr 1, 0.5, 0.3, 0.0
        return 1 - ((float)hearingCount) / totalHearingInteractables;
    }

    public void SmellAFlower()
    {
        smelledTheFlowers = true;
        StartCoroutine(FlowerSpread());
        //na kraju korutine ozon/vjetar/whatever postaje interactable
    }

    //ovo je zapravo ozon
    public void SmellTheWind()
    {
        ozoneSmelled = true;
        images[0].sprite = tasteSprites[0];
        crosses[0].SetActive(false);
        Destroy(images[1].transform.parent.gameObject);
        //prelazimo na okus
        appleInteractable.SetInteractable(true);


    }
    public void EatApple()
    {
        //TODO
        //ovo je zapravo prijelaz na ono sve s kišom
    }

    IEnumerator FlowerSpread()
    {
        //zasada spawna cvijece u tri vala
        List<SpriteRenderer> flowerWave = new();
        Color color;
        float t;

        for (int i = 1; i < 4; i++)
        {

            //možda zvuk pustiti
            flowerWave = new List<SpriteRenderer>
            {
                Instantiate(flowerPrefab, startingFlowerPosition + new Vector3(i, 0, 0), flowerRotation).GetComponent<SpriteRenderer>(),
                Instantiate(flowerPrefab, startingFlowerPosition + new Vector3(-i, 0, 0), flowerRotation).GetComponent<SpriteRenderer>(),
                Instantiate(flowerPrefab, startingFlowerPosition + new Vector3(0, 0, i), flowerRotation).GetComponent<SpriteRenderer>(),
                Instantiate(flowerPrefab, startingFlowerPosition + new Vector3(0, 0, -i), flowerRotation).GetComponent<SpriteRenderer>(),
                Instantiate(flowerPrefab, startingFlowerPosition + new Vector3(i*Mathf.Sqrt(2)/2, 0, i*Mathf.Sqrt(2)/2), flowerRotation).GetComponent<SpriteRenderer>(),
                Instantiate(flowerPrefab, startingFlowerPosition + new Vector3(i*Mathf.Sqrt(2)/2, 0, -i*Mathf.Sqrt(2)/2), flowerRotation).GetComponent<SpriteRenderer>(),
                Instantiate(flowerPrefab, startingFlowerPosition + new Vector3(-i*Mathf.Sqrt(2)/2, 0, i*Mathf.Sqrt(2)/2), flowerRotation).GetComponent<SpriteRenderer>(),
                Instantiate(flowerPrefab, startingFlowerPosition + new Vector3(-i*Mathf.Sqrt(2)/2, 0, -i*Mathf.Sqrt(2)/2), flowerRotation).GetComponent<SpriteRenderer>()
            };
            color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            foreach (SpriteRenderer renderer in flowerWave)
            {
                renderer.color = color;
            }
            t = 0.0f;
            while (t < 1)
            {
                color.a = Mathf.Clamp01(Mathf.Lerp(0.0f, 1.0f, t));
                foreach (SpriteRenderer renderer in flowerWave)
                {
                    renderer.color = color;
                }
                t = t + Time.deltaTime / flowerSpawnTime;
                yield return null;
            }




        }
        //postavljamo ozon/vjetar da je interactable
        windInteractable.SetInteractable(true);
        //dodati nes drugo s vjetrom?
    }

    //stavljanje krizica na overlay ovisno o poziciji koja je definirana u interactable
    public void CrossOff(int index)
    {
        crosses[index].SetActive(true);
    }




}
