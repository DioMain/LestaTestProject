using Unity.VisualScripting;
using UnityEngine;

public static class MathfPlus
{
    public static float Mod(float value, float mod)
    {
        float result = value % mod;

        if (result < 0)
            result = mod + result;

        return result;
    }
    public static int Mod(int value, int mod)
    {
        int result = value % mod;

        if (result < 0)
            result = mod + result;

        return result;
    }
}
