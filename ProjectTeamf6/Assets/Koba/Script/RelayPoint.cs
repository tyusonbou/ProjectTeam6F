using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayPoint : MonoBehaviour
{

    public  Vector3 relayPoint1, relayPoint2, relayPoint3, relayPoint4, relayPoint5;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(relayPoint1, 2);
        Gizmos.DrawWireSphere(relayPoint2, 2);
        Gizmos.DrawWireSphere(relayPoint3, 2);
        Gizmos.DrawWireSphere(relayPoint4, 2);
        Gizmos.DrawWireSphere(relayPoint5, 2);
    }
}
