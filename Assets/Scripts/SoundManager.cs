using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_audio;
    [SerializeField]
    private AudioClip[] puffClips;
    [SerializeField]
    private AudioClip[] popClips;
    public void PlayPuff(){m_audio.PlayRandomClipFromClips(puffClips);}
    public void PlayPop(){m_audio.PlayRandomClipFromClips(popClips);}
}
