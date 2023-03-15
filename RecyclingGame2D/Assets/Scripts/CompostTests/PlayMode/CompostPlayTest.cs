using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;

public class CompostPlayTest
{
    [OneTimeSetUp]
    public void LoadScene()
    {
        //Load Compost Minigame Scene to Test
        EditorSceneManager.LoadScene("CompostMinigame");
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator testCollision()
    {
        //Get Game manager component and check if initialised correctly
        CompostGameState cgs = GameObject.FindObjectOfType<CompostGameState>();
        Assert.NotNull(cgs);

        //Get the Starting score (0)
        int score = cgs.score;

        //Find starting compostable item and bin
        CompostItem item = GameObject.FindObjectOfType<CompostItem>();
        compostBinScript bin = GameObject.FindObjectOfType<compostBinScript>();

        //Enforce item is compostable and force collision with bin
        item.isCompostable = true;
        item.transform.position = bin.transform.position;

        //Wait one second for unity's physics engine to detect and handle collision, calling our functions
        yield return new WaitForSeconds(1f);

        //Check if score was successfully increased as a result of the collision
        int newScore = cgs.score;
        Assert.Less(score, newScore);
    }

}
