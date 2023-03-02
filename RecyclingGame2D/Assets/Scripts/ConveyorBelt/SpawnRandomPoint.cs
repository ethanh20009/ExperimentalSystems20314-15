using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

using System.IO;
using System.Linq;
using System;

public class SpawnRandomPoint : MonoBehaviour
{
    //public List<GameObject> ItemsToRecycle;
    public System.Random randomDecider = new System.Random();
    public GameObject RecyclingPrefab;


    // Start is called before the first frame update
    void Start()
    {
        itemDecider();
    }

    
    void FixedUpdate()
    {
        //var goOrStayDecider = new System.Random();
        int goOrStay = randomDecider.Next(200);

        if (goOrStay == 100)
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
        int goToBin = randomDecider.Next(binsLength);  // chooses where the item is spawned upon
        int itemType = randomDecider.Next(-5,6); // chooses whether the item is incorrect (-ve), correct(+ve) or bonus(0)
        int itemIndex = randomDecider.Next(totalItemsPerBin); //chooses which item in the bin
        int offset = randomDecider.Next(1, binsLength); // if we want an incorrect(-ve) itemType, the offset (mod(bin.Length)) chooses which bin the sprite is from 
        int offsetHeight = randomDecider.Next(6,10);

        if (itemType == 0)
        {
            //change to bonus later on
            itemFrom = bins[goToBin];
            itemTypeName = "correct";

        } 
        else if(itemType > 0) 
        {
            //correct(+ve)
            itemFrom = bins[goToBin];
            itemTypeName = "correct";
        }
        else if (itemType < 0)
        {
            //incorrect(-ve)
            itemFrom = bins[(goToBin + randomDecider.Next(1, binsLength)) % binsLength];
            itemTypeName = "incorrect";
        }

        string objectName = itemFrom + "_" + itemIndex;
        string spriteLocationEnding = "/" + itemFrom + "/" + itemIndex + ".png";

        instantiateRecyclingItem(objectName, spriteLocationEnding, new Vector2(itemsSpawnPositions[goToBin], offsetHeight), itemTypeName);

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

    void instantiateRecyclingItem(string objectName, string spriteLocationEnding, Vector2 itemsSpawnPosition, string itemTypeName, float dragFactor = 6.0f)
    {
        string recyclingItemsPath = Application.dataPath + "/Sprites/conveyorBeltMinigame/RecyclingItems";
        GameObject recycleItem = Instantiate(RecyclingPrefab, itemsSpawnPosition, Quaternion.identity);
        recycleItem.name = itemTypeName;
        recycleItem.GetComponent<SpriteRenderer>().sprite = getRecyclingSprite(recyclingItemsPath + spriteLocationEnding);

    }

}
