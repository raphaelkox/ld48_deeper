using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleParticle : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem particles;
    bool playing;

    private void Start() {
        particles = GetComponent<ParticleSystem>();
    }

    public void Play() {
        if (!playing) {
            particles.Play();
            playing = true;
        }
    }

    public void Stop() {
        particles.Stop();
        playing = false;
    }
}
