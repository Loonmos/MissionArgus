using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainAbility : MonoBehaviour
{
    public PlayerMovement2DPlatformer playerMovement;
    public GameObject boots;
    public GameObject particles;
    public Animator anim;
    public DialogueSystem tut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.unlockedJump = true;
        boots.SetActive(false);
        particles.SetActive(false);
        anim.Play("Ability");
        tut.state = DialogueSystem.State.Jump1;
        gameObject.SetActive(false);
    }
}
