using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitTrigger : MonoBehaviour
{
    [SerializeField]
    public LevelManager levelManager;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            levelManager.ProceedToNextLevel();
        }
    }
}
