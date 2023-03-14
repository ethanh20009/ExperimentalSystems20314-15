using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompostGameState : MonoBehaviour
{

    private int score;
    private int highScore;
    private float timeLeft;

    private Vector3 spawnPosition;
    [SerializeField]
    private float spawnRange = 1f;


    [SerializeField]
    private float roundTime = 10f; //Round time in seconds

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject foodItemPrefab;


    public List<Sprite> CompostableItems;
    public List<Sprite> NonCompostableItems;
    
    
    void Start()
    {
        spawnPosition = spawnPoint.position;
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
        scoreText.text = score.ToString();

        //Time Setup
        timeLeft = roundTime;
        timeText.text = "Time left: " + ((int)timeLeft).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = "Time left: " + ((int)timeLeft).ToString();

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
        scoreText.text = score.ToString();
    }

    public void spawnNewItem()
    {
        spawnPosition = spawnPoint.position;
        spawnPosition.x += Random.Range(-spawnRange, spawnRange);
        Instantiate(foodItemPrefab, spawnPosition, Quaternion.Euler(0,0,0));
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 left = spawnPoint.position;
        Vector3 right = spawnPoint.position;

        left.x -= spawnRange;
        right.x += spawnRange;
        Gizmos.DrawLine(left, right);
    }

}
