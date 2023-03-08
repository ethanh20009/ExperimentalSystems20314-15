using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public string[] Objectives = new string[] { "Toilet_Roll","new_glass","Brick","Spray_bottle" };
    SpriteChanger sc;
    ObjectSpawner os;
    public int Score = 0;
    public string collisionObject = "";

    void Start()
    {
        sc = GameObject.Find("Objective").GetComponent<SpriteChanger>();
        System.Random rand = new System.Random();
        int index = rand.Next(Objectives.Length);
        sc.index = index;
        collisionObject = Objectives[index];
    }

    void Update()
    {
        sc = GameObject.Find("Objective").GetComponent<SpriteChanger>();
        os = GameObject.Find("GameObject").GetComponent<ObjectSpawner>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision");
        if (other.gameObject.name == collisionObject + "(Clone)")
        {
            Debug.Log("correct");
            Score += 1;
            Destroy(other.gameObject);
            os.objects_created = 0;
            System.Random rand = new System.Random();
            int index = rand.Next(Objectives.Length);
            sc.index = index;
            collisionObject = Objectives[index];
        }
    }
}
