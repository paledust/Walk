using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTrigger : MonoBehaviour
{
    [SerializeField]
    private float bounceTime;
    [SerializeField]
    private float bounceDegree;
    [SerializeField]
    private ParticleSystem grass_particle;
    bool shaking = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            if(!shaking){
                shaking = true;
                grass_particle.transform.position = transform.position;
                grass_particle.Play();
                StartCoroutine(CoroutineTrigger());
            }
        }
    }
    IEnumerator CoroutineTrigger(){
        Vector3 currentScale = transform.localScale;
        for(float t=0; t<1; t+=Time.deltaTime/bounceTime){
            transform.localScale = Vector3.Lerp(currentScale, currentScale*bounceDegree, EasingFunc.Easing.pcurve(t));
            yield return null;
        }
        transform.localScale = currentScale;
        shaking = false;
    }
}
