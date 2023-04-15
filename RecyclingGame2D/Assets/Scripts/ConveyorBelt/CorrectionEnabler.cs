using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorrectionEnabler : MonoBehaviour
{
    Toggle CorrectionCheckbox;
    public Text m_Text;

    void Start()
    {
        CorrectionCheckbox = GetComponent<Toggle>();
        CorrectionCheckbox.onValueChanged.AddListener(delegate {
            ToggleValueChanged(CorrectionCheckbox);
        });
    }

    //Output the new state of the Toggle into Text when the user uses the Toggle
    void ToggleValueChanged(Toggle change)
    {
        GameObject.Find("Spawn_Point").GetComponent<SpawnRandomPoint>().correctionActive = CorrectionCheckbox.isOn;
    }
}
