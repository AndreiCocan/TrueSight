using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class VibSettingsManager : ScriptableObject
{

    public List<VibSettings> vibSettingsList = new List<VibSettings>();

    private static VibSettingsManager m_Instance;

    public static VibSettingsManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = AssetDatabase.LoadAssetAtPath<VibSettingsManager>("Assets/VibSettings/VibSettingsManager.asset");
            }

            if(m_Instance == null)
            {
                m_Instance = ScriptableObject.CreateInstance<VibSettingsManager>();
     
                AssetDatabase.CreateAsset(m_Instance, "Assets/VibSettings/VibSettingsManager.asset");
            }
            return m_Instance;
        }
    }

    // use this to save AnimationCurve
    public static void SaveCurve(VibSettings settings)
    {
        VibSettings saveSettings = VibSettings.CreateInstance(settings);
        AssetDatabase.CreateAsset(saveSettings, "Assets/VibSettings/VibSettings/" + Instance.vibSettingsList.Count+".asset");
        Instance.vibSettingsList.Add(saveSettings);
        EditorUtility.SetDirty(Instance);
        AssetDatabase.SaveAssetIfDirty(Instance);
    }
    
}


