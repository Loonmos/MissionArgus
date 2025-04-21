using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    
    void Start()
    {
        pauseScreen.SetActive(false);
    }

    
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }
}
