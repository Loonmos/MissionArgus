using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeTransparency : MonoBehaviour
{
    public Image image;
    public PlayerHealth playerHealth;
    private int health;
    public AudioSource breathingSound;

    void Start()
    {

    }

    void Update()
    {
        if (playerHealth.currentHealth == 100)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            breathingSound.volume = 0;
        }
        else if (playerHealth.currentHealth == 90)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.1f);
            breathingSound.volume = 0.01f;
        }
        else if (playerHealth.currentHealth == 80)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.2f);
            breathingSound.volume = 0.02f;
        }
        else if (playerHealth.currentHealth == 70)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.3f);
            breathingSound.volume = 0.03f;
        }
        else if (playerHealth.currentHealth == 60)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.4f);
            breathingSound.volume = 0.04f;
        }
        else if (playerHealth.currentHealth == 50)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
            breathingSound.volume = 0.05f;
        }
        else if (playerHealth.currentHealth == 40)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.6f);
            breathingSound.volume = 0.06f;
        }
        else if (playerHealth.currentHealth == 30)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
            breathingSound.volume = 0.07f;
        }
        else if (playerHealth.currentHealth == 20)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.8f);
            breathingSound.volume = 0.08f;
        }
        else if (playerHealth.currentHealth == 10)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.9f);
            breathingSound.volume = 0.09f;
        }
        else if (playerHealth.currentHealth == 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            breathingSound.volume = 0.1f;
        }
    }
}
