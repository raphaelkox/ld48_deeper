using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

public class Radar_Sensor : MonoBehaviour
{
    public GameObject RadarPoint;
    public Transform RadarUI;
    public Transform player;
    public List<Transform> charges = new List<Transform>();
    public List<Transform> radar_points = new List<Transform>();
    public float scale;

    private void Update() {
        for (int i = 0; i < charges.Count - 1; i++) {
            var dist = charges[i].position - player.position;
            dist /= 5f;
            radar_points[i].localPosition = dist * scale;
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
