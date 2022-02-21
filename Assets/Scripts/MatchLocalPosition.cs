using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchLocalPosition : MonoBehaviour
{
    public Transform targetTransform;
    void Update(){
        transform.localPosition = targetTransform.localPosition;
    }
}
