using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnRandomPointBackup : MonoBehaviour
{   
    public GameObject RecyclingPrefab;
    public GameObject liveHeartPrefab;
    public GameObject deadHeartPrefab;
    public GameObject gameOverPrefab;
    public GameObject gameRulesPrefab;
    public TMP_Text scoreValue;

    private System.Random randomDecider = new System.Random();
    private int maxHearts = 3;
    private int hearts;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        createHearts();
        GameObject gameRulesScreen = Instantiate(gameRulesPrefab, new Vector2(0, 0), Quaternion.identity);
        Destroy(gameRulesScreen, 4);
    }

    
    void FixedUpdate()
    {
        //var goOrStayDecider = new System.Random();
        int goOrStay;
        if (score < 100)
        {
            goOrStay = randomDecider.Next(150 - score);
        }
        else
        {
            goOrStay = randomDecider.Next(50);
        }
        

        if (goOrStay < 2 )
        {
            itemDecider();
        }
    }

    void createItem(string objectName , string spriteLocationEnding, Vector2 itemsSpawnPosition, string itemTypeName, float dragFactor = 6.0f)
    {
        GameObject recyclingItem = new GameObject();
        string recyclingItemsPath = Application.dataPath + "/Sprites/conveyorBeltMinigame/RecyclingItems";
        recyclingItem.name = objectName;


        recyclingItem.AddComponent<SpriteRenderer>();
        recyclingItem.AddComponent<BoxCollider2D>();
        recyclingItem.AddComponent<SelfDestructConcept>();        
        recyclingItem.AddComponent<Rigidbody2D>();


        recyclingItem.transform.position = itemsSpawnPosition; // remember to change according to bin
        recyclingItem.GetComponent<Rigidbody2D>().drag = dragFactor; 
        recyclingItem.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Items");
        // something to change the sprite
        recyclingItem.GetComponent<SpriteRenderer>().sprite = getRecyclingSprite(recyclingItemsPath + spriteLocationEnding); //add specific

        Destroy(recyclingItem, 15); // remember to fix
        //ItemsToRecycle.Add(recyclingItem);
    }

    void itemDecider()
    {
        // the following lines could be automated, but decided not to, for time efficiency
        string[] bins = { "food", "general", "glass", "paper", "plastic" };        
        int binsLength = bins.Length; // just to stop repeated calls
        int totalItemsPerBin = 3; // number of items in each bin folder
        float[] itemsSpawnPositions = { -10.0f, -5.0f, 0.0f, 5.0f, 10.0f };

        //choosing random item
        string itemFrom = ""; //set later, this is the bin the item should actually go to
        string itemTypeName = "";
        string spriteLocationEnding = "";
        int goToBin = randomDecider.Next(binsLength);  // chooses where the item is spawned upon
        int itemType = randomDecider.Next(-7,6); // chooses whether the item is incorrect (-ve), correct(+ve) or bonus(0) // more incorrect than correct
        int itemIndex = randomDecider.Next(totalItemsPerBin); //chooses which item in the bin
        int offset = randomDecider.Next(1, binsLength); // if we want an incorrect(-ve) itemType, the offset (mod(bin.Length)) chooses which bin the sprite is from 
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
            //objectName = itemFrom + "_" + itemIndex;
            spriteLocationEnding = "/" + itemFrom + "/" + itemIndex + ".png";
        }
        else if (itemType < 0)
        {
            //incorrect(-ve)
            itemFrom = bins[(goToBin + randomDecider.Next(1, binsLength)) % binsLength];
            itemTypeName = "incorrect";
            //objectName = itemFrom + "_" + itemIndex;
            spriteLocationEnding = "/" + itemFrom + "/" + itemIndex + ".png";
        }

        //string objectName = itemFrom + "_" + itemIndex;
        //string spriteLocationEnding = "/" + itemFrom + "/" + itemIndex + ".png";

        instantiateRecyclingItem(spriteLocationEnding, new Vector2(itemsSpawnPositions[goToBin], offsetHeight), itemTypeName);

        //createItem(objectName , spriteLocationEnding, new Vector2(itemsSpawnPositions[goToBin], offsetHeight), itemTypeName);
        // name, sprite address, spawnpoint, itemType, dragFactor

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

    public void minusHearts()
    {
        if (hearts > 0)
        {
            GameObject.Find("Live_" + hearts).GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Dead_" + hearts).GetComponent<SpriteRenderer>().enabled = true;
            hearts--;
        }

        if (hearts == 0)
        {
            // giving user time to realise game is over
            GameObject gameOverScreen = Instantiate(gameOverPrefab, new Vector2(0,0), Quaternion.identity);            
            var delay = Task.Run(async () => {
                GameObject gameOverScreen = Instantiate(gameOverPrefab, new Vector2(0, 0), Quaternion.identity);                
                await Task.Delay(6000);                
            });

            SceneManager.LoadScene(0);
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
        score++;
        scoreValue.text = score.ToString();
        //Debug.Log("score = " + score);
        // change score on screen
    }

}
