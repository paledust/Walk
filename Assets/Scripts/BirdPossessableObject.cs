using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPossessableObject: MonoBehaviour
{
    [SerializeField]
    private TextMesh text;
    [SerializeField]
    private Transform m_root;
    [SerializeField]
    private SpriteRenderer m_sprite;
    public Transform PossesCamRoot{get{return m_root;}}
    bool Detecting = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            Detecting = true;
            text.gameObject.SetActive(true);
            GameManager.Instance.characterManager.RegisterObject(this);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == Service.playerTag){
            Detecting = false;
            text.gameObject.SetActive(false);
            GameManager.Instance.characterManager.UnRegisterObject(this);
            if(GameManager.Instance.characterManager.Possessing){
                GameManager.Instance.characterManager.UnPossess();
            }
        }
    }
}
