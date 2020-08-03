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
        cameraSize = SizeM;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CameraFollow();
        CameraSizeChange();
    }

    void CameraFollow()
    {
        Vector3 playerPos = player.transform.position;

        //transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        transform.position = new Vector3(
            playerPos.x + (Input.GetAxis("RSX") * cameraSize), playerPos.y + (Input.GetAxisRaw("RSY") * cameraSize * 4 / 5), transform.position.z);

        if (transform.position.x <= minX + cameraSize + (cameraSize*4 / 5))
        {
            transform.position = new Vector3(minX + cameraSize + (cameraSize*4 / 5), transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= maxX - cameraSize - (cameraSize*4 / 5)) 
        {
            transform.position = new Vector3(maxX - cameraSize - (cameraSize*4 / 5), transform.position.y, transform.position.z);
        }

        if (transform.position.y <= minY + cameraSize)
        {
            transform.position = new Vector3(transform.position.x, minY + cameraSize, transform.position.z);
        }
        else if (transform.position.y >= maxY - cameraSize) 
        {
            transform.position = new Vector3(transform.position.x, maxY - cameraSize, transform.position.z);
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
