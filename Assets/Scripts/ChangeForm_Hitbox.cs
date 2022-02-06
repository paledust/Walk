using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForm_Hitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private Vector3 spawnOffset;
    bool TestStarted = false;
    void Update(){
        if(TestStarted){
            if(!GameManager.Instance.characterManager.Transformed){
                GameManager.Instance.characterManager.TransformedToThis(prefabs[Random.Range(0, prefabs.Length)], spawnOffset);
            }
        }
    }
    void OnTriggerEnter (Collider other){
        if(other.gameObject.tag == "Player"){
            TestStarted = true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player"){
            TestStarted = false;
        }
    }
}
