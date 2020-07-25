using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractScript : MonoBehaviour
{
    [SerializeField, Header("注意を引く時間"), Range(0, 100)]
    private float attractTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attractTime -= Time.deltaTime;
        if(attractTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
