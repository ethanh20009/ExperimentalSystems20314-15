using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostGameState : MonoBehaviour
{
    private int score;
    private int highScore;
    void Start()
    {
        score = 0;
        try
        {
            highScore = SaveObjectBasic.DeserializeObjectFromFile<CompostSavedData>("compostSave.txt").getHighScore();
        }
        catch{
            highScore = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        CompostSavedData data = new CompostSavedData(highScore);
        SaveObjectBasic.SerializeObjectToFile<CompostSavedData>(data, "compostSave.txt");
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
