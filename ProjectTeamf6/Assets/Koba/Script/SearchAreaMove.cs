using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAreaMove : MonoBehaviour
{
    public bool IsSearchPlayer;
    // Start is called before the first frame update
    void Start()
    {
        IsSearchPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsSearchPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsSearchPlayer = false;
        }
    }

    public bool RetrunIsSearchPlayer()
    {
        return IsSearchPlayer;
    }
}

