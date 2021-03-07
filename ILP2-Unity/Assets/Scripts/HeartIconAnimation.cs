using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartIconAnimation : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;

    [SerializeField]
    Vector3 minScale, maxScale;

    [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    float animLength;

    float startTime = 0;

    float timePercent = 1;

    sbyte invert = -1;// 1 = play forwards, -1 = play backwards

    /*void Update()
    {
        playAnimation();
    }*/

    
    public void playAnimation()
    {
        if (timePercent >= 1) {
            invert *= -1;//switch sign
            startTime = Time.time;
        }

        timePercent = (Time.time - startTime) / animLength;
        

        if (invert == 1) {
            rect.localScale = Vector3.Lerp(minScale, maxScale, curve.Evaluate(timePercent));
        }
        else {//play animation backwards
            rect.localScale = Vector3.Lerp(minScale, maxScale, curve.Evaluate(1 - timePercent));
        }

        
    }
}
