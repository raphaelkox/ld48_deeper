using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cockpit_window : MonoBehaviour
{
    SpriteRenderer window;
    public Gradient seaColors;
    public Player player;

    private void Start() {
        window = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var n = -player.transform.position.y / player.max_depth;

        window.color = seaColors.Evaluate(n);
    }
}
