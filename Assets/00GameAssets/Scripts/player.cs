using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    const float stop_mass = 0.7055434f;
    const float up_mass = 0.63f;
    const float down_mass = 0.8f;
    public float speed;
    public Rigidbody2D rb;
    public float currentMass;
    public float external_pressure;
    public float internal_pressure;
    public float pressure_difference;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMass = stop_mass;
        rb.mass = currentMass;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Input.GetAxis("Vertical");
        var targetMass = dir == 0 ? stop_mass : dir < 0 ? down_mass : up_mass;
        currentMass = Mathf.MoveTowards(currentMass, targetMass, 0.001f);
        rb.mass = currentMass;
    
        var velY = rb.velocity.y;
        var velX = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(velX, velY);
    }
}