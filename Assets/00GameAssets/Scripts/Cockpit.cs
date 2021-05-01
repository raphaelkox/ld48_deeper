using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cockpit : MonoBehaviour
{
    public Transform cockpitBase;
    public SpriteRenderer window;
    public Gradient seaColors;

    public float shakeDuration;
    public float shakeStrenght;
    public int shakeVibrato;

    // Update is called once per frame
    void Update()
    {
        window.color = seaColors.Evaluate(PressureSystem.depth_normalized);
    }

    public void Shake() {
        cockpitBase.DOShakePosition(shakeDuration, shakeStrenght, shakeVibrato);
    }
}
