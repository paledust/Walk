using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Trigger : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem leaf_particle;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            leaf_particle.transform.position = transform.position + Vector3.up*1.5f;
            leaf_particle.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            leaf_particle.Stop();
        }
    }
}
