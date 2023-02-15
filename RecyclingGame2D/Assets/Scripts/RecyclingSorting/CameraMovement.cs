using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 0.5f;
    [SerializeField]
    public float backgroundParallax = 0.5f;
    [SerializeField]
    public Material tilingMat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraVel = new Vector3();
        if (Input.GetKey(KeyCode.D))
        {
            cameraVel.x += 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraVel.x -= 1;
        }
        cameraVel.Normalize();
        cameraVel *= speed * Time.deltaTime;
        gameObject.transform.position += cameraVel;

        tilingMat.SetTextureOffset(Shader.PropertyToID("_NormalMap"), new Vector2(gameObject.transform.position.x * backgroundParallax, gameObject.transform.position.y * backgroundParallax));
        tilingMat.mainTextureOffset = new Vector2(gameObject.transform.position.x * backgroundParallax, gameObject.transform.position.y * backgroundParallax);

    }
}
