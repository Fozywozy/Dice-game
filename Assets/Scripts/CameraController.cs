using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Player => GameObject.FindGameObjectWithTag("Player");
    private GameObject Board => GameObject.FindGameObjectWithTag("Move board");

    private float UDMouse = 0;
    private float SSMouse = 180;
    private float Zoom = 1;

    public bool FollowPlayer;

    private void Start()
    {
        transform.eulerAngles = new Vector3(UDMouse, SSMouse, 0);
    }

    private void LateUpdate()
    {
        CheckInputs();

        if (FollowPlayer)
        {
            transform.position = Player.transform.position + (-5 * Zoom * transform.forward);
        }

        if (transform.eulerAngles.y < 90 && transform.eulerAngles.y > 0)
        {
            Board.transform.eulerAngles = new Vector3(0, 270, 0);
        }
        else if (transform.eulerAngles.y < 180 && transform.eulerAngles.y > 90)
        {
            Board.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.eulerAngles.y < 270 && transform.eulerAngles.y > 180)
        {
            Board.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            Board.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }


    private void CheckInputs()
    {
        Zoom += Input.mouseScrollDelta.y;
        if (Zoom < 0.1f) { Zoom = 0.1f; }
        if (Zoom > 2) { Zoom = 2; }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            UDMouse -= Input.GetAxis("Mouse Y") * 8;
            SSMouse += Input.GetAxis("Mouse X") * 8;
            if (UDMouse > 80) { UDMouse = 80; }
            if (UDMouse < -80) { UDMouse = -80; }
            transform.eulerAngles = new Vector3(UDMouse, SSMouse, 0);
        }
    }
}
