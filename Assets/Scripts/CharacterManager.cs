using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;
using UnityEngine.UI;
public class CharacterManager : MonoBehaviour
{
[Header("Control Switch")]
    [SerializeField]
    private bool alterControl;
    [SerializeField]
    private Camera cam_1st;
    [SerializeField]
    private Camera cam_3rd;
    [SerializeField]
    private Transform world_2d;
    [SerializeField]
    private Transform world_3d;
    [SerializeField]
    private LevelManager levelManager;
[Header("Transition")]
    [SerializeField]
    private Image blackScreen;
    [SerializeField]
    private float transitionTime = 1;
[Header("Control")]
    [SerializeField]
    private Transform Dummy;
    [SerializeField]
    private FadeFootPrint leftFoot;
    [SerializeField]
    private FadeFootPrint rightFoot;
    [SerializeField]
    private float MaxSpeed;
    [SerializeField]
    private float FootDistance;
[Header("Particle")]
    [SerializeField]
    private ParticleSystem flowerPuff;
[Header("Audio")]
    [SerializeField]
    private AudioSource m_audio;
    [SerializeField]
    private AudioClip[] grass_clips;
    [SerializeField]
    private AudioClip[] water_clips;
    public FadeFootPrint LeftFoot{get{return leftFoot;}}
    public FadeFootPrint RightFoot{get{return rightFoot;}}
    public Vector3 position{get{return Dummy.position;}}
    public float Speed{get; private set;}
    public bool Transformed{get; private set;}
    public bool Possessing{get{return possessing;}}
    GameObject transformedObject;
    BirdPossessableObject possessObject;
    Vector3 PersonPosition;
    Vector3 move;
    Quaternion lookDir;
    float distance;
    bool use_right_foot = true;
    bool in_water = false;
    bool possessing = false;
    bool DuringPossess = false;
    bool pressPossess = false;
    bool PossessingDisabled = false;
    void Awake(){
        Transformed = false;
        PersonPosition = transform.position;
        GameManager.Instance.GrabCharacterManager(this);
    }
    void Update(){
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move = Vector3.ClampMagnitude(move, 1);
        HandleMove(move);

        pressPossess = Input.GetKeyDown(KeyCode.E);
        HandlePossess(pressPossess);
    }
    void HandlePossess(bool possess){
        if(PossessingDisabled) return;
        if(possess){
            if(!possessing){
                Possess(possessObject);
            }
            else{
                UnPossess();
            }
        }
    }
    void HandleMove(Vector3 move){
        Speed = move.magnitude * MaxSpeed;
        PersonPosition = Dummy.position;

        if(move.magnitude>0){
        //Move Character
            if(alterControl){
                move = (Camera.main.transform.right*move.x + Camera.main.transform.forward*move.y);
                move.y = move.z;
                move.z = 0;
                PersonPosition += move * MaxSpeed * Time.deltaTime;
            }
            else{
                PersonPosition += move * MaxSpeed * Time.deltaTime;
            }
            Dummy.position = PersonPosition;
            Dummy.rotation = Quaternion.LookRotation(move, -Vector3.forward);

            distance += move.magnitude * MaxSpeed * Time.deltaTime;
            if(distance>=FootDistance){
                distance = 0;

                if(in_water){
                    m_audio.PlayRandomClipFromClips(water_clips, 0.1f);
                }
                else{
                    m_audio.PlayRandomClipFromClips(grass_clips,0.1f);
                }

                if(use_right_foot){
                    rightFoot.transform.position = PersonPosition + Dummy.right * 0.25f;
                    rightFoot.transform.rotation = Dummy.rotation;
                    rightFoot.RefreshFootPrint();
                    EventManager.Instance.FireEvent(new FootPrintEvent(rightFoot.transform.position));
                }
                else{
                    leftFoot.transform.position  = PersonPosition - Dummy.right * 0.25f;
                    leftFoot.transform.rotation  = Dummy.rotation;
                    leftFoot.RefreshFootPrint();
                    EventManager.Instance.FireEvent(new FootPrintEvent(leftFoot.transform.position));
                }

                use_right_foot = !use_right_foot;
            }
        }
    }
    public void EnterWater(){
        in_water = true;
    }
    public void ExitWater(){
        in_water = false;
    }
    public void TransformedToThis(GameObject prefab){
        flowerPuff.transform.position = Dummy.position;
        flowerPuff.Play();
        transformedObject = GameObject.Instantiate(prefab, Dummy.position, prefab.transform.rotation);
        GameManager.Instance.soundManager.PlayPop();
        leftFoot.TurnOffSprite();
        rightFoot.TurnOffSprite();
        Transformed = true;
    }
    public void RegisterObject(BirdPossessableObject obj){
        possessObject = obj;
    }
    public void UnRegisterObject(BirdPossessableObject obj){
        if(possessObject == obj){
            possessObject = null;
        }
    }
    public void Possess(BirdPossessableObject obj){
        if(obj==null || DuringPossess) return;

        DuringPossess = true;
        StartCoroutine(CoroutinePossess(obj));
    }
    public void UnPossess(){
        if(DuringPossess) return;

        DuringPossess = true;
        StartCoroutine(CoroutineUnPossess());
    }
    public void TurnOnAltControl(){
        alterControl = true;
    }
    public void TurnOffAltControl(){
        alterControl = false;
    }
    public void EnablePossess(){PossessingDisabled = false;}
    public void DisablePossess(){PossessingDisabled = true;}
    IEnumerator CoroutinePossess(BirdPossessableObject obj){
        Vector3 targetCamPos = obj.transform.position;
        Vector3 initCamPos   = cam_3rd.transform.position;
        targetCamPos.z = cam_3rd.transform.position.z;

        for(float t=0; t<1; t+=Time.deltaTime/transitionTime){
            float _t = EasingFunc.Easing.QuadEaseIn(t);
            blackScreen.color = Color.Lerp(Color.clear, Color.black, _t);
            cam_3rd.transform.position = Vector3.Lerp(initCamPos, targetCamPos, _t);
            cam_3rd.orthographicSize = Mathf.Lerp(10, 4, _t);
            yield return null;
        }
        cam_3rd.orthographicSize = 4;
        cam_3rd.transform.position = targetCamPos;

        blackScreen.color = Color.black;

    //Camera Switch
        cam_1st.gameObject.SetActive(true);
        cam_3rd.gameObject.SetActive(false);
        Vector3 camPos = world_2d.InverseTransformPoint(obj.PossesCamRoot.position);
        camPos.z = cam_1st.transform.localPosition.z;
        cam_1st.transform.localPosition = camPos;

        for(float t=0; t<1; t+=Time.deltaTime/transitionTime){
            float _t = EasingFunc.Easing.QuadEaseOut(t);
            blackScreen.color = Color.Lerp(Color.black, Color.clear, _t);
            cam_1st.fieldOfView = Mathf.Lerp(60, 30, _t);
            yield return null;
        }
        cam_1st.fieldOfView = 30;
        blackScreen.color = Color.clear;
        
        DuringPossess = false;
        possessing = true;
    }
    IEnumerator CoroutineUnPossess(){
        yield return null;
        UnRegisterObject(possessObject);

        for(float t=0; t<1; t+=Time.deltaTime/transitionTime){
            float _t = EasingFunc.Easing.QuadEaseIn(t);
            blackScreen.color = Color.Lerp(Color.clear, Color.black, _t);
            cam_1st.fieldOfView = Mathf.Lerp(30, 60, _t);
            yield return null;
        }
        cam_1st.fieldOfView = 60;

        cam_1st.gameObject.SetActive(false);
        cam_3rd.gameObject.SetActive(true);

        Vector3 targetCamPos = levelManager.CurrentLevel.CameraPos.position;
        Vector3 initCamPos   = cam_3rd.transform.position;
        targetCamPos.z = cam_3rd.transform.position.z;
        for(float t=0; t<1; t+=Time.deltaTime/transitionTime){
            float _t = EasingFunc.Easing.QuadEaseOut(t);
            blackScreen.color = Color.Lerp(Color.black, Color.clear, _t);
            cam_3rd.transform.position = Vector3.Lerp(initCamPos, targetCamPos, _t);
            cam_3rd.orthographicSize = Mathf.Lerp(4, 10, _t);
            yield return null;
        }
        cam_3rd.transform.position = targetCamPos;
        cam_3rd.orthographicSize = 10;
        blackScreen.color = Color.clear;

        DuringPossess = false;
        possessing = false;
    }
}
