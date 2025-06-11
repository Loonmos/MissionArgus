using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement2DPlatformer playerMovement;
    public SaveAndLoad saveLoad;
    public Rigidbody2D rb;
    public GameObject playerSprite;

    public Healthbar healthbar;
    public GameObject deathScreen;
    public Animator animPlayer;
    public Animator animPart;

    public int maxHealth = 100;
    public int minHealth = 0;
    public int currentHealth;
    private float animDelay = 0.2f;
    public float animTime;

    bool isDead = false;

    public float particleTime;

    public AllDialogue dialogue;

    public List<Sprite> damagePics;
    public List<AudioSource> damageSounds;
    public Image damageImage;
    public GameObject deathAnim;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement2DPlatformer>();
    }

    public void Start()
    {
        playerMovement.enabled = true;

        healthbar.SetMaxHealth(maxHealth);
        currentHealth = minHealth;

        deathAnim.SetActive(false);
        deathScreen.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (currentHealth <= minHealth)
        {
            currentHealth = minHealth;
            healthbar.SetHealth(currentHealth);
        }

        if (currentHealth < maxHealth)
        {
            deathScreen.SetActive(false);
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            healthbar.SetHealth(currentHealth);
        }

        if (Input.GetKeyDown(KeyCode.R) && !dialogue.isChasing)
        {
            isDead = true;
            rb.velocity = new Vector2(0, 0);
            playerMovement.enabled = false;
            GlassCrack(currentHealth);
            ObstacleDeath();
        }
        else if (Input.GetKeyDown(KeyCode.R) && dialogue.isChasing)
        {
            isDead = true;
            rb.velocity = new Vector2(0, 0);
            playerMovement.enabled = false;
            GlassCrack(currentHealth);
            dialogue.ChaseFail();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth += damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth < maxHealth)
        {

        }
        else if (!isDead && !dialogue.isChasing)
        {
            isDead = true;
            rb.velocity = new Vector2(0, 0);
            playerMovement.enabled = false;
            StartCoroutine(Death());
        }
        else if (!isDead && dialogue.isChasing)
        {
            isDead = true;
            rb.velocity = new Vector2(0, 0);
            playerMovement.enabled = false;
            dialogue.ChaseFail();
        }
        
        GlassCrack(currentHealth);
    }

    public void GlassCrack(int currentHealth)
    {
        if (currentHealth == minHealth)
        {
            damageImage.gameObject.SetActive(false);
            return;
        }

        //if (currentHealth == maxHealth)
        //{
        //    Sprite spriteToChange = damagePics[6];
        //    return;
        //}

        float stepSize = maxHealth / damagePics.Count + 1;
        Debug.Log(stepSize);

        for (int i = 0; i < damagePics.Count; i++)
        {
            if (currentHealth <= stepSize + (stepSize * i))
            {
                Sprite spriteToChange = damagePics[damagePics.Count - 1 - i];
                if (damageImage.sprite != spriteToChange || !damageImage.gameObject.activeSelf)
                {
                    damageImage.gameObject.SetActive(true);
                    damageImage.sprite = spriteToChange;
                    damageSounds[damagePics.Count - 1 - i].Play();
                }
                return;
            }
        }

        damageImage.gameObject.SetActive(false);
    }


    public void AddHealth(int health)
    {
        currentHealth -= health;
        healthbar.SetHealth(currentHealth);
        animPart.Play("Ability");
        GlassCrack(currentHealth);
    }

    void HitAnim()
    {
        //anim.Play("GetHit");
    }

    public void ObstacleDeath()
    {
        StartCoroutine(ObDeath());
    }

    IEnumerator ObDeath()
    {
        animPart.SetBool("obsDeath", true);
        playerSprite.SetActive(false);
        playerMovement.rb.velocity = new Vector2(0, 0);
        playerMovement.enabled = false;

        //yield return new WaitForSeconds(particleTime);

        deathAnim.SetActive(true);
        yield return new WaitForSeconds(2);

        deathAnim.SetActive(false);
        animPart.SetBool("obsDeath", false);
        playerSprite.SetActive(true);
        playerMovement.enabled = true;
        saveLoad.LoadPosition();
        currentHealth = minHealth;
        GlassCrack(currentHealth);
        isDead = false;
    }

    IEnumerator Death()
    {
        animPlayer.speed = 1;
        animPlayer.Play("Death");
        playerMovement.rb.velocity = new Vector2(0, 0);
        playerMovement.enabled = false;
        Debug.Log("turned movement off");

        yield return new WaitForSeconds(animTime);

        if (isDead)
        {
            StartCoroutine(deathSequence());
        }
    }

    public void ResetAfterDeath()
    {
        StopCoroutine(Death());
        isDead = false;
        animPlayer.Play("Idle");
        playerMovement.enabled = true;
        currentHealth = minHealth;
        GlassCrack(currentHealth);
        deathAnim.SetActive(false);
        deathScreen.SetActive(false);
        saveLoad.LoadPosition();
        Debug.Log("RESET AFTER DEATH CALLED!!!!!!!!!");
    }

    IEnumerator deathSequence()
    {
        deathAnim.SetActive(true);
        yield return new WaitForSeconds(2);

        deathAnim.SetActive(false);
        playerSprite.SetActive(true);
        animPlayer.Play("Idle");
        playerMovement.enabled = true;
        saveLoad.LoadPosition();
        currentHealth = minHealth;
        GlassCrack(currentHealth);
        isDead = false;
    }
}
