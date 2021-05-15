using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressureDiffMeter : MonoBehaviour
{
    Image pressure_meter;
    public Sprite very_low;
    public Sprite low;
    public Sprite center;
    public Sprite high;
    public Sprite very_high;

    public float center_thres;
    public float low_thres;
    public float high_thres;

    // Start is called before the first frame update
    void Start()
    {
        pressure_meter = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        low_thres = PressureSystem.explode_test;
        high_thres = PressureSystem.implode_test;

        if (Mathf.Abs(PressureSystem.pressure_difference) < center_thres) {
            pressure_meter.sprite = center;
        }
        else if(PressureSystem.pressure_difference < low_thres) {
            pressure_meter.sprite = very_low;
        }
        else if (PressureSystem.pressure_difference < 0) {
            pressure_meter.sprite = low;
        }
        else if (PressureSystem.pressure_difference > high_thres) {
            pressure_meter.sprite = very_high;
        }
        else if (PressureSystem.pressure_difference > 0) {
            pressure_meter.sprite = high;
        }
    }
}
