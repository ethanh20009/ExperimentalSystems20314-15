using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

using System.IO;
using System.Linq;
using System;
using TMPro;

public class SpawnRandomPoint : MonoBehaviour
{
    //public List<GameObject> ItemsToRecycle;    
    public GameObject RecyclingPrefab;
    public GameObject liveHeartPrefab;
    public GameObject deadHeartPrefab;
    public TMP_Text scoreValue;
    [SerializeField] GameObject _GameOver;
    [SerializeField] GameObject _ExitButton;
    [SerializeField] GameObject _ObjectsOnScreen;

    private System.Random randomDecider = new System.Random();
    private int maxHearts = 6;
    private int hearts;
    private int score = 0;
    //private GameObject[] cubes = new GameObject[];
    //private GameObject[] heartObjects;


    // Start is called before the first frame update
    void Start()
    {
        createHearts();
    }

    
    void FixedUpdate()
    {
        //var goOrStayDecider = new System.Random();
        int goOrStay = randomDecider.Next(115 - score );

        if (goOrStay == 0)
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
        // remember to return nameItem, itemType and sprite
        // var rnd = new System.Random(); // random seed generator // if it don't work use Random.Range();

        // the following lines could be automated, but decided not to, for time efficiency
        string[] bins = { "food", "general", "glass", "paper", "plastic" };        
        int binsLength = bins.Length; // just to stop repeated calls
        int totalItemsPerBin = 3; // number of items in each bin folder
        float[] itemsSpawnPositions = { -10.0f, -5.0f, 0.0f, 5.0f, 10.0f };

        //string recyclingItemsPath = Application.dataPath + "/Sprites/conveyorBeltMinigame/RecyclingItems";
        //float offPosition = -0.5f;
        //Vector2[] itemsSpawnPositions = { new Vector2(-10 + offPosition, 6), new Vector2(-5 + offPosition, 6 ), new Vector2(0 + offPosition, 6), new Vector2(5 + offPosition, 6), new Vector2(-10 + offPosition, 6) };

        //choosing random item
        string itemFrom = ""; //set later, this is the bin the item should actually go to
        string itemTypeName = "";
        string spriteLocationEnding = "";
        int goToBin = randomDecider.Next(binsLength);  // chooses where the item is spawned upon
        int itemType = randomDecider.Next(-7,6); // chooses whether the item is incorrect (-ve), correct(+ve) or bonus(0) // more incorrect than correct
        int itemIndex = randomDecider.Next(totalItemsPerBin); //chooses which item in the bin
        int offset = randomDecider.Next(1, binsLength); // if we want an incorrect(-ve) itemType, the offset (mod(bin.Length)) chooses which bin the sprite is from 
        int offsetHeight = randomDecider.Next(9,13);

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


    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);

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

        if (hearts == 0)
        {
            // game over
            Debug.Log("game_over");

            //Liam added this
            _ExitButton.gameObject.SetActive(false);
            _GameOver.gameObject.SetActive(true);
            //_ObjectsOnScreen.gameObject.SetActive(false);

            String oldHS = BFSaveSystem.LoadClass<String>("HS4");
            try
            {
                int result = Int32.Parse(oldHS);
                if (result < score)
                {
                    BFSaveSystem.SaveClass<String>(score.ToString(), "HS4");
                }
            }
            catch (FormatException)
            {
                //In this case the highscore is invalid anyway and so should be replaced
                BFSaveSystem.SaveClass<String>(score.ToString(), "HS4");
            }   
            StartCoroutine(wait());
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
