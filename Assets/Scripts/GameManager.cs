using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    public void AssignCharacterManager(CharacterManager manager){
        characterManager = manager;
    }
}
