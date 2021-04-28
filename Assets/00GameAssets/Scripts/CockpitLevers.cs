using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ANIM_STATE
{
    NEUTRAL,
    FORWARD,
    BACKWARD
}

public class CockpitLevers : MonoBehaviour
{
    public Ease easingIn;
    public Ease easingOut;
    public float EaseInTime;
    public float EaseOutTime;

    ANIM_STATE anim = ANIM_STATE.NEUTRAL;

    public void SetFoward() {
        if (anim == ANIM_STATE.FORWARD) return;
        transform.DOMoveY(1f, EaseInTime).SetEase(easingIn);
        anim = ANIM_STATE.FORWARD;
    }

    public void SetBackward() {
        if (anim == ANIM_STATE.BACKWARD) return;
        transform.DOMoveY(-1f, EaseInTime).SetEase(easingIn);
        anim = ANIM_STATE.BACKWARD;
    }

    public void SetNeutral() {
        if (anim == ANIM_STATE.NEUTRAL) return;
        transform.DOMoveY(0f, EaseOutTime).SetEase(easingOut);
        anim = ANIM_STATE.NEUTRAL;
    }
}
