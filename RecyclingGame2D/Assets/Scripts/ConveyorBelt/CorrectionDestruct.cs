using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectionDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(waitToDeactivate());       
    }

    IEnumerator waitToDeactivate()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("Spawn_Point").GetComponent<SpawnRandomPoint>().hideCorrection(gameObject.name);
        gameObject.SetActive(false);
    }
}
