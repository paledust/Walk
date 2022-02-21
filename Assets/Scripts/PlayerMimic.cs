using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMimic : MonoBehaviour
{
    [SerializeField]
    private Transform LevelTrans_2D;
    [SerializeField]
    private Transform LevelTrans_3D;
    [SerializeField]
    private Camera cam;
    Vector3 pos;
    float CamHeight;
    void Start(){
        CamHeight = LevelTrans_3D.InverseTransformPoint(cam.transform.position).z;
    }
    void Update()
    {
        pos = LevelTrans_2D.InverseTransformPoint(GameManager.Instance.characterManager.position);
        pos.z = CamHeight;
        transform.localPosition = pos;
    }
}
