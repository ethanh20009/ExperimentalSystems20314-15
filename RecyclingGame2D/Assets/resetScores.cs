using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetScores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BFSaveSystem.SaveClass<string>("0", "HS1");
        BFSaveSystem.SaveClass<string>("0", "HS2");
        BFSaveSystem.SaveClass<string>("0", "HS3");
        BFSaveSystem.SaveClass<string>("0", "HS4");
    }

  
}
