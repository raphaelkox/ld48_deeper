using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class DepthCharge : MonoBehaviour
{
    public float explode_depth;
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < explode_depth) {
            Explode();
        }
    }

    void Explode() {
        LeanPool.Spawn(explosion, transform.position, Quaternion.identity);
        LeanPool.Despawn(gameObject);
    }
}
