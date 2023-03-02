using System;
using System.IO;
using System.Collections.Generic;

///*
// * filename as argument
// *     maybe code around .csv being typed or not
// * read csv
// * list of strings headers
// * list of records (records are lists (records contain strings))
// * add both of the above as attributes to an object
// * return the object
// */

public class ReadCSV
{
    public CSVObject Read(string fileName)
    {
        CSVObject csvObject = new CSVObject();
        csvObject.headers = new List<string>();
        csvObject.data = new List<List<string>>();

        string line;
        bool isFirstLine = true;

        StreamReader file = new StreamReader(fileName);
        while ((line = file.ReadLine()) != null)
        {
            string[] parts = line.Split(',');

            if (isFirstLine)
            {
                foreach (string header in parts)
                {
                    csvObject.headers.Add(header);
                }
                isFirstLine = false;
            }
            else
            {
                List<string> record = new List<string>();
                foreach (string part in parts)
                {
                    record.Add(part);
                }
                csvObject.data.Add(record);
            }
        }
        file.Close();

        return csvObject;
    }
}


//class Program
//{
//    static void Main(string[] args)
//    {
//        CSVObject csvObject = ReadCSV.Read("Data.csv");

//        Console.WriteLine("Headers:");
//        foreach (string header in csvObject.headers)
//        {
//            Console.Write(header + "\t");
//        }
//        Console.WriteLine();

//        Console.WriteLine("Data:");
//        foreach (List<string> record in csvObject.data)
//        {
//            foreach (string field in record)
//            {
//                Console.Write(field + "\t");
//            }
//            Console.WriteLine();
//        }
//    }
//}