using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Periscope : MonoBehaviour
{
    public float energy;
    public float max_energy;
    public float energy_recharge;
    public float periscope_cost;
    public bool periscope_active;

    public RectTransform periscope_ui;
    public Image energy_bar;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (energy > periscope_cost && !periscope_active) {
                periscope_active = true;
                periscope_ui.gameObject.SetActive(periscope_active);
                Debug.Log("Enable");
            }
            else {
                periscope_active = false;
                periscope_ui.gameObject.SetActive(periscope_active);
                Debug.Log("Disable");
            }
        }

        if (periscope_active) {
            energy -= periscope_cost * Time.deltaTime;

            if(energy <= 0) {
                periscope_active = false;
                periscope_ui.gameObject.SetActive(periscope_active);
                energy = 0;
            }
        }
        else {
            energy += energy_recharge * Time.deltaTime;
            if (energy > max_energy) energy = max_energy;
        }

        energy_bar.fillAmount = energy / max_energy;
    }
}