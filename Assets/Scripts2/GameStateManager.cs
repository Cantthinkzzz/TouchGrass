using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager instance;
    public bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }
    
    public void TogglePause()
    {
        if(isPaused) {
            Time.timeScale = 1;
            isPaused = false;
        }
        else {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
