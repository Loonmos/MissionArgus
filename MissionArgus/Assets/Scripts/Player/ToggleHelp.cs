using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHelp : MonoBehaviour
{
    public GameObject helpUI;
    private bool toggled;
    
    void Start()
    {
        //helpUI.SetActive(true);
        toggled = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            toggled = !toggled;

            if (toggled)
            {
                helpUI.SetActive(true);
            }
            else if (!toggled)
            {
                helpUI.SetActive(false);
            }
        }
    }
}
