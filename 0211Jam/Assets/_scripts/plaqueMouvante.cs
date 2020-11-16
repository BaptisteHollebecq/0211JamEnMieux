using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaqueMouvante : MonoBehaviour
{
    public bool Left = true;
    public float speed = 3;
    public float range = 6;

    private Vector3 startPos;
    private Vector3 translation;

    private void Start()
    {
        startPos = transform.position;
        if (Left)
            translation = -transform.right * speed;
        else
            translation = transform.right * speed;
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
