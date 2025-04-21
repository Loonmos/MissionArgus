using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject UIPlayer;
    public GameObject UIScreen;
    public GameObject UIEnd;
    public GameObject player;
    public PlayerMovement2DPlatformer playerMovement;
    public GameObject cam1;
    public GameObject cam2;
    public Animator anim;
    public float animTime;

    private void Start()
    {
        UIEnd.SetActive(false);
    }

    public void EndOfGame()
    {
        UIPlayer.SetActive(false);
        UIScreen.SetActive(false);
        player.transform.SetParent(transform);
        cam1.SetActive(false);
        cam2.SetActive(true);
        playerMovement.enabled = false;
        StartCoroutine(EndingAnimation());
    }

    private IEnumerator EndingAnimation()
    {
        anim.Play("Pod");

        yield return new WaitForSeconds(animTime);

        UIEnd.SetActive(true);
    }
}
