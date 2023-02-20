using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostGameState : MonoBehaviour
{
    private int score;
    private int highScore;
    void Start()
    {
        /*score = 0;
        try
        {
            highScore = BFSaveSystem.LoadClass<CompostSavedData>("compostSave").getHighScore();
            Debug.Log("Loaded stuff");
            Debug.Log($"High score: {highScore}");
        }
        catch{
            highScore = 0;
        }*/
        score = 0;

        CompostSavedData save = BFSaveSystem.LoadClass<CompostSavedData>("compostSave");
        if (save == null)
        {
            highScore = 0;
        }
        else
        {
            highScore = save.highScore;
            Debug.Log("Loaded stuff");
            Debug.Log($"High score: {highScore}");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        CompostSavedData data = new CompostSavedData(highScore);
        BFSaveSystem.SaveClass(data, "compostSave");
        Debug.Log("Saved");
        Debug.Log(data.highScore);
    }

    public void updateScore(int value)
    {
        this.score += value;
        if (highScore < score)
        {
            highScore = score;
        }
    }

}
