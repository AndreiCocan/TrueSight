using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationCurveSave : ScriptableObject
{
   
    public List<List<AnimationCurve>> curves = new List<List<AnimationCurve>>();
    private static AnimationCurveSave m_Instance;

    public static AnimationCurveSave Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = AssetDatabase.LoadAssetAtPath<AnimationCurveSave>("Assets/Curves/curves");
            }

            return m_Instance;
        }
    }

    // use this to save AnimationCurve
    public static void SaveCurve(List<AnimationCurve> runtimeCurve)
    {
        List<AnimationCurve> savedCurveData = new List<AnimationCurve>();
        for(int i = 0; i < runtimeCurve.Count; i++)
        {
            savedCurveData.Add(new AnimationCurve(runtimeCurve[i].keys));
        }
        Instance.curves.Add(savedCurveData);
        EditorUtility.SetDirty(Instance);
        AssetDatabase.SaveAssetIfDirty(Instance);
    }
    
}
