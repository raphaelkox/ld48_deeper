using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;

public class Charge_explosion : MonoBehaviour
{
    Vector3 start_scale = new Vector3(0.1f, 0.1f, 1f);
    public float end_scale = 3f;

    public Ease easing;
    public float explode_time;
    public float destroy_delay;

    void OnEnable()
    {
        transform.localScale = start_scale;
        transform.DOScale(end_scale, explode_time).SetEase(easing).OnComplete(() => LeanPool.Despawn(gameObject, destroy_delay));
    }
}
