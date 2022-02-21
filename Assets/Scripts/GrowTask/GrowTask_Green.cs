using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTask_Green : GrowTask
{
    [SerializeField]
    private SpriteRenderer growSprite;
    [SerializeField]
    private Color GreenColor = Color.white;
    [SerializeField]
    private float Scale = 1;
    [SerializeField]
    private float GrowUpTime = .5f;
    protected override void GrowMethod()
    {
        StartCoroutine(coroutineGrowUp());
    }
    IEnumerator coroutineGrowUp(){
        Color initColor = growSprite.color;
        Vector3 initScale = growSprite.transform.localScale;
        for(float t=0; t<1; t+=Time.deltaTime/GrowUpTime){
            growSprite.color = Color.Lerp(initColor, GreenColor, EasingFunc.Easing.BackEaseOut(t));
            growSprite.transform.localScale = Vector3.LerpUnclamped(initScale, Vector3.one, EasingFunc.Easing.BackEaseOut(t));
            yield return null;
        }
        growSprite.color = GreenColor;
        growSprite.transform.localScale = Vector3.one;
    }
}
