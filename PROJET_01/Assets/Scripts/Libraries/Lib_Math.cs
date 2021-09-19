using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lib_Math
{
    static class Easing
    {
        public static float CubicIn(float x)
        {
            return x*x*x;
        }

        public static float CubicOut(float x)
        {
            int i = (int)(x / Mathf.Abs(x));
            return i * (1 - Mathf.Pow(1 - Mathf.Abs(x), 3));
        }

        public static float SineIn(float x)
        {
            int i = (int)(x / Mathf.Abs(x));
            return i * (1 - Mathf.Cos((x * Mathf.PI) / 2));
        }

        public static float SineOut(float x)
        {
            return Mathf.Sin((x * Mathf.PI) / 2);
        }

        public static float CircleIn(float x)
        {
            int i = (int)(x / Mathf.Abs(x));
            return i * (1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2)));
        }

        public static float CircleOut(float x)
        {
            int i = (int)(x / Mathf.Abs(x));
            return i * (Mathf.Sqrt(1 - Mathf.Pow((Mathf.Abs(x) - 1), 2)));
        }
    }
}


