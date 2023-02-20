using System;
using System.IO;
using Newtonsoft.Json;

class SaveObjectBasic
{
    static void Main(string[] args)
    {
        // create an object of type objectClass
        var objectIn = new objectClass
        {
            attributeStr = "Ten",
            attributeInt = 10
        };

        string fileName = "filename.txt";

        SerializeObjectToFile(objectIn, fileName);

        objectClass objectOut;
        try
        {
            objectOut = DeserializeObjectFromFile<objectClass>(fileName);
        }
        catch (Exception ex)
        {
            objectOut = default(objectClass);
        }

        if (objectOut == null)
        {
            Console.WriteLine("Object is null");
        }
        else
        {
            Console.WriteLine(objectOut.attributeInt + " " + objectOut.attributeStr);
        }
    }

    public static void SerializeObjectToFile<T>(T obj, string fileName)
    {
        string jsonString = JsonConvert.SerializeObject(obj);
        File.WriteAllText(fileName, jsonString);
    }

    public static T DeserializeObjectFromFile<T>(string fileName)
    {
        string jsonString = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}

class objectClass
{
    public string attributeStr { get; set; }
    public int attributeInt { get; set; }
}
