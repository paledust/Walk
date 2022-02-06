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
    private float speed;
    [SerializeField]
    private float FootDistance;
[Header("Audio")]
    [SerializeField]
    private AudioSource m_audio;
    [SerializeField]
    private AudioClip[] grass_clips;
    Vector3 PersonPosition;
    Vector3 move;
    Quaternion lookDir;
    float distance;
    bool use_right_foot = true;
    // Start is called before the first frame update
    void Start()
    {
        PersonPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.z = Input.GetAxisRaw("Vertical");
        move = Vector3.ClampMagnitude(move, 1);
        
        if(move.magnitude>0){
            PersonPosition += move * speed * Time.deltaTime;
            Dummy.position = PersonPosition;
            Dummy.rotation = Quaternion.LookRotation(move, Vector3.up);

            distance += move.magnitude * speed * Time.deltaTime;
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
}
