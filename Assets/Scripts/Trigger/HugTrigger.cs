using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private GameObject cryAudio;
    [SerializeField]
    private GameObject cryTear;
    bool Detecting = false;
    void Update(){
        if(Detecting && GameManager.Instance.characterManager.Possessing){
            if(Input.GetKeyDown(KeyCode.E)){
                levelManager.EndGame();
                text.SetActive(false);
                cryAudio.SetActive(false);
                cryTear.SetActive(true);
                this.enabled = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            Detecting = true;
            text.SetActive(true);
            GameManager.Instance.characterManager.DisablePossess();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == Service.playerTag) {
            Detecting = false;
            text.SetActive(false);
            GameManager.Instance.characterManager.EnablePossess();
        }
    }
}
