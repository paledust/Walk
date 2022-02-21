using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForm_Hitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private GrowTask growTask;
    [SerializeField]
    private float GrowDelay = 0.1f;
    [SerializeField]
    private float SpawnDelay = 0.1f;
    [SerializeField]
    private float GrowAmount = 10;
    [SerializeField]
    private float SpawnRadius = 3;
    [SerializeField]
    private ParticleSystem cloudPuff;
    [SerializeField]
    private bool Growed = false;
    bool TestStarted = false;
    float timer = 0;
    void Update(){
        if(TestStarted){
            if(GameManager.Instance.characterManager.Speed == 0){
                if(!GameManager.Instance.characterManager.Transformed){
                    timer = Time.time;
                    GameManager.Instance.characterManager.TransformedToThis(prefabs[Random.Range(0, prefabs.Length)]);
                }
                if(!Growed && Time.time - timer >= GrowDelay){
                    StartCoroutine(growCoroutine());
                    Growed = true;
                }
            }
        }
    }
    IEnumerator growCoroutine(){
        if(growTask!=null)growTask.GrowUp();

        yield return new WaitForSeconds(SpawnDelay);

        GameObject spanwer=null;
        Vector3 spawnpos = Vector3.zero;
        for(int i=0; i< GrowAmount; i++){
            GameManager.Instance.soundManager.PlayPop();
            spawnpos = Random.insideUnitCircle * SpawnRadius;
            spanwer = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.parent.position, Quaternion.identity);
            spanwer.transform.parent = this.transform.parent;
            spanwer.transform.position = transform.parent.position + spawnpos;
            spanwer.transform.localScale = Vector3.one * Random.Range(0.4f,1f);
            cloudPuff.transform.position = spanwer.transform.position;
            cloudPuff.Play();
            yield return new WaitForSeconds(0.1f);
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.parent.position, SpawnRadius);
    }
    void OnTriggerEnter (Collider other){
        if(other.gameObject.tag == "Player"){
            timer = Time.time;
            TestStarted = true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Player"){
            TestStarted = false;
        }
    }
}
