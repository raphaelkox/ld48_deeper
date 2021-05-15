using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PressureSystem
{
    public static event Action OnDangerActivate;
    public static event Action OnDangerDeactivate;

    public static float max_pressure = 110f;
    public static float max_depth = 32f;

    public static float up_mass = 0.53f;
    public static float down_mass = 0.9f;
    public static float stop_mass = 0.7055434f;

    public static float external_pressure;
    public static float internal_pressure;
    public static float pressure_difference;
    public static float pressure_max_delta = 10f;
    public static float depth_normalized;

    public static float explode_pressure_max_top = -100f;
    public static float explode_pressure_factor = 100f;
    public static float explode_test;
    public static float implode_pressure_max_top = 100f;
    public static float implode_pressure_factor = 80f;
    public static float implode_test;

    public static bool in_danger;
    public static bool last_danger;
    public static float danger_delay = 3f;
    public static float danger_timer = 0f;

    public static void Update(float depth) { 

        depth_normalized = -depth / max_depth;
        external_pressure = depth_normalized * max_pressure;
        pressure_difference = external_pressure - internal_pressure;        

        in_danger = false;

        //explode
        if (pressure_difference < 0) {
            explode_test = explode_pressure_max_top - (depth_normalized * explode_pressure_factor);
            if (pressure_difference < explode_test) {
                in_danger = true;
            }
        }
        else if (pressure_difference > 0) {
            implode_test = implode_pressure_max_top - (depth_normalized * implode_pressure_factor);
            if (pressure_difference > implode_test) {
                in_danger = true;
            }
        }

        if(!last_danger && in_danger) {
            danger_timer = danger_delay;
            last_danger = true;
            OnDangerActivate?.Invoke();
        }
        else if(last_danger && !in_danger) {
            last_danger = false;
            OnDangerDeactivate?.Invoke();
        }

        if (last_danger) {
            danger_timer -= Time.deltaTime;

            if(danger_timer <= 0f) {
                Debug.Log("You Died");
                Time.timeScale = 0f;
            }
        }

        var pressure_control = Input.GetAxis("Vertical") + PressureValveMobileUI.valve_value;
        pressure_control = Mathf.Clamp(pressure_control, -1f, 1f);
        internal_pressure += pressure_control * pressure_max_delta * Time.deltaTime;
        internal_pressure = Mathf.Clamp(internal_pressure, 0f, max_pressure * 2f);
    }

    public static float CalculateMass(float vertical_dir) {
        return vertical_dir == 0 ? stop_mass : vertical_dir < 0 ? down_mass : up_mass;
    }

    public static float GetVerticalDir() {
        return Mathf.Abs(pressure_difference) < 1f ? 0f : external_pressure < internal_pressure ? 1f : -1f;
    }
}
