using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionBehaviour : MonoBehaviour
{
    public CameraBehaviour cam;
    public float MaxHeight = 5f;
    public float MinHeight = 1f;

    [SerializeField]
    [HideInInspector]
    private Vector3 initialOffset;

    private Vector3 currentOffset;

    public float _distToGround;

    [ContextMenu("Set Current Offset")]
    private void SetCurrentOffset()
    {
        if (cam.target == null)
        {
            return;
        }

        initialOffset = transform.position - cam.target.position;
    }

    private void Start()
    {
        if (cam.target == null)
        {
            Debug.LogError("Assign a target for the camera in Unity's inspector");
        }

        currentOffset = initialOffset;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.gameObject.layer == 8)
            {
                _distToGround = (hit.point - transform.position).magnitude;
            }
        }

    }

    private void LateUpdate()
    {
        transform.position = cam.target.position + currentOffset;

        float movement = Input.GetAxis("Horizontal") * cam.angularSpeed * Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Keypad4) || Input.GetKeyUp(KeyCode.Keypad6))
            movement = 0f;
        if (!Mathf.Approximately(movement, 0f))
        {
            transform.RotateAround(cam.target.position, Vector3.up, movement);
            currentOffset = transform.position - cam.target.position;
        }

        float upMovement = Input.GetAxis("Vertical") * cam.angularSpeed * Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Keypad8) || Input.GetKeyUp(KeyCode.Keypad2))
            upMovement = 0f;
        if (!Mathf.Approximately(upMovement, 0f))
        {
            if (upMovement < 0 && _distToGround < MaxHeight)
            {
                transform.RotateAround(cam.target.position, cam.transform.right, upMovement);
                currentOffset = transform.position - cam.target.position;
            }
            if (upMovement > 0 && _distToGround > MinHeight)
            {
                transform.RotateAround(cam.target.position, cam.transform.right, upMovement);
                currentOffset = transform.position - cam.target.position;
            }
        }

        //Debug.DrawRay(transform.position, cam.target.position- transform.position);
    }
}
