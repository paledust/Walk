using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
[Header("Move")]
    [SerializeField]
    private Transform birdTrans;
    [SerializeField]
    private float seekRadius;
    [SerializeField]
    private float keptRadius;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float moveStep;
[Header("Jump")]
    [SerializeField]
    private Transform birdJump;
    [SerializeField]
    private float jumpRate = 10;
    [SerializeField]
    private float jumpDist = 0.1f;
[Header("Visual")]
    [SerializeField]
    private SpriteRenderer birdSprite;
    bool Detecting = false;
    Vector3 initPos;
    Vector3 targetPos;
    void Start(){
        initPos = transform.position;
        targetPos = initPos;
    }
    void Update(){
        if(Detecting){
            targetPos = GameManager.Instance.characterManager.position;
            targetPos.z = initPos.z;
            ComparePositionToFlipSprite(targetPos.x<initPos.x);
        }
        else{
            ComparePositionToFlipSprite(targetPos.x<birdTrans.position.x);
        }
        
        Vector3 diff = targetPos - initPos;
        targetPos = initPos + Vector3.ClampMagnitude(diff-diff.normalized*keptRadius, seekRadius);


        if(Vector3.Distance(targetPos, birdTrans.position)>=moveStep){
            birdTrans.position += (targetPos - birdTrans.position).normalized*speed*Time.deltaTime;
            birdJump.localPosition = Vector3.Lerp(Vector3.zero, Vector3.up*jumpDist, EasingFunc.Easing.pcurve(Time.time*jumpRate - Mathf.Floor(Time.time*jumpRate)));
        }
    }
    void ComparePositionToFlipSprite(bool LeftSide){
        if(LeftSide && !birdSprite.flipX) birdSprite.flipX = true;
        else if(!LeftSide && birdSprite.flipX) birdSprite.flipX = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag) Detecting = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == Service.playerTag) {
            Detecting = false;
            targetPos = initPos;
        }
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seekRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, keptRadius);
    }
}
