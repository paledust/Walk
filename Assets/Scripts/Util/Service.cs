using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Service
{
    public static string playerTag = "Player";
}
#region Extension
public static class ExtensionMethods{
    //Play Audio clip from a group of audio clips
    public static void PlayRandomClipFromClips(this AudioSource audio,AudioClip[] clips){
        audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
    public static void PlayRandomClipFromClips(this AudioSource audio,AudioClip[] clips, float pitchShift){
        audio.pitch = 1+Random.Range(-pitchShift, pitchShift);
        audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}

#endregion
