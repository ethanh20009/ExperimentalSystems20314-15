using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject _GameOver;
    [SerializeField] GameObject _ExitButton;
    private int hearts = 5, maxHearts = 5;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void plusHearts()
    {
        //only for heart bonuses
        if (hearts < maxHearts)
        {
            hearts++;
        }
        Debug.Log("more hearts = " + hearts);
        // change hearts on screen
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);

    }


    public void minusHearts()
    {
        if (hearts > 1)
        {
            hearts--;
        } else if (hearts == 1)
        {
            // game over
            Debug.Log("game_over");

            //Liam added this
            _ExitButton.gameObject.SetActive(false);
            _GameOver.gameObject.SetActive(true);
            
            BFSaveSystem.SaveClass(score.ToString(), "HS4");
            StartCoroutine(wait());
        }
        Debug.Log("less hearts = " + hearts);
        // change hearts on screen
    }

    public void plusScore()
    {
        score++;
        Debug.Log("score = " + score);
        // change score on screen
    }
}
