using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Level[] levels;
    [SerializeField]
    private PlayableDirector director;
    public Level CurrentLevel{get{return levels[levelIndex];}}
    int levelIndex = 0;
    private void Awake(){
        for(int i=0; i<levels.Length; i++) levels[i].AssignLevelManager(this);
        levels[levelIndex].OnEnterLevel();
    }
    public void ProceedToNextLevel(){
        levels[levelIndex].OnExitLevel();
        levelIndex ++;
        if(levelIndex<levels.Length)
            levels[levelIndex].OnEnterLevel();
        else
            Debug.Log("Game Complete");
    }
    public void MoveCameraToPosition(Vector3 pos){
        StartCoroutine(CoroutineMoveCamera(pos));
    }
    public void EndGame(){
    //Disable Control from Player
        GameManager.Instance.characterManager.enabled = false;
        director.Play();
    }
    IEnumerator CoroutineMoveCamera(Vector3 position){
        Vector3 initPosition = mainCam.transform.position;

        for(float t=0; t<1; t+=Time.deltaTime*0.25f){
            mainCam.transform.position = Vector3.Lerp(initPosition, position, EasingFunc.Easing.SmoothInOut(t));
            yield return null;
        }

        mainCam.transform.position = position;
    }
}
