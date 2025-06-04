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

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        patrolStart = gameObject.transform;

        tween = transform.DOMove((patrolEnd.position), botSpeed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).OnStepComplete(() => FlipSprite());
    }

    void FlipSprite()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
