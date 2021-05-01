using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cockpit_window : MonoBehaviour
{
    SpriteRenderer window;
    public Gradient seaColors;

    private void Start() {
        window = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        window.color = seaColors.Evaluate(PressureSystem.depth_normalized);
    }
}
