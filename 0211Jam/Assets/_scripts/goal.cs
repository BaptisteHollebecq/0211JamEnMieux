using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    public ParticleSystem particles;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            particles.Play();
            other.GetComponent<GolfBall>().notWin = false;

        }
    }
}
