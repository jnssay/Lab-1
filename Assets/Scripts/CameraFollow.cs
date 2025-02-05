using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform mario;
    public float smoothSpeed = 5f;
    public float leftLimit = -5f;
    public float rightLimit = -5f;

    private float offsetX;

    void Start()
    {

        offsetX = transform.position.x - mario.position.x;
    }

    void LateUpdate()
    {
        if (mario == null) return;


        float targetX = mario.position.x + offsetX;


        targetX = Mathf.Clamp(targetX, leftLimit, rightLimit);


        transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), smoothSpeed * Time.deltaTime);
    }
}
