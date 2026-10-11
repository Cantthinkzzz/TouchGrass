using UnityEngine;

public class HoverChecker : MonoBehaviour
{
    public Texture2D touchTexture;
    public Texture2D sightTexture;
    public Texture2D hearingTexture;
    public Texture2D smellTexture;
    public Texture2D tasteTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public bool isCursorNormal = true;
    public Vector2 hotSpot = Vector2.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if(interactable != null && interactable.IsInteractable()==true)
                {
                    //Debug.Log("Hovering!");
                    Sense sense = interactable.getSense();
                    if (isCursorNormal)
                    {
                        Cursor.SetCursor(getSenseTexture(sense), hotSpot, cursorMode);
                        isCursorNormal = false;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        interactable.Interact();
                    }

                }
            else
            {
                if (!isCursorNormal)
                {
                    
                    Cursor.SetCursor(null, hotSpot, cursorMode);
                    isCursorNormal = true;
                }
            }
            }
    }

    public Texture2D getSenseTexture(Sense sense)
    {
        if(sense == Sense.SIGHT) return sightTexture;
        else if(sense == Sense.HEARING) return hearingTexture;
        else if(sense == Sense.SMELL) return smellTexture;
        else if(sense == Sense.TOUCH) return touchTexture;
        else return tasteTexture;
    }


    
}
