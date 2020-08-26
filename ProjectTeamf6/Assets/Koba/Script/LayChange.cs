using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayChange : MonoBehaviour
{
    [SerializeField]
    Base baseScript;
    [SerializeField]
    int State;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        State = baseScript.ReturnBaseType();
        if(State == 4)
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyVillage");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Village");
        }
    }
}
