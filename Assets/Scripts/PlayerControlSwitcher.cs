using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSwitcher : MonoBehaviour
{
    void OnEnable(){
        GameManager.Instance.characterManager.TurnOnAltControl();
    }
    void OnDisable(){
        GameManager.Instance.characterManager.TurnOffAltControl();
    }
}
