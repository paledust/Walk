using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand_Trigger : MonoBehaviour
{
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform endPos;
    [SerializeField]
    private AudioSource amb_sea;
    [SerializeField]
    private AudioSource amb_forest;
    bool Detecting = false;
    float lerp;
    FadeFootPrint leftFoot;
    FadeFootPrint rightFoot;
    float initAlpha_left;
    float initAlpha_right;
    void Update(){
        if(!Detecting) return;

        lerp = (GameManager.Instance.characterManager.position.y-startPos.position.y)/(endPos.position.y-startPos.position.y);
        
        leftFoot.ChangeInitAlpha(Mathf.Lerp(initAlpha_left, 0, lerp));
        rightFoot.ChangeInitAlpha(Mathf.Lerp(initAlpha_right, 0 ,lerp));

        amb_sea.volume = Mathf.Lerp(1, 0, lerp);
        amb_forest.volume = Mathf.Lerp(0, 1, lerp);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            Detecting = true;
            leftFoot = GameManager.Instance.characterManager.LeftFoot;
            rightFoot= GameManager.Instance.characterManager.RightFoot;
            initAlpha_left = leftFoot.initAlpha;
            initAlpha_right= rightFoot.initAlpha;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            Detecting = false;
        }
    }
}
