using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureDiffMeter : MonoBehaviour
{
    SpriteRenderer pressure_meter;
    public Sprite very_low;
    public Sprite low;
    public Sprite center;
    public Sprite high;
    public Sprite very_high;

    public float center_thres;
    public float low_thres;
    public float high_thres;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        pressure_meter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.pressure_difference) < center_thres) {
            pressure_meter.sprite = center;
        }
        else if(player.pressure_difference < low_thres) {
            pressure_meter.sprite = very_low;
        }
        else if (player.pressure_difference < 0) {
            pressure_meter.sprite = low;
        }
        else if (player.pressure_difference > high_thres) {
            pressure_meter.sprite = very_high;
        }
        else if (player.pressure_difference > 0) {
            pressure_meter.sprite = high;
        }
    }
}
