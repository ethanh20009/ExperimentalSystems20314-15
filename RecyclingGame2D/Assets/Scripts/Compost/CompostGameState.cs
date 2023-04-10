using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompostGameState : MonoBehaviour
{

    public int score;
    private int highScore;
    private float timeLeft;

    private Vector3 spawnPosition;

    [Header("Game Properties")]

    [SerializeField]
    private float spawnRange = 1f;

    [SerializeField]
    private float roundTime = 60f; //Round time in seconds

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject foodItemPrefab;

    [Header("UI")]

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI timeText, currentItemText;



    private const string SAVELOCATION = "compostSave";
    public List<Sprite> CompostableItems;
    public List<Sprite> NonCompostableItems;
    
    
    void Start()
    {
        spawnPosition = spawnPoint.position;
        score = 0;

        CompostSavedData save = BFSaveSystem.LoadClass<CompostSavedData>(SAVELOCATION);
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
        timeLeft = 60f;
        timeText.text = "Time left: " + ((int)timeLeft).ToString();
        spawnNewItem();

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
        BFSaveSystem.SaveClass(data, SAVELOCATION);
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
        if (spawnPosition == null) { return; }
        spawnPosition = spawnPoint.position;
        spawnPosition.x += Random.Range(-spawnRange, spawnRange);
        GameObject item = Instantiate(foodItemPrefab, spawnPosition, Quaternion.Euler(0,0,0));
        updateItemText(item.GetComponent<CompostItem>().getItemName());
    }

    private void updateItemText(string text)
    {
        //Replace camel case with spaces
        string stringToUpdate = "";
        if (text == null) { this.currentItemText.text = stringToUpdate; return; }

        //Ignore first captial
        stringToUpdate += text[0];
        text = text.Substring(1);
        foreach(char c in text)
        {
            //Check if upper        
            if (char.IsUpper(c))
            {
                stringToUpdate += " ";
            }
            stringToUpdate += c;
        }
        

        this.currentItemText.text = stringToUpdate;
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
