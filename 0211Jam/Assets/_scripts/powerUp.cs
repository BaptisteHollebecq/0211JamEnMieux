using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public static event System.Action touched;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
            collision.transform.GetComponent<GolfBall>().canJump = true;
            touched?.Invoke();
        }
    }
}
