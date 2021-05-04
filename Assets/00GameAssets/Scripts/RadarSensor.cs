using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

public class RadarSensor : MonoBehaviour
{
    public GameObject RadarPoint;
    public Transform RadarUI;
    public Transform player;
    public List<Transform> charges = new List<Transform>();
    public List<Transform> radar_points = new List<Transform>();
    public float scale;
    public float radius;

    private void Start() {
        radius = GetComponent<CircleCollider2D>().radius / 2f;
    }

    private void Update() { 
        for (int i = 0; i < charges.Count; i++) {
            var dist = charges[i].position - player.position;
            var dir = dist.normalized;
            var ratio = dist.magnitude / radius;
            radar_points[i].localPosition = dir * ratio * scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("charge")){
            charges.Add(collision.transform);
            radar_points.Add(LeanPool.Spawn(RadarPoint, RadarUI).transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("charge")) {
            charges.Remove(collision.transform);
            LeanPool.Despawn(radar_points[0]);
            radar_points.RemoveAt(0);
        }
    }
}
