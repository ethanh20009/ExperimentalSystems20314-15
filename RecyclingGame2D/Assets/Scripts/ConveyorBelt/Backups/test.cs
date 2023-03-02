using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using System.Linq; 
using System; 



public class test : MonoBehaviour
{
    //private static IMG2Sprite _instance;
    // Start is called before the first frame update
    void Start()
    {
        string recyclingItems_Path = Application.dataPath + "/Sprites/conveyorBeltMinigame/RecyclingItems/general/1.png";
        //gameObject.GetComponent<SpriteRenderer>().sprite = LoadNewSprite(recyclingItems_Path);
        gameObject.GetComponent<SpriteRenderer>().sprite = getRecyclingSprite(recyclingItems_Path);

        var rnd = new System.Random(); // random seed generator // if it don't work use Random.Range();
        int goToBin = rnd.Next(5);  // chooses where the item is spawned upon
        int itemType = rnd.Next(-5, 6); // chooses whether the item is incorrect (-ve), correct(+ve) or bonus(6)
        int itemIndex = rnd.Next(3); //chooses which item in the bin
        int offset = rnd.Next(0,5); // if we want an incorrect(-ve) itemType, the offset (mod(bin.Length)) chooses which bin the sprite is from 

        Debug.Log("random numbers = og " + goToBin + ", -ve?? " + itemType + "," + itemIndex + "," + offset + ", offset "+ (goToBin + rnd.Next(1, 5)) % 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

   
    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 300.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {
        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference
        Texture2D SpriteTexture = LoadTexture(FilePath);
        Debug.Log("Size is " + SpriteTexture.width + " by " + SpriteTexture.height);
        //SpriteTexture.Resize(600, 600);
        //Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, 400, 400), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);
        //TextureImporter importer = (TextureImporter) AssetImporter.GetAtPath(FilePath);
        //string line = File.ReadLines(FilePath + ".meta").Skip(49).Take(1).First();

        string importedPixelsPerUnitLine = File.ReadLines(FilePath + ".meta").ElementAt(49);
        int importedPixelsPerUnit = Convert.ToInt32(importedPixelsPerUnitLine.Substring(importedPixelsPerUnitLine.LastIndexOf(' ') + 1));
        //int numVal = Int32.Parse("-105");
        Debug.Log("PPU = " + importedPixelsPerUnit + "+");
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), importedPixelsPerUnit, 0, spriteType);
        //Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect());
        return NewSprite;
    }

    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    public Sprite getRecyclingSprite(string FilePath)
    {
        // get the adjusted PipelPerUnit value, which has been adjusted and left in the meta file of the image
        string importedPixelsPerUnitLine = File.ReadLines(FilePath + ".meta").ElementAt(49);
        int importedPixelsPerUnit = Convert.ToInt32(importedPixelsPerUnitLine.Substring(importedPixelsPerUnitLine.LastIndexOf(' ') + 1));

        // load texture and create sprite from there
        Texture2D SpriteTexture = new Texture2D(2, 2);
        byte[] SpriteTextureData = File.ReadAllBytes(FilePath);
        SpriteTexture.LoadImage(SpriteTextureData);

        Sprite RecyclingSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), importedPixelsPerUnit, 0, SpriteMeshType.Tight);

        return RecyclingSprite;

    }
}
