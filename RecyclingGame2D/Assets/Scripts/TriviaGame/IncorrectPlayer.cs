using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IncorrectPlayer : MonoBehaviour
{
    private VideoPlayer player;
    public GameObject image;


    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
        player = GetComponent<VideoPlayer>();
    }

    public void PlayRockIncorrect()
    {
        image.SetActive(true);
        player.Play();
        StartCoroutine(VideoPlay());
    }

    IEnumerator VideoPlay()
    {
        yield return new WaitForSeconds(2);
        image.SetActive(false);
    }
}
