using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnRandomPoint : MonoBehaviour
{
    public GameObject exitButton;

    public GameObject RecyclingPrefab;
    public GameObject correctionPrefab;
    public GameObject liveHeartPrefab;
    public GameObject deadHeartPrefab;
    public GameObject gameOverPrefab;
    public GameObject gameRulesPrefab;
    public TMP_Text scoreValue;
    public bool correctionActive = false; 
    public bool[] correctionPopupsActive = new bool[] {false, false, false}; // lenght of 3 is for the lenght of correctionPosition

    private Vector2[] correctionPosition = new Vector2[] { new Vector2(2.52f, 5.5f), new Vector2(-2.52f, 5.5f), new Vector2(-7.48f, 5.5f) };
    private GameObject[] correctionPopups = new GameObject[3]; // 3 is for the lenght of correctionPosition
    private System.Random randomDecider = new System.Random();
    private int maxHearts = 5;
    private int hearts;
    private int score = 0;
    private bool gameOver = false;
    
    

    public List<List<Sprite>> spriteList = new List<List<Sprite>>();
    public List<Sprite> food;
    public List<Sprite> general;
    public List<Sprite> glass;
    public List<Sprite> paper;
    public List<Sprite> plastic;
    public List<Sprite> bonus;
    public List<Sprite> bins;

    GameObject gameOverScreen;
    GameObject gameRulesScreen;

    // Start is called before the first frame update
    void Start()
    {
        createHearts();
        createCorrectionPopups();

        spriteList.Add(food);
        spriteList.Add(general);
        spriteList.Add(glass);
        spriteList.Add(paper);
        spriteList.Add(plastic);
        spriteList.Add(bonus);
        spriteList.Add(bins);

        gameOverScreen = Instantiate(gameOverPrefab, new Vector2(0, 0), Quaternion.identity);
        gameRulesScreen = Instantiate(gameRulesPrefab, new Vector2(0, 0), Quaternion.identity);
        gameOverScreen.SetActive(false);
        gameRulesScreen.name = "Loaded_Rule_Screen";
        Destroy(gameRulesScreen, 3);
    }

    
    void FixedUpdate()
    {
        int goOrStay;
        if (score < 100)
        {
            goOrStay = randomDecider.Next(180 - score);
        }
        else
        {
            goOrStay = randomDecider.Next(100);
        }
        

        if (goOrStay < 2 && gameOver == false)
        {            
            itemDecider();
        }
    }


    void itemDecider()
    {
        // the following lines could be automated, but decided not to, for time efficiency
        string[] bins = { "food", "general", "glass", "paper", "plastic" };        
        int binsLength = bins.Length; // just to stop repeated calls
        int totalItemsPerBin = 5; // number of items in each bin folder
        float[] itemsSpawnPositions = { -10.0f, -5.0f, 0.0f, 5.0f, 10.0f};

        //choosing random item
        string itemTypeName = "";
        int comeFromBin = randomDecider.Next(binsLength);  // chooses where the item is spawned upon
        int itemType = randomDecider.Next(-8,6); // chooses whether the item is incorrect (-ve), correct(+ve) or bonus(0) // more incorrect than correct
        int itemIndex = randomDecider.Next(totalItemsPerBin); //chooses which item in the bin
        int offsetHeight = randomDecider.Next(10,13);
        int spawnPositionInd = comeFromBin;

        if (itemType == 0)
        {
            //change to bonus later on
            spawnPositionInd = randomDecider.Next(binsLength);
            comeFromBin = 5;
            itemIndex = 0;
            itemTypeName = "heart";
        } 
        else if(itemType > 0) 
        {
            //correct(+ve)
            itemTypeName = "correct";
        }
        else if (itemType < 0)
        {
            //incorrect(-ve)
            //if we want an incorrect(-ve) itemType, the offset(mod(bin.Length)) chooses which bin the sprite is from
            spawnPositionInd = (comeFromBin + randomDecider.Next(1, binsLength)) % binsLength; 
            itemTypeName = "incorrect";
        }

        instantiateRecyclingItem(new int[] { comeFromBin, itemIndex }, new Vector2(itemsSpawnPositions[spawnPositionInd], offsetHeight), itemTypeName);
    }


    void instantiateRecyclingItem(int[] spriteLocationEnding, Vector2 itemsSpawnPosition, string itemTypeName)
    {
        GameObject recycleItem = Instantiate(RecyclingPrefab, itemsSpawnPosition, Quaternion.identity);
        recycleItem.GetComponent<SpriteRenderer>().sprite = spriteList[spriteLocationEnding[0]][spriteLocationEnding[1]];
        recycleItem.GetComponent<Rigidbody2D>().drag = (float)(2.0 + (5.0 * Math.Exp(-(0.02 * (double)score)))); // drag starts at 7(=5+2) goes to 2 and about a score of 100. Equation  = 2+5e^(-(0.02*score)^2), change score to be lower
        recycleItem.GetComponent<SelfDestruct>().spriteLocationEnding = spriteLocationEnding;
        recycleItem.name = itemTypeName;
    }

    public void plusHearts()
    {
        //only for heart bonuses
        if (hearts < maxHearts)
        {
            hearts++;
            GameObject.Find("Live_" + hearts).GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Dead_" + hearts).GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    IEnumerator waitExit()
    {
        yield return new WaitForSeconds(3);
        save();
        SceneManager.LoadScene(0);
    }


    public void minusHearts()
    {
        if (hearts > 0)
        {
            GameObject.Find("Live_" + hearts).GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Dead_" + hearts).GetComponent<SpriteRenderer>().enabled = true;
            hearts--;
        }

        if (hearts == 0 && !gameOver )
        {

            // giving user time to realise game is over
            disableGuide();
            gameOverScreen.SetActive(true);
            gameOverScreen.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = score.ToString();
            exitButton.SetActive(false);
            save();
            gameOver = true;            
            StartCoroutine(waitExit());
        }
    }


    

    public void createHearts()
    {
        // allows you to set however many hearts at the start of the game
        // Creates both live and dead hearts (dead hearts are disabled at the start)
        hearts = maxHearts;
        float xCoord = 11.7f;
        float yCoord = 6.0f;
        float yJump = 1.3f;
        for (int i = 1; i <= maxHearts; i++)
        {
            GameObject liveHeart = Instantiate(liveHeartPrefab, new Vector2(xCoord, yCoord - (i-1) * yJump ), Quaternion.identity);
            GameObject deadHeart = Instantiate(deadHeartPrefab, new Vector2(xCoord, yCoord - (i-1) * yJump), Quaternion.identity);
            liveHeart.name = "Live_"+i;
            deadHeart.name = "Dead_"+i;
            deadHeart.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void plusScore()
    {
        // updates score on screen        
        score++;
        scoreValue.text = score.ToString();
    }

    void save()
    {
        if (File.Exists(Application.persistentDataPath + "/HS4.fun"))
        {
            string previousHighScore = BFSaveSystem.LoadClass<string>("HS4");
            // to delete =======================
            //if (previousHighScore == null)
            //{
            //    previousHighScore = "0";
            //}
            //================================
            gameOverScreen.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = previousHighScore;
            if (Int32.Parse(previousHighScore) < score)
            {
                BFSaveSystem.SaveClass<string>(score.ToString(), "HS4");
            }
        }
        else
        {
            BFSaveSystem.SaveClass<string>(score.ToString(), "HS4");

        }
       
    }

    void disableGuide()
    {
        GameObject[] PopupScreens = GameObject.FindGameObjectsWithTag("PopupScreen");
        foreach (GameObject screen in PopupScreens)
        {
            if (screen != null)
            {
                screen.SetActive(false);
            }
        }
    }

    void createCorrectionPopups()
    {        
        for (int i = 0; i < correctionPosition.Length; i++)
        {
            GameObject correctionPopup = Instantiate(correctionPrefab, correctionPosition[i], Quaternion.identity);
            correctionPopup.name = "Correction_" + i;
            correctionPopup.SetActive(false);                       
            correctionPopups[i] = correctionPopup;
        }
    }

    public void showCorrection(int[] spriteLocationEnding)
    {
        if (correctionActive)
        {
            int corInd = 0;
            bool foundSpace = false;

            for (int i = 0; i < correctionPopupsActive.Length; i++)
            {
                if (foundSpace == false && correctionPopupsActive[i] == false)
                {
                    corInd = i;
                    foundSpace = true;
                }
            }

            correctionPopupsActive[corInd] = true;
            correctionPopups[corInd].SetActive(true);
            //correctionPopups[corInd].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = getRecyclingSprite(Application.dataPath + "/Sprites/conveyorBeltMinigame/RecyclingItems" + "/" + spriteLocationEnding[0] + "/" + spriteLocationEnding[1] + ".png");
            //correctionPopups[corInd].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = getRecyclingSprite(Application.dataPath + "/Sprites/conveyorBeltMinigame/" + spriteLocationEnding[0] + ".png"); // ATTENTION==================================
            correctionPopups[corInd].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteList[spriteLocationEnding[0]][spriteLocationEnding[1]];
            correctionPopups[corInd].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spriteList[6][spriteLocationEnding[0]];
            //recycleItem.GetComponent<SpriteRenderer>().sprite = spriteList[spriteLocationEnding[0]][spriteLocationEnding[1]];
        }

        
    }

    public void hideCorrection(string correctionName)
    {
        string[] nameSplit = correctionName.Split('_');
        correctionPopupsActive[Int32.Parse(nameSplit[nameSplit.Length - 1])] = false;
    }

}
