using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            collision.transform.GetComponent<GolfBall>().Death();
        }
    }
}
