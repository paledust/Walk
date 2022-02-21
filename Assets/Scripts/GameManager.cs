using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SoundManager _soundManager;
    public SoundManager soundManager{get{return _soundManager;}}
    public static GameManager Instance{get{return instance;}}
    private static GameManager instance;
    public CharacterManager characterManager{get; private set;}
    void Awake(){
        if(instance==null){
            instance = this;
        } 
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            EndGame();
        }
    }
    public void EndGame(){Application.Quit();}
    public void GrabCharacterManager(CharacterManager manager){
        characterManager = manager;
    }
}
