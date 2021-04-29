using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cockpit_fx : MonoBehaviour
{
    public float shake_duration;
    public float shake_strenght;
    public int shake_vibrato;

    public void Shake() {
        transform.DOShakePosition(shake_duration, shake_strenght, shake_vibrato);
    }
}