using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostSavedData
{
    private int highScore;
    public CompostSavedData(int highScore)
    {
        this.highScore = highScore;
    }

    public int getHighScore()
    {
        return highScore;
    }

}
