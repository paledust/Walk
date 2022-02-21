using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea_Trigger : MonoBehaviour
{
    public GameObject rippleParticle;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            rippleParticle.SetActive(true);
            GameManager.Instance.characterManager.EnterWater();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            rippleParticle.SetActive(false);
            GameManager.Instance.characterManager.ExitWater();
        }
    }
}
