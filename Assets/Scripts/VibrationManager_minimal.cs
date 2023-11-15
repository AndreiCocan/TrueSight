using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using hapticDriver;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class VibrationManager_minimal: MonoBehaviour
{
    public string devicePort = "COM5"; // Check which port is used on your system !
    private Timer callbackTimer;
    private Driver driver;

    [Range(0,255)]
    public byte vibIntensity1 = 200;

    [Range(0, 255)]
    public byte vibIntensity2 = 0;

    [Range(0, 255)]
    public byte vibIntensity3 = 0;

    [Range(0, 255)]
    public byte vibIntensity4 = 0;

    [Range(0, 255)]
    public byte[] vibIntensity = new byte[4];

    public bool exampleBool;

    public float directionValue;

    public AnimationCurve[] vibCurves = new AnimationCurve[4];

    // the Start function is called when a script is enabled
    private void Start()
    {
        // create a new driver instance
        driver = new Driver(devicePort);
        // create a new timer that will call the emitterCallback function every 40ms
        callbackTimer = new Timer(emitterCallback, null, 0, 40);
    }

    private void Update()
    {
        evaluateVibCurve();
        playVib();   
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
        for(int i = 0; i < vibCurves.Length; i++)
        {
            vibIntensity[i] = (byte) vibCurves[i].Evaluate(directionValue);
        }
    }
    public void playVib()
    {
        driver.SetMessage(new byte[5] { vibIntensity[0], vibIntensity[1], vibIntensity[2], vibIntensity[3], Driver.EndMarker });
    }

    public void endVib()
    {
        driver.SetMessage(new byte[5] { 0, 0, 0, 0, Driver.EndMarker });
    }
}


// this is the editor script that will be used to display buttons in the inspector
[CustomEditor(typeof(VibrationManager_minimal))]
public class VibrationManagerEditor : Editor
{

    // instance is the object that is being edited/displayed
    private VibrationManager_minimal instance;

    private void OnEnable()
    {
        instance = (VibrationManager_minimal)target;
    }

    // this function is called when the inspector is drawn
    // this is where we can add buttons
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // draw the default inspector

        instance.directionValue = EditorGUILayout.Slider("Direction Slider", instance.directionValue, -180, 180);

        instance.vibCurves[0] = EditorGUILayout.CurveField(
            "Vibration en 1",
            instance.vibCurves[0],
            Color.cyan,
            new Rect(-180, 0, 360, 255)
            );

        instance.vibCurves[1] = EditorGUILayout.CurveField(
            "Vibration en 2",
            instance.vibCurves[1],
            Color.cyan,
            new Rect(-180, 0, 360, 255)
            );

        instance.vibCurves[2] = EditorGUILayout.CurveField(
            "Vibration en 3",
            instance.vibCurves[2],
            Color.cyan,
            new Rect(-180, 0, 360, 255)
            );

        instance.vibCurves[3] = EditorGUILayout.CurveField(
            "Vibration en 4",
            instance.vibCurves[3],
            Color.cyan,
            new Rect(-180, 0, 360, 255)
            );


        if (GUILayout.Button("New setting"))
        {
            for(int i = 0; i < instance.vibCurves.Length; i++)
            {
                instance.vibCurves[i] = new AnimationCurve();
            }

            VibSettings vibSettings = VibSettings.CreateInstance(instance.vibCurves[0], instance.vibCurves[1], instance.vibCurves[2], instance.vibCurves[3]);

            VibSettingsManager.SaveCurve(vibSettings);
        }

        if (GUILayout.Button("Load settings 1"))
        {
            for (int i = 0;i < instance.vibCurves.Length; i++)
            {
                instance.vibCurves[i] = VibSettingsManager.Instance.vibSettingsList[0].vibIntensityCurves[i];
            }
        }

    }
}
