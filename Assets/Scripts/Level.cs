using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPos;
    [SerializeField]
    private GameObject[] CleanedUpObjects;
    public Transform CameraPos{get{return cameraPos;}}
    LevelManager levelManager;
    public void OnEnterLevel(){
        levelManager.MoveCameraToPosition(cameraPos.position);
    }
    public void OnExitLevel(){
        for(int i=0; i<CleanedUpObjects.Length; i++){
            CleanedUpObjects[i].SetActive(false);
        }
    }
    public void AssignLevelManager(LevelManager _levelManager){
        levelManager=_levelManager;
    }
}
