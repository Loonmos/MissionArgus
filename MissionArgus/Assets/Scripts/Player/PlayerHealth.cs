using System.Collections;
using System.Collections.Generic;
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
    public int currentHealth;
    private float animDelay = 0.2f;
    public float animTime;

    bool isDead = false;

    public float particleTime;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement2DPlatformer>();
    }

    public void Start()
    {
        playerMovement.enabled = true;

        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;

        deathScreen.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (currentHealth >= 100)
        {
            currentHealth = 100;
            healthbar.SetHealth(currentHealth);
        }

        if (currentHealth > 0)
        {
            deathScreen.SetActive(false);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthbar.SetHealth(currentHealth);
        }

        if (deathScreen.activeInHierarchy)
        {
            //Debug.Log("deathScreen is active in hierarchy");
        }
        else
        {
            //Debug.Log("deathScreen is not active in hierarchy");
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth > 0)
        {

        }
        else if (!isDead)
        {
            isDead = true;
            rb.velocity = new Vector2(0, 0);
            playerMovement.enabled = false;
            StartCoroutine(Death());
        }
    }

    public void AddHealth(int health)
    {
        currentHealth += health;
        healthbar.SetHealth(currentHealth);
        animPart.Play("Ability");
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

        yield return new WaitForSeconds(particleTime);

        animPart.SetBool("obsDeath", false);
        playerSprite.SetActive(true);
        playerMovement.enabled = true;
        saveLoad.LoadPosition();
        currentHealth = 100;
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
            deathScreen.SetActive(true);
        }
    }

    public void ResetAfterDeath()
    {
        StopCoroutine(Death());
        isDead = false;
        animPlayer.Play("Idle");
        playerMovement.enabled = true;
        currentHealth = 100;
        deathScreen.SetActive(false);
        saveLoad.LoadPosition();
        Debug.Log("RESET AFTER DEATH CALLED!!!!!!!!!");
    }
}
