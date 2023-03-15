using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;

public class compostTests
{

    [SetUp]
    public void Setup()
    {
         EditorSceneManager.OpenScene("Assets\\Scenes\\CompostMinigame.unity");
    }

    [Test]
    public void testGameManager()
    {
        CompostGameState cgs = GameObject.FindObjectOfType<CompostGameState>();
        Assert.NotNull(cgs);
        int score = cgs.score;
        cgs.updateScore(1);
        int scoreNew = cgs.score;
        Assert.Less(score, scoreNew);
    }

}



