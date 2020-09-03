using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayPoint : MonoBehaviour
{

    public  Vector3 relayPoint1, relayPoint2, relayPoint3, relayPoint4, relayPoint5,relayPoint6,relayPoint7;
    public float radius;

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
        Gizmos.DrawWireSphere(relayPoint1, radius);
        Gizmos.DrawWireSphere(relayPoint2, radius);
        Gizmos.DrawWireSphere(relayPoint3, radius);
        Gizmos.DrawWireSphere(relayPoint4, radius);
        Gizmos.DrawWireSphere(relayPoint5, radius);
        Gizmos.DrawWireSphere(relayPoint6, radius);
        Gizmos.DrawWireSphere(relayPoint7, radius);
    }
}
