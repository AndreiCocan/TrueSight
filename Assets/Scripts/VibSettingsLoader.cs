using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class VibSettingsLoader 
{

    private static VibSettingsLoader instance;

    

    private int currentAssetIndex;


    public List<VibSettings> assetList;

    private string assetFolderPath = "Assets/VibSettings/";

    // Private constructor to prevent instantiation
    private VibSettingsLoader() { }

    // Public method to get the instance of the class
    public static VibSettingsLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new VibSettingsLoader();
                instance.Init();
            }
            return instance;
        }
    }

    //Init function
    private void Init()
    {
        currentAssetIndex = 0;
        assetList = LoadAssetsFromFiles(assetFolderPath);
        if(0 >= assetList.Count)
        {
            NewAsset();
        }
    }

    public int CurrentAssetIndex
    {
        get {
            return currentAssetIndex;
        }
    }

    //Load all asset at path
    private static List<VibSettings> LoadAssetsFromFiles(string folderPath)
    {
        string[] assetGuids = AssetDatabase.FindAssets("t:" + typeof(VibSettings).Name, new[] { folderPath });

        List<VibSettings> loadedAssets = new List<VibSettings>();

        foreach (var assetGuid in assetGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            VibSettings loadedAsset = AssetDatabase.LoadAssetAtPath<VibSettings>(assetPath);

            if (loadedAsset != null)
            {
                loadedAssets.Add(loadedAsset);
            }
        }

        return loadedAssets;
    }

    public void SetAssetIndex(int index)
    {
        if(0 <= index && assetList.Count > index)
        {
            currentAssetIndex = index;
            return;
        }
        Debug.LogError("Wrong Settings Index. Can't change current settings Index");
    }

    public VibSettings NewAsset()
    {
        VibSettings asset = VibSettings.CreateInstance();
       
        AssetDatabase.CreateAsset(asset, assetFolderPath + assetList.Count + ".asset");
        
        currentAssetIndex = assetList.Count;
        assetList.Add(asset);
        
        return GetCurrentAsset();
    }

    public VibSettings GetCurrentAsset()
    {
        return assetList[currentAssetIndex];
    }

    public int GetNumberofAssets()
    {
        return assetList.Count;
    }

    public void SaveAsset(VibSettings modifiyedVibSettings)
    {
        assetList[currentAssetIndex] = modifiyedVibSettings;
        EditorUtility.SetDirty(assetList[currentAssetIndex]);
        AssetDatabase.SaveAssetIfDirty(assetList[currentAssetIndex]);
    }
}
