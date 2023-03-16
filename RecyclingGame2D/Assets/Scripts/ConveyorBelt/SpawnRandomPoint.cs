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
    public GameObject liveHeartPrefab;
    public GameObject deadHeartPrefab;
    public GameObject gameOverPrefab;
    public GameObject gameRulesPrefab;
    public TMP_Text scoreValue;   

    private System.Random randomDecider = new System.Random();
    private int maxHearts = 5;
    private int hearts;
    private int score = 0;
    private bool gameOver = false;

    GameObject gameOverScreen;
    GameObject gameRulesScreen;

    // Start is called before the first frame update
    void Start()
    {
        createHearts();
        
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
        

        if (goOrStay < 2 )
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
        float[] itemsSpawnPositions = { -10.0f, -5.0f, 0.0f, 5.0f, 10.0f };

        //choosing random item
        string itemFrom = ""; //set later, this is the bin the item should actually go to
        string itemTypeName = "";
        string spriteLocationEnding = "";
        int goToBin = randomDecider.Next(binsLength);  // chooses where the item is spawned upon
        int itemType = randomDecider.Next(-7,6); // chooses whether the item is incorrect (-ve), correct(+ve) or bonus(0) // more incorrect than correct
        int itemIndex = randomDecider.Next(totalItemsPerBin); //chooses which item in the bin
        int offsetHeight = randomDecider.Next(10,13);

        if (itemType == 0)
        {
            //change to bonus later on
            itemFrom = bins[goToBin];
            itemTypeName = "heart";
            spriteLocationEnding = "/bonus/heartBonus.png";

        } 
        else if(itemType > 0) 
        {
            //correct(+ve)
            itemFrom = bins[goToBin];
            itemTypeName = "correct";            
            spriteLocationEnding = "/" + itemFrom + "/" + itemIndex + ".png";
        }
        else if (itemType < 0)
        {
            //incorrect(-ve)
            //if we want an incorrect(-ve) itemType, the offset(mod(bin.Length)) chooses which bin the sprite is from
            itemFrom = bins[(goToBin + randomDecider.Next(1, binsLength)) % binsLength];
            itemTypeName = "incorrect";
            spriteLocationEnding = "/" + itemFrom + "/" + itemIndex + ".png";
        }
        instantiateRecyclingItem(spriteLocationEnding, new Vector2(itemsSpawnPositions[goToBin], offsetHeight), itemTypeName);
    }
    
    public Sprite getRecyclingSprite(string FilePath)
    {
        // get the adjusted PipelPerUnit value, which has been adjusted and left in the meta file of the image
        string importedPixelsPerUnitLine = File.ReadLines(FilePath + ".meta").ElementAt(49);
        int importedPixelsPerUnit = Convert.ToInt32(importedPixelsPerUnitLine.Substring(importedPixelsPerUnitLine.LastIndexOf(' ') + 1));

        // load texture and create sprite from it
        Texture2D SpriteTexture = new Texture2D(2, 2);
        byte[] SpriteTextureData = File.ReadAllBytes(FilePath);
        SpriteTexture.LoadImage(SpriteTextureData);

        // pivot argument has been adjusted to fix clickable area of onMouseDown
        Sprite RecyclingSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0.5f, 0.5f), importedPixelsPerUnit, 0, SpriteMeshType.Tight); 

        return RecyclingSprite;

    }

    void instantiateRecyclingItem(string spriteLocationEnding, Vector2 itemsSpawnPosition, string itemTypeName)
    {
        string recyclingItemsPath = Application.dataPath + "/Sprites/conveyorBeltMinigame/RecyclingItems";
        GameObject recycleItem = Instantiate(RecyclingPrefab, itemsSpawnPosition, Quaternion.identity);
        recycleItem.GetComponent<SpriteRenderer>().sprite = getRecyclingSprite(recyclingItemsPath + spriteLocationEnding);
        recycleItem.GetComponent<Rigidbody2D>().drag = (float)(2.0 + (5.0 * Math.Exp(-(0.02 * (double)score)))); // drag starts at 7(=5+2) goes to 2 and about a score of 100. Equation  = 2+5e^(-(0.02*score)^2), change score to be lower
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
            gameOverScreen.SetActive(true);
            gameOverScreen.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = score.ToString();
            exitButton.SetActive(false);
            // for previous score, upload the number then just access
            // gameOverScreen.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().text = // previousValue;

            // the saving class crashes too much, so I decided against it
            //save();
            gameOver = true;
            StartCoroutine(waitExit());
        }
    }


    

    public void createHearts()
    {
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
        
        string previousHighScore = BFSaveSystem.LoadClass<String>("HS4");
        try
        {            
            int prevScore = Int32.Parse(previousHighScore);
            if (prevScore < score)
            {
                BFSaveSystem.SaveClass<String>(score.ToString(), "HS4");
            }
        }
        catch(FormatException)
        {
            // In this case the highscore is invalid anyway and so should be replaced
            try
            {
                BFSaveSystem.SaveClass<String>(score.ToString(), "HS4");
            }            
            catch (Exception e)
            {
                Debug.Log("Saving script crashed = " + e);
            }
        }
        
        catch(Exception e)
        {
            Debug.Log("Saving script crashed = " + e);
        }
    }

}
