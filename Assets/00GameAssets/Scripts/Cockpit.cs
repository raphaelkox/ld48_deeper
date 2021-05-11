using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cockpit : MonoBehaviour
{
    public Transform cockpitBase;
    public RectTransform depthIndicator;
    public Image dangerLight;
    public SpriteRenderer window;
    public Gradient seaColors;

    public float shakeDuration;
    public float shakeStrenght;
    public int shakeVibrato;

    public Sequence dangerLightFlash;

    private void Start() {
        PressureSystem.OnDangerActivate += DangerLightOn;
        PressureSystem.OnDangerDeactivate += DangerLightOff;
    }

    // Update is called once per frame
    void Update()
    {
        window.color = seaColors.Evaluate(PressureSystem.depth_normalized);

        depthIndicator.anchoredPosition = new Vector2(0, (PressureSystem.depth_normalized * 50f) * -1f);
    }

    public void Shake() {
        cockpitBase.DOShakePosition(shakeDuration, shakeStrenght, shakeVibrato);
    }

    public void DangerLightOn() {
        dangerLight.DOFade(0.0f, 0.0f);
        dangerLight.enabled = true;        

        dangerLightFlash = DOTween.Sequence();
        for (int i = 0; i < Mathf.CeilToInt(PressureSystem.danger_delay); i++) {
            dangerLightFlash.Append(dangerLight.DOFade(0.6f, 0.15f));
            dangerLightFlash.Append(dangerLight.DOFade(0.0f, 0.15f));
            dangerLightFlash.Append(dangerLight.DOFade(0.6f, 0.15f));
            dangerLightFlash.Append(dangerLight.DOFade(0.0f, 0.15f));
            dangerLightFlash.Append(dangerLight.DOFade(0.6f, 0.15f));
            dangerLightFlash.Append(dangerLight.DOFade(0.0f, 0.15f));
        }
    }

    public void DangerLightOff() {
        dangerLightFlash.Kill();
        dangerLight.enabled = false;
    }
}
