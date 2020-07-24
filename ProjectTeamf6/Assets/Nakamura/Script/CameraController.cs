using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera camera;

    GameObject player;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    private float CameraSize;
    public float SizeS;
    public float SizeM;
    public float SizeL;

    string CameraSizeState;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        CameraSizeState = "M";
        CameraSize = SizeM;
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
        CameraMove();
    }

    void CameraFollow()
    {
        Vector3 playerPos = player.transform.position;

        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);

        if (playerPos.x <= minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        else if (playerPos.x >= maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }

        if (playerPos.y <= minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
        else if (playerPos.y >= maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
    }

    void CameraMove()
    {
        camera.orthographicSize = CameraSize;

        if (Input.GetButtonDown("R3"))
        {
            switch (CameraSizeState)
            {
                case "S":
                    CameraSizeState = "M";
                    CameraSize = SizeM;
                    break;
                case "M":
                    CameraSizeState = "L";
                    CameraSize = SizeL;
                    break;
                case "L":
                    CameraSizeState = "S";
                    CameraSize = SizeS;
                    break;
            }
        }
    }
}
