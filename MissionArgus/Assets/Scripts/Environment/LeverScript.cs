using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LeverScript : MonoBehaviour, IActivatable
{
    public bool isFlipped, canBeFlipped = false;
    private SpriteRenderer spriteRenderer;
    public float playerDistance;
    public Transform player;
    public Sprite unInteractable, Interactable, flipped;
    public GameObject iActivatable, lightsOff, lightsOn;
    public int leversNeeded;
    private int leversPulled;
    public GameObject itemLight;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unInteractable;
        itemLight.SetActive(false);
    }

    void Update()
    {
        if (isFlipped) return;
        playerDistance = (player.transform.position - transform.position).magnitude;

        if (playerDistance <= 2 && spriteRenderer.sprite != Interactable && canBeFlipped)
        {
            spriteRenderer.sprite = Interactable;
            itemLight.SetActive(true);
        }
        else if (playerDistance >= 2 && spriteRenderer.sprite != unInteractable && canBeFlipped)
        {
            spriteRenderer.sprite = unInteractable;
            itemLight.SetActive(false);
        }

        if (spriteRenderer.sprite == Interactable && Input.GetKeyDown(KeyCode.E) && canBeFlipped)
        {
            if (lightsOff != null && lightsOn != null)
            {
                lightsOff.SetActive(false);
                lightsOn.SetActive(true);
                itemLight.SetActive(false);
            }
            spriteRenderer.sprite = flipped;
            iActivatable.GetComponent<IActivatable>().Activate();
            isFlipped = true;
        }
    }

    void IActivatable.Activate()
    {
        leversPulled++;
        if (leversPulled >= leversNeeded)
        {
            canBeFlipped = true;
        }
    }
}
