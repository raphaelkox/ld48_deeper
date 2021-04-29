using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;

public class EnemyShip : MonoBehaviour
{
    public GameObject DepthCharge;
    public float delay_min;
    public float delay_max;

    bool moving;
    public Transform player;
    public Ease easing;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        var delay = Random.Range(delay_min, delay_max);
        Invoke("Spawn", delay);
    }

    void Spawn() {
        var depthcharge = LeanPool.Spawn(DepthCharge, transform.position, Quaternion.identity);
        depthcharge.GetComponent<DepthCharge>().explode_depth = player.position.y + 0.6f;
        var delay = Random.Range(delay_min, delay_max);
        Invoke("Spawn", delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving) {
            var time = Mathf.Abs(transform.position.x - player.position.x) / speed;
            transform.DOMoveX(player.position.x, time).SetEase(easing).OnComplete(() => moving = false);
            moving = true;
        }
    }
}
