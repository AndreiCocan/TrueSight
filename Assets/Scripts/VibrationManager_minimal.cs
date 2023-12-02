using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using hapticDriver;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class VibrationManager_minimal: MonoBehaviour
{
    public string devicePort = "COM5"; // Check which port is used on your system !
    private Timer callbackTimer;
    private Driver driver;
    public bool isArrived = false;

    [Range(0, 255)]
    public byte[] vibIntensity = new byte[4];

    public float directionValue;


    public VibSettings currentVibSetting;

    // the Start function is called when a script is enabled
    private void Start()
    {
        // create a new driver instance
        driver = new Driver(devicePort);
        driver.SetVerbose(false);
        // create a new timer that will call the emitterCallback function every 40ms
        callbackTimer = new Timer(emitterCallback, null, 0, 40);
    }

    private void Update()
    {
        if (!isArrived)
        {
            playVib();
        }
    }

    private void OnApplicationQuit()
    {
        endVib();
    }

    // this function is called every 40ms
    private void emitterCallback(object state)
    {
        //driver.SetMessage(getDefaultMessage());
        driver.SendMessage();
    }
    
    private void evaluateVibCurve()
    {
        for(int i = 0; i < currentVibSetting.vibIntensityCurves.Length; i++)
        {
            vibIntensity[i] = (byte) currentVibSetting.vibIntensityCurves[i].Evaluate(directionValue);
        }
    }
    public void playVib()
    {
        evaluateVibCurve();
        driver.SetMessage(new byte[5] { vibIntensity[0], vibIntensity[1], vibIntensity[2], vibIntensity[3], Driver.EndMarker });
    }

    public void endVib()
    {
        driver.SetMessage(new byte[5] { 0, 0, 0, 0, Driver.EndMarker });
    }

    public async void sendArrivedMsg()
    {
        isArrived = true; // Set the flag to start the arrived sequence
        for (int i = 0; i < 3; i++)
        {
            driver.SetMessage(new byte[5] { 50, 50, 50, 50, Driver.EndMarker });
            await Task.Delay(200);
            endVib();
            await Task.Delay(200);
        }

    }

}


// this is the editor script that will be used to display buttons in the inspector
[CustomEditor(typeof(VibrationManager_minimal))]
public class VibrationManagerEditor : Editor
{

    // instance is the object that is being edited/displayed
    private VibrationManager_minimal instance;

    private VibSettingsLoader vslInstance;

    private void OnEnable()
    {
        instance = (VibrationManager_minimal)target;
        vslInstance = VibSettingsLoader.Instance;
    }

    // this function is called when the inspector is drawn
    // this is where we can add buttons
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI(); // draw the default inspector

        instance.directionValue = EditorGUILayout.Slider("Direction Slider", instance.directionValue, -180, 180);

        for(int i = 0; i < instance.currentVibSetting.vibIntensityCurves.Length; i++)
        {
            instance.currentVibSetting.vibIntensityCurves[i] = EditorGUILayout.CurveField(
                "Vibration en " + i,
                instance.currentVibSetting.vibIntensityCurves[i],
                Color.cyan,
                new Rect(-180, 0, 360, 255)
                );
        }

        if (GUILayout.Button("New Settings"))
        {
            instance.currentVibSetting = vslInstance.NewAsset();
        }

        if (GUILayout.Button("Save Current Settings"))
        {
            vslInstance.SaveAsset(instance.currentVibSetting);
        }

        vslInstance.SetAssetIndex(EditorGUILayout.IntPopup("Select Settings", vslInstance.CurrentAssetIndex, GenerateChoicesArray(vslInstance.GetNumberofAssets()), GenerateIndexArray(vslInstance.GetNumberofAssets())));
        instance.currentVibSetting = vslInstance.GetCurrentAsset();

    }


    private string[] GenerateChoicesArray(int count)
    {
        string[] choices = new string[count];
        for (int i = 0; i < count; i++)
        {
            choices[i] = "Settings " + (i + 1).ToString();
        }
        return choices;
    }

    private int[] GenerateIndexArray(int count)
    {
        int[] indices = new int[count];
        for (int i = 0; i < count; i++)
        {
            indices[i] = i;
        }
        return indices;
    }
}
