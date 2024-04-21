using System;
using UnityEngine;

public enum FloatType
{
    MasterVolume,
    SfxVolume,
    MusicVolume,
    Money
}

public enum IntType
{
    HighestStreak
}

public enum BoolType
{
    FirstPlay
}

public static class SerializeManager
{
    public static void SetFloat(FloatType type, float value)
    {
        PlayerPrefs.SetFloat(type.ToString(), value);
    }

    public static float GetFloat(FloatType type)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
            return PlayerPrefs.GetFloat(type.ToString());
        else
        {
            PlayerPrefs.SetFloat(type.ToString(), 1f);
            return 1f;
        }
    }

    public static void SetInt(IntType type, int value)
    {
        PlayerPrefs.SetInt(type.ToString(), value);
    }

    public static int GetInt(IntType type)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
            return PlayerPrefs.GetInt(type.ToString());
        else
        {
            PlayerPrefs.SetInt(type.ToString(), 0);
            return 0;
        }
    }

    public static void SetBool(BoolType type, bool value)
    {
        PlayerPrefs.SetInt(type.ToString(), Convert.ToInt32(value));
    }

    public static bool GetBool(BoolType type)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
            return Convert.ToBoolean(PlayerPrefs.GetInt(type.ToString()));
        else
        {
            PlayerPrefs.SetInt(type.ToString(), Convert.ToInt32(true));
            return true;
        }
    }
}