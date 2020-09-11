using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimimapCamera : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MiniMapPlayer");        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = player.transform.position;
        //pos.x = transform.position.x;
        //pos.y = transform.position.y;
        transform.position = new Vector3(pos.x, pos.y, -0.1f);
        //transform.position = pos;
    }
}
