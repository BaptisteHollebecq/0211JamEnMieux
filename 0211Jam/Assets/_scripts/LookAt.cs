using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public float offset = 0.5f;

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y + offset, target.position.z);
    }
}
