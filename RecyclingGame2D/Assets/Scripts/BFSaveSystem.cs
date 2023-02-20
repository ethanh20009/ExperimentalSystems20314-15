using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class BFSaveSystem
{
    public static void SaveClass(CompostSavedData objectToSave, string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename + ".fun";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        bf.Serialize(fileStream, objectToSave);
        fileStream.Close();
    }

    public static CompostSavedData LoadClass(string filename)
    {
        string path = Application.persistentDataPath + "/" + filename + ".fun";
        if (!File.Exists(path))
        {
            return null;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Open);
        CompostSavedData objectRead = bf.Deserialize(fileStream) as CompostSavedData;
        return objectRead;
    }
}
