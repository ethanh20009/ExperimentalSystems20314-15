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

        CompostSavedData save = BFSaveSystem.LoadClass("compostSave");
        highScore = save.getHighScore();
        Debug.Log("Loaded stuff");
        Debug.Log($"High score: {highScore}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        CompostSavedData data = new CompostSavedData(highScore);
        BFSaveSystem.SaveClass(data, "compostSave.txt");
        Debug.Log("Saved");
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
