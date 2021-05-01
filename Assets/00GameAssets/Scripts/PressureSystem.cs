using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PressureSystem
{
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

    public static void Update(float depth) {
        depth_normalized = -depth / max_depth;
        external_pressure = depth_normalized * max_pressure;
        pressure_difference = external_pressure - internal_pressure;

        var pressure_control = Input.GetAxis("Vertical");
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
