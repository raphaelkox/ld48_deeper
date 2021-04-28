using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumericExtensions
{
    public static float Step(this float value, float increment){
        var step = value / increment;
        return Mathf.Round(step);
    }

    public static float Snap(this float value, float increment){
        var step = value.Step(increment);
        return step * increment;
    }
}
