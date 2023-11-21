using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VibSettings : ScriptableObject
{
    public AnimationCurve[] vibIntensityCurves = new AnimationCurve[4];

    public void Init(AnimationCurve curve1, AnimationCurve curve2, AnimationCurve curve3, AnimationCurve curve4)
    {
        vibIntensityCurves[0] = curve1;
        vibIntensityCurves[1] = curve2;
        vibIntensityCurves[2] = curve3;
        vibIntensityCurves[3] = curve4;
    }

    public static VibSettings CreateInstance(AnimationCurve curve1, AnimationCurve curve2, AnimationCurve curve3, AnimationCurve curve4)
    {
        VibSettings vibSettings = ScriptableObject.CreateInstance<VibSettings>();
        
        vibSettings.Init(curve1, curve2, curve3, curve4);

        return vibSettings;
    }

    public static VibSettings CreateInstance()
    {
        VibSettings vibSettings = ScriptableObject.CreateInstance<VibSettings>();

        vibSettings.Init(new AnimationCurve(), new AnimationCurve(), new AnimationCurve(), new AnimationCurve());

        return vibSettings;
    }
}