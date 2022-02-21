using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Trigger : MonoBehaviour
{
    [SerializeField]
    private Animation m_anime;
    [SerializeField]
    private Collider2D m_collider;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            m_anime.Play();
            m_collider.enabled = false;
        }
    }
}
