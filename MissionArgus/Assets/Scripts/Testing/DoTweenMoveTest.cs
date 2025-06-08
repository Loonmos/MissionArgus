using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenMoveTest : MonoBehaviour
{
    public Transform movePos;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(movePos.position, 5.0f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        //aaa
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
