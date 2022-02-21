using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSystem;
public class RippleParticle : MonoBehaviour
{
    ParticleSystem m_particleSystem;
    void OnEnable(){
        m_particleSystem = GetComponent<ParticleSystem>();
        EventManager.Instance.Register<FootPrintEvent>(FootPrintEventHandler);
    }
    void OnDisable(){
        EventManager.Instance.UnRegister<FootPrintEvent>(FootPrintEventHandler);
    }
    public void FootPrintEventHandler(MyEventSystem.Event e){
        FootPrintEvent tempEvent = e as FootPrintEvent;
        transform.position = tempEvent.printPos;
        m_particleSystem.Play();
    }
}
