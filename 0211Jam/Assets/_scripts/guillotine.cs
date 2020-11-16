using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guillotine : MonoBehaviour
{
    public float speed = 3;
    public float range = 6;

    private Vector3 startPos;
    private Vector3 translation;

    private void Start()
    {
        startPos = transform.position;
        translation = transform.up * speed;
    }

    private void FixedUpdate()
    {
        transform.Translate(translation, Space.Self);

        if ((startPos - transform.position).magnitude > range)
        {
            translation *= -1;
        }
    }

}