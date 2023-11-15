using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VibSettings : ScriptableObject
{
    public AnimationCurve[] vibIntensityCurves = new AnimationCurve[4];

    private static VibSettingsManager m_Instance;

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
        
        EditorUtility.SetDirty(vibSettings);
        AssetDatabase.SaveAssetIfDirty(vibSettings);

        return vibSettings;
    }

    public static VibSettings CreateInstance(VibSettings initialVibSettings)
    {
        VibSettings vibSettings = ScriptableObject.CreateInstance<VibSettings>();
        
        vibSettings.Init(initialVibSettings.vibIntensityCurves[0], initialVibSettings.vibIntensityCurves[1], initialVibSettings.vibIntensityCurves[2], initialVibSettings.vibIntensityCurves[3]);
        


        return vibSettings;
    }
}