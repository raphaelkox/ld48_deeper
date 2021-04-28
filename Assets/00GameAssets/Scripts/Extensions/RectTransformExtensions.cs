using UnityEngine;
public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left) {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right) {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top) {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom) {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    public static Vector2 NormalizedPosition(this RectTransform rt, Vector2 position){
        var pos = rt.InverseTransformPoint(position);
        return pos / (rt.sizeDelta / 2f);
    }
}
