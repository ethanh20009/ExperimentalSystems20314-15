using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class CSVObject
{
    public List<string> headers { get; set; }
    public List<List<string>> data { get; set; }
}
