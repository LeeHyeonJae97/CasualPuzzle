using UnityEngine;

public static class Random
{
    public static Vector2 insideUnitCircle => UnityEngine.Random.insideUnitCircle;
    public static Vector2 insideUnitRect { get { return new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)); } }
    public static Vector3 insideUnitRectXZ
    {
        get
        {
            var vec = insideUnitRect;

            return new Vector3(vec.x, 0, vec.y);
        }
    }
    public static Vector2 insideUnitSphere => UnityEngine.Random.insideUnitSphere;
    public static Vector2 onUnitRect
    {
        get
        {
            int random = UnityEngine.Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    return new Vector2(-1, UnityEngine.Random.value * 2 - 1);

                case 1:
                    return new Vector2(1, UnityEngine.Random.value * 2 - 1);

                case 2:
                    return new Vector2(UnityEngine.Random.value * 2 - 1, -1);

                case 3:
                    return new Vector2(UnityEngine.Random.value * 2 - 1, 1);

                default:
                    return Vector2.zero;
            }
        }
    }
    public static Vector3 onUnitRectXZ
    {
        get
        {
            var vec = onUnitRect;

            return new Vector3(vec.x, 0, vec.y);
        }
    }
    public static Vector3 onUnitSphere => UnityEngine.Random.onUnitSphere;
    public static Quaternion rotation => UnityEngine.Random.rotation;
    public static Quaternion rotationUniform => UnityEngine.Random.rotationUniform;
    public static UnityEngine.Random.State state => UnityEngine.Random.state;
    public static float value => UnityEngine.Random.value;

    public static bool CheckPercent(int value)
    {
        // need to check
        return Random.Range(0, 100) < value;
    }

    public static Color ColorHSV()
    {
        return UnityEngine.Random.ColorHSV();
    }

    public static Color ColorHSV(float hueMin, float hueMax)
    {
        return UnityEngine.Random.ColorHSV(hueMin, hueMax);
    }

    public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
    {
        return UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax);
    }

    public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
    {
        return UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
    }

    public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
    {
        return UnityEngine.Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);
    }

    public static void InitState(int seed)
    {
        UnityEngine.Random.InitState(seed);
    }

    public static int Range(int minInclusive, int maxExclusive)
    {
        return UnityEngine.Random.Range(minInclusive, maxExclusive);
    }

    public static float Range(float minInclusive, float maxInclusive)
    {
        return UnityEngine.Random.Range(minInclusive, maxInclusive);
    }
}