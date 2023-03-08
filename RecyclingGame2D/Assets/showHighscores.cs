using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showHighscores : MonoBehaviour
{
    [SerializeField] private Text t;
    
    public string filename;
    // Start is called before the first frame update
    void Start()
    {
        // t.text = bfss.LoadClass(filename);
        t.text = BFSaveSystem.LoadClass<string>(filename);
    }

    
}
