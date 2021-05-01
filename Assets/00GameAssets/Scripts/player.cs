using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static float external_x;
    public static float external_x_damp;

    public static float horizontal_dir;
    public static float vertical_dir;
    public static float horizontal_vel;
    public static float vertical_vel;

    public float speed;
    public Rigidbody2D rb;
    public float currentMass;
    
    public UnityEvent OnMoveForward;
    public UnityEvent OnMoveBackward;
    public UnityEvent OnMoveNeutral;
    public UnityEvent OnExplosion;

    public RectTransform depth_indicator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMass = PressureSystem.stop_mass;
        rb.mass = currentMass;

        PressureSystem.external_pressure = -transform.position.y / PressureSystem.max_depth * PressureSystem.max_pressure;
        PressureSystem.internal_pressure = PressureSystem.external_pressure;
    }

    // Update is called once per frame
    void Update()
    {
        PressureSystem.Update(transform.position.y);

//        depth_indicator.anchoredPosition = new Vector2(0, (PressureSystem.depth_normalized * 50f) * -1f);
        currentMass = Mathf.MoveTowards(currentMass, PressureSystem.CalculateMass(vertical_dir), 0.001f);
        rb.mass = currentMass;
    
        vertical_vel = rb.velocity.y;
        vertical_dir = PressureSystem.GetVerticalDir();
        horizontal_dir = Input.GetAxis("Horizontal");
        horizontal_vel = horizontal_dir * speed;

        if (horizontal_vel == 0) {
            OnMoveNeutral?.Invoke();
        }
        else {
            if (horizontal_vel > 0) {
                OnMoveForward?.Invoke();
            }
            else {
                OnMoveBackward?.Invoke();
            }
        }

        horizontal_vel += external_x;

        if(Mathf.Abs(external_x) > 0) {
            external_x -= Mathf.Sign(external_x) * external_x_damp * Time.deltaTime;
        }        

        rb.velocity = new Vector2(horizontal_vel, vertical_vel);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("explosion")) {
            var explosion = collision.collider.GetComponent<ChargeExplosion>();
            var force = explosion.end_scale - explosion.transform.localScale.x; 

            if(collision.GetContact(0).point.x > transform.position.x) {
                external_x = -force;
            }
            else {
                external_x = force;
            }

            OnExplosion?.Invoke();
        }
    }
}
