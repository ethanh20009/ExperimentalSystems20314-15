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
        EditorSceneManager.LoadScene("CompostMinigame");
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator testCollision()
    {
        CompostGameState cgs = GameObject.FindObjectOfType<CompostGameState>();
        Assert.NotNull(cgs);
        int score = cgs.score;
        CompostItem item = GameObject.FindObjectOfType<CompostItem>();
        compostBinScript bin = GameObject.FindObjectOfType<compostBinScript>();
        item.isCompostable = true;
        item.transform.position = bin.transform.position;
        yield return new WaitForSeconds(1f);
        int newScore = cgs.score;
        Assert.Less(score, newScore);
    }

}
