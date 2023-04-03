using UnityEngine;

public static class VectorExtension
{
    public static Vector2 SetX(this Vector2 vec, float x)
    {
        return new Vector2(x, vec.y);
    }

    public static Vector2 SetY(this Vector2 vec, float y)
    {
        return new Vector2(vec.x, y);
    }

    public static Vector3 SetX(this Vector3 vec, float x)
    {
        return new Vector3(x, vec.y, vec.z);
    }

    public static Vector3 SetY(this Vector3 vec, float y)
    {
        return new Vector3(vec.x, y, vec.z);
    }

    public static Vector3 SetZ(this Vector3 vec, float z)
    {
        return new Vector3(vec.x, vec.y, z);
    }

    public static Vector2Int SetX(this Vector2Int vec, int x)
    {
        return new Vector2Int(x, vec.y);
    }

    public static Vector2Int SetY(this Vector2Int vec, int y)
    {
        return new Vector2Int(vec.x, y);
    }

    public static Vector3Int SetX(this Vector3Int vec, int x)
    {
        return new Vector3Int(x, vec.y, vec.z);
    }

    public static Vector3Int SetY(this Vector3Int vec, int y)
    {
        return new Vector3Int(vec.x, y, vec.z);
    }

    public static Vector3Int SetZ(this Vector3Int vec, int z)
    {
        return new Vector3Int(vec.x, vec.y, z);
    }

    public static Vector2Int ToInt(this Vector2 vec)
    {
        return new Vector2Int((int)vec.x, (int)vec.y);
    }

    public static Vector3Int ToInt(this Vector3 vec)
    {
        return new Vector3Int((int)vec.x, (int)vec.y, (int)vec.z);
    }

    public static Vector2 Round(this Vector2 vec)
    {
        return new Vector2(Mathf.Round(vec.x), Mathf.Round(vec.y));
    }

    public static Vector3 Round(this Vector3 vec)
    {
        return new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));
    }

    public static Vector2 Ceil(this Vector2 vec)
    {
        return new Vector2(Mathf.Ceil(vec.x), Mathf.Ceil(vec.y));
    }

    public static Vector3 Ceil(this Vector3 vec)
    {
        return new Vector3(Mathf.Ceil(vec.x), Mathf.Ceil(vec.y), Mathf.Ceil(vec.z));
    }

    public static Vector2 Floor(this Vector2 vec)
    {
        return new Vector2(Mathf.Floor(vec.x), Mathf.Floor(vec.y));
    }

    public static Vector3 Floor(this Vector3 vec)
    {
        return new Vector3(Mathf.Floor(vec.x), Mathf.Floor(vec.y), Mathf.Floor(vec.z));
    }

    public static Vector3 XYToXZ(this Vector2 vec)
    {
        return new Vector3(vec.x, 0, vec.y);
    }

    public static Vector2 XZToXY(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }

    public static int Snap4(this Vector2 vec)
    {
        return Snap(vec, 4);
    }

    public static int Snap8(this Vector2 vec)
    {
        return Snap(vec, 8);
    }

    private static int Snap(Vector2 vec, int count)
    {
        return (Mathf.RoundToInt(Mathf.Atan2(vec.y, vec.x) / (2 * Mathf.PI / count)) + count) % count;
    }

    public static float Dst(this Vector2 from, Vector2 to)
    {
        return (from - to).magnitude;
    }

    public static float Dst(this Vector3 from, Vector3 to)
    {
        return (from - to).magnitude;
    }

    public static float SqrDst(this Vector2 from, Vector2 to)
    {
        return (from - to).sqrMagnitude;
    }

    public static float SqrDst(this Vector3 from, Vector3 to)
    {
        return (from - to).sqrMagnitude;
    }

    public static Vector2 Clamp(this Vector2 vec, Vector2 min, Vector2 max)
    {
        return new Vector2(Mathf.Clamp(vec.x, min.x, max.x), Mathf.Clamp(vec.y, min.y, max.y));
    }

    public static Vector3 Clamp(this Vector3 vec, Vector3 min, Vector3 max)
    {
        return new Vector3(Mathf.Clamp(vec.x, min.x, max.x), Mathf.Clamp(vec.y, min.y, max.y), Mathf.Clamp(vec.z, min.z, max.z));
    }
}
