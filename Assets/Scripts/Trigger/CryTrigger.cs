using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_audio;
    bool InRange = false;
    void Update(){
        if(InRange && GameManager.Instance.characterManager.Possessing){
            if(!m_audio.isPlaying){
                m_audio.Play();
            }
        }
        else{
            if(m_audio.isPlaying){
                m_audio.Stop();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            InRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == Service.playerTag) {
            InRange = false;
        }
    }
}
