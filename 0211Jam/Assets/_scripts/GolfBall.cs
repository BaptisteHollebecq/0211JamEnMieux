using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfBall : MonoBehaviour
{
    public Transform direction;
    public GameObject duplicate;
    public GameObject indicator;
    public Text scoreText;
    public float power = 10;
    public AudioClip bonus;
   // public float timerPreview = 2;

    private Rigidbody _rb;
    public bool onGround = true;
    public bool lunchAtGround = false;
    public bool notWin = true;

    public bool canJump = false;
    public int Score = 0;

    private Vector3 LastPos;
    private Vector3 StartPos;
    private AudioSource source;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        powerUp.touched += Sound;
        Preview.isDestroyed += Lunch;
        Time.timeScale = 3;
        LastPos = transform.position;
        StartPos = transform.position;
        scoreText.text = Score.ToString();
    }

    private void OnDestroy()
    {
        Preview.isDestroyed -= Lunch;
        powerUp.touched -= Sound;
    }

    void Sound()
    {
        source.PlayOneShot(bonus);
    }

    void Lunch()
    {
        if (onGround && notWin)
        {
            var inst = Instantiate(duplicate, transform.position, Quaternion.identity);
            inst.GetComponent<Preview>().forceDir = (direction.position - transform.position) * direction.GetComponent<DirectionBehaviour>()._distToGround;
            inst.GetComponent<Preview>().timer = direction.GetComponent<DirectionBehaviour>()._distToGround;
        }
        else
            lunchAtGround = true;
    }

    private void Start()
    {
        Lunch();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad5) && onGround && notWin)
        {
            source.Play();
            Vector3 forceDir = (direction.position - transform.position) * direction.GetComponent<DirectionBehaviour>()._distToGround;
            _rb.AddForce(forceDir, ForceMode.Impulse);
            Score++;
            scoreText.text = Score.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Keypad7) && canJump)
        {
            //Time.timeScale = 1;
            _rb.AddForce(Vector3.up * power, ForceMode.Impulse);
            canJump = false;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.position = StartPos;
            Score = 0;
            scoreText.text = Score.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Application.Quit();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 8 && _rb.velocity.magnitude <= .3f)
        {
            onGround = true;
            _rb.velocity = Vector3.zero;
            indicator.SetActive(true);
            LastPos = transform.position;

            if (lunchAtGround)
            {
                lunchAtGround = false;
                Lunch();

            }
        }
    }

    public void Death()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.position = LastPos;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            onGround = false;
            indicator.SetActive(false);
        }
    }
}
