using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
[Header("Control")]
    [SerializeField]
    private Transform Dummy;
    [SerializeField]
    private Transform leftFoot;
    [SerializeField]
    private Transform rightFoot;
    [SerializeField]
    private float MaxSpeed;
    [SerializeField]
    private float FootDistance;
[Header("Visual")]
    [SerializeField]
    private Projector leftFoot_Proj;
    [SerializeField]
    private Projector rightFoot_Proj;
[Header("Audio")]
    [SerializeField]
    private AudioSource m_audio;
    [SerializeField]
    private AudioClip[] grass_clips;
    public float Speed{get; private set;}
    public bool Transformed{get; private set;}
    GameObject transformedObject;
    Vector3 PersonPosition;
    Vector3 move;
    Quaternion lookDir;
    float distance;
    bool use_right_foot = true;
    void Start(){
        Transformed = false;
        PersonPosition = transform.position;
        GameManager.Instance.AssignCharacterManager(this);
    }
    void Update(){
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");
        move = Vector3.ClampMagnitude(move, 1);

        Speed = move.magnitude * MaxSpeed;
        
        if(move.magnitude>0){
        //First DeTrasnformed
            if(Transformed){
                Transformed = false;
                leftFoot_Proj.enabled = true;
                rightFoot_Proj.enabled = true;
                Destroy(transformedObject);
                transformedObject = null;
            }
        //Move Character
            PersonPosition += move * MaxSpeed * Time.deltaTime;
            Dummy.position = PersonPosition;
            Dummy.rotation = Quaternion.LookRotation(move, Vector3.up);

            distance += move.magnitude * MaxSpeed * Time.deltaTime;
            if(distance>=FootDistance){
                distance = 0;

                m_audio.pitch = Random.Range(0.9f,1.1f);
                m_audio.PlayOneShot(grass_clips[Random.Range(0, grass_clips.Length)], 1);

                if(use_right_foot){
                    rightFoot.position = PersonPosition + Dummy.right * 0.25f;
                    rightFoot.rotation = Dummy.rotation;
                }
                else{
                    leftFoot.position  = PersonPosition - Dummy.right * 0.25f;
                    leftFoot.rotation  = Dummy.rotation;
                }

                use_right_foot = !use_right_foot;
            }
        }
    }
    public void TransformedToThis(GameObject prefab, Vector3 spawnOffset){
        transformedObject = GameObject.Instantiate(prefab, Dummy.position, prefab.transform.rotation);
        transformedObject.transform.position += spawnOffset;
        leftFoot_Proj.enabled = false;
        rightFoot_Proj.enabled = false;
        Transformed = true;
    }
}
