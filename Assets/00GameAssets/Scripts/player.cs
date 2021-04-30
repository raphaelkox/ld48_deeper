using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float max_pressure = 110f;
    public float max_depth = 32f;

    const float stop_mass = 0.7055434f;
    //const float up_mass = 0.63f;
    //const float down_mass = 0.8f;
    const float up_mass = 0.53f;
    const float down_mass = 0.9f;
    public float speed;
    public Rigidbody2D rb;
    public float currentMass;
    public float external_pressure;
    public float internal_pressure;
    public float pressure_difference;
    public float pressure_max_delta;

    public UnityEvent OnMoveForward;
    public UnityEvent OnMoveBackward;
    public UnityEvent OnMoveNeutral;
    public UnityEvent OnExplosion;

    public float externalX;
    public float externalXdamp;

    public RectTransform depth_indicator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMass = stop_mass;
        rb.mass = currentMass;

        external_pressure = -transform.position.y / max_depth * max_pressure;
        internal_pressure = external_pressure;
    }

    // Update is called once per frame
    void Update()
    {
        var depth_normalized = -transform.position.y / max_depth;
        depth_indicator.anchoredPosition = new Vector2(0, (depth_normalized * 50f) * -1f);
        external_pressure = depth_normalized * max_pressure;
        pressure_difference = external_pressure - internal_pressure;

        var pressure_control = Input.GetAxis("Vertical");
        internal_pressure += pressure_control * pressure_max_delta * Time.deltaTime;
        internal_pressure = Mathf.Clamp(internal_pressure, 0f, max_pressure * 2f);

        var dir = Mathf.Abs(pressure_difference) < 1f ? 0f : external_pressure < internal_pressure ? 1f : -1f;
        var targetMass = dir == 0 ? stop_mass : dir < 0 ? down_mass : up_mass;
        currentMass = Mathf.MoveTowards(currentMass, targetMass, 0.001f);
        rb.mass = currentMass;
    
        var velY = rb.velocity.y;
        var velX = Input.GetAxis("Horizontal") * speed;

        if (velX == 0) {
            OnMoveNeutral?.Invoke();
        }
        else {
            if (velX > 0) {
                OnMoveForward?.Invoke();
            }
            else {
                OnMoveBackward?.Invoke();
            }
        }

        velX += externalX;

        if(Mathf.Abs(externalX) > 0) {
            externalX -= Mathf.Sign(externalX) * externalXdamp * Time.deltaTime;
        }        

        rb.velocity = new Vector2(velX, velY);       
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("explosion")) {
            var explosion = collision.collider.GetComponent<Charge_explosion>();
            var force = explosion.end_scale - explosion.transform.localScale.x; 

            if(collision.GetContact(0).point.x > transform.position.x) {
                externalX = -force;
            }
            else {
                externalX = force;
            }

            OnExplosion?.Invoke();
        }
    }
}
