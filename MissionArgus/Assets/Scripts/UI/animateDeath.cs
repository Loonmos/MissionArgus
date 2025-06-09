using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class animateDeath : MonoBehaviour
{
    public Image image;
    public List<Sprite> deathFrames;
    public float fps;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(playDeathAnim());
    }

    IEnumerator playDeathAnim()
    {
        image.gameObject.SetActive(true);
        for (int i = 0; i < deathFrames.Count; i++)
        {
            image.sprite = deathFrames[i];
            yield return new WaitForSeconds(fps / 60);
        }
    }
}
