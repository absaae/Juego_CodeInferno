using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float speed = 1.0f;
    public float floatStrength = 8f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * floatStrength;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
