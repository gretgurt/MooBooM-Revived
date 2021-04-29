using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ease {
    private const float c1 = 1.70158f;
    private const float c2 = c1 * 1.525f;
    private const float c3 = c1 + 1f;
    private const float c4 = (2f * Mathf.PI) / 3f;
    private const float n1 = 7.5625f;
    private const float d1 = 2.75f;

    public static float EaseInQuad(float x) {
        return x * x;
    }

    public static float EaseInQuart(float x) {
        return x * x * x * x;
    }

    public static float EaseInQuint(float x) {
        return x * x * x * x * x;
    }

    public static float EaseInCirc(float x) {
        return 1f - Mathf.Sqrt(1f - Mathf.Pow(x, 2f));
    }

    public static float EaseInExpo(float x) {
        return x == 0 ? 0 : Mathf.Pow(2f, 10f * x - 10f);
    }

    public static float EaseInBack(float x) {
        return c3 * x * x * x - c1 * x * x;
    }

    public static float EaseOutCirc(float x) {
        return Mathf.Sqrt(1f - Mathf.Pow(x - 1f, 2f));
    }

    public static float EaseOutQuint(float x) {
        return 1 - Mathf.Pow(1f - x, 5f);
    }

    public static float EaseOutElastic(float x) {
        return x == 0
                ? 0
                : x == 1
                        ? 1
                        : Mathf.Pow(2f, -10f * x)
                        * Mathf.Sin((x * 10f - 0.75f)
                        * c4) + 1;
    }

    public static float EaseOutExpo(float x) {
        return x == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * x);
    }

    public static float EaseOutBack(float x) {
        return 1f + c3 * Mathf.Pow(x - 1f, 3f) + c1
                * Mathf.Pow(x - 1f, 2f);
    }

    public static float EaseOutBounce(float x)  {
        if (x < 1f / d1) {
            return n1 * x * x;
        } else if (x < 2 / d1) {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        } else if (x < 2.5 / d1) {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        } else {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }

    public static float EaseInBounce(float x) {
        return 1f - EaseOutBounce(1f - x);
    }

    public static float EaseInOutCirc(float x) {
        return x < 0.5f
                ? (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f
                : (Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
    }

    public static float EaseInOutQuint(float x) {
        return x < 0.5f
                ? 16f * x * x * x * x * x
                : 1f - Mathf.Pow(-2f * x + 2f, 5f) / 2f;
    }

    public static float EaseInOutCubic(float x) {
        return x < 0.5f
                ? 4f * x * x * x
                : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
    }

    public static float EaseInOutSine(float x) {
        return -(Mathf.Cos(Mathf.PI * x) - 1f) / 2f;
    }

    public static float EaseInOutBack(float x) {
        return x < 0.5f
                ? (Mathf.Pow(2f * x, 2f)
                        * ((c2 + 1f) * 2f * x - c2)) / 2f
                : (Mathf.Pow(2f * x - 2f, 2f)
                        * ((c2 + 1f) * (x* 2f - 2f) + c2) + 2f) / 2f;
    }
}