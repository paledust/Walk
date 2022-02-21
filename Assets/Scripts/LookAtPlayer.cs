using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform playerMimic;
    void Update()
    {
        Vector3 lookForward = playerMimic.transform.position - transform.position;
        lookForward.y = 0;
        transform.rotation = Quaternion.LookRotation(lookForward, Vector3.up);        
    }
}
