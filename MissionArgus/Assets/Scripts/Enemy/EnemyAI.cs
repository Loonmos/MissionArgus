using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAI : MonoBehaviour
{
    public float botSpeed;
    public Transform patrolEnd;
    private Transform patrolStart;
    Vector3 originalScale;
    public Tween tween;
    [SerializeField] Animator animator;
    //public float waitBeforeTurnaround;

    // Start is called before the first frame update
    void Start()
    {
        if(animator == null) animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        patrolStart = gameObject.transform;

        tween = transform.DOMove((patrolEnd.position), botSpeed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).OnStepComplete(() => FlipSprite());
    }

    void FlipSprite()
    {
        StartCoroutine(botWait());
    }

    IEnumerator botWait()
    {
        tween.Pause();
        animator.SetBool("isWalking", false);
        yield return new WaitForSeconds(2f);
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        tween.Play();
        animator.SetBool("isWalking", true);
    }
}
