using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera camera;

    GameObject player;

    //public float minX;
    public float X;

    //public float minY;
    public float Y;

    public float moveDouble;

    private float cameraSize;
    public float SizeS;
    public float SizeM;
    public float SizeL;

    string CameraSizeState;


    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        CameraSizeState = "L";
        cameraSize = SizeL;
    }

    // Update is called once per frame
    void Update()
    {
        CameraSizeChange();
    }

    private void FixedUpdate()
    {
        CameraFollow();
        
    }

    void CameraFollow()
    {
        Vector3 playerPos = player.transform.position;

        //transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        transform.position = new Vector3(
            playerPos.x + (Input.GetAxis("RSX") * cameraSize), playerPos.y + (Input.GetAxisRaw("RSY")/* * cameraSize * 4 / 5*/), transform.position.z);

        if (transform.position.x <= -X + cameraSize/* + (cameraSize*4 / 5)*/)
        {
            transform.position = new Vector3(-X + cameraSize /*+ (cameraSize*4 / 5)*/, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= X - cameraSize /*- (cameraSize*4 / 5)*/) 
        {
            transform.position = new Vector3(X - cameraSize /*- (cameraSize*4 / 5)*/, transform.position.y, transform.position.z);
        }

        if (transform.position.y <= -Y + cameraSize)
        {
            transform.position = new Vector3(transform.position.x, -Y + cameraSize, transform.position.z);
        }
        else if (transform.position.y >= Y - cameraSize) 
        {
            transform.position = new Vector3(transform.position.x, Y - cameraSize, transform.position.z);
        }
    }

    void CameraSizeChange()
    {
        camera.orthographicSize = cameraSize;

        if (Input.GetButtonDown("RB"))
        {
            switch (CameraSizeState)
            {
                case "S":
                    CameraSizeState = "M";
                    cameraSize = SizeM;
                    break;
                case "M":
                    CameraSizeState = "L";
                    cameraSize = SizeL;
                    break;
                case "L":
                    CameraSizeState = "S";
                    cameraSize = SizeS;
                    break;
            }
        }
    }

}
