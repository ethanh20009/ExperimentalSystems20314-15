using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class BFSaveSystem
{
    public static void SaveClass<T>(T objectToSave, string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename + ".fun";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        bf.Serialize(fileStream, objectToSave);
        fileStream.Close();
    }

    public static T LoadClass<T>(string filename) where T : class
    {
        string path = Application.persistentDataPath + "/" + filename + ".fun";
        if (!File.Exists(path))
        {
            Debug.Log("File not found");
            return default(T);
        }
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            T objectRead = bf.Deserialize(fileStream) as T;
            fileStream.Close();
            return objectRead;

        }
        catch
        {
            return null;
        }
    }
}
