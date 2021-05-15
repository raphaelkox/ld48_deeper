using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PressureValveMobileUI : MonoBehaviour
{
    Image image;
    Vector2 start_pos;
    public float min_offset;
    public RectTransform pivot;

    public static float valve_value;

    private void Start() {
        
    }

    public void OnDragBegin(BaseEventData e) {
        var pe = e as PointerEventData;
        start_pos = pe.position;
        Debug.Log("Drag On!");
    }

    public void OnDrag(BaseEventData e) {
        var pe = e as PointerEventData;

        Debug.Log("point: " + Mathf.Abs(pe.position.x - start_pos.x));
        if(Mathf.Abs(pe.position.x - start_pos.x) < min_offset) {
            valve_value = 0f;
            pivot.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        }
        else if(pe.position.x < start_pos.x) {
            valve_value = -1f;
            pivot.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
        }
        else {
            valve_value = 1f;
            pivot.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
        }
    }

    public void OnDragEnd(BaseEventData e) {
        Debug.Log("Drag Off!");
        valve_value = 0F;
    }
}
