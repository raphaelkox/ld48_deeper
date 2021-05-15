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
    public RectTransform periscope_chain;
    public Image energy_bar;

    private void Start() {
        periscope_chain = GetComponent<RectTransform>();
    }

    public void Toggle() {
        Debug.Log("Toggle Periscope");
        if (energy > periscope_cost && !periscope_active) {
            periscope_active = true;
            periscope_ui.gameObject.SetActive(periscope_active);
            Debug.Log("Enable");
            var sq = DOTween.Sequence();
            sq.Append(periscope_chain.DOAnchorPosY(periscope_chain.anchoredPosition.y - 30f, 0.2f).SetEase(Ease.OutExpo));
            sq.Append(periscope_chain.DOAnchorPosY(periscope_chain.anchoredPosition.y, 0.1f).SetEase(Ease.OutExpo));
        }
        else {
            periscope_active = false;
            periscope_ui.gameObject.SetActive(periscope_active);
            Debug.Log("Disable");
            var sq = DOTween.Sequence();
            sq.Append(periscope_chain.DOAnchorPosY(periscope_chain.anchoredPosition.y - 30f, 0.2f).SetEase(Ease.OutExpo));
            sq.Append(periscope_chain.DOAnchorPosY(periscope_chain.anchoredPosition.y, 0.1f).SetEase(Ease.OutExpo));
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Toggle();
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
