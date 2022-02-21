using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFootPrint : MonoBehaviour
{
    [SerializeField]
    private float fadeOutSpeed = 1;
    [SerializeField]
    private SpriteRenderer m_sprite;
    public float initAlpha{get; private set;}
    Color footprintColor;
    Material m_mat;
    void Start()
    {
        footprintColor = m_sprite.color;
        initAlpha = footprintColor.a;
    }
    void Update()
    {
        if(footprintColor.a != 0){
            footprintColor.a = Mathf.Lerp(footprintColor.a, 0, Time.deltaTime * fadeOutSpeed);
            if(footprintColor.a<=0.001f){
                footprintColor.a = 0;
            }

            m_sprite.color = footprintColor;
        }
    }
    public void RefreshFootPrint(){
        footprintColor.a = initAlpha;
    }
    public void ChangeInitAlpha(float alpha){initAlpha = alpha;}
    public void TurnOnSprite(){m_sprite.enabled=true;}
    public void TurnOffSprite(){m_sprite.enabled=false;}
}
