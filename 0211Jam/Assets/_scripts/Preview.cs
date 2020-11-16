using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Preview : MonoBehaviour
{
    public static event System.Action isDestroyed;

    private Rigidbody _rb;

    public Vector3 forceDir;
    public float timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.AddForce(forceDir, ForceMode.Impulse);
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        isDestroyed?.Invoke();
    }

}
