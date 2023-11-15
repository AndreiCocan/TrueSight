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

    public bool exampleBool;
    
    
    public List<int> exampleList = new List<int>(); // The elements type must be Serializable
    
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
    
    // default message is a message with all motors at 0 + the end marker
    private byte[] getDefaultMessage()
    {
        return new byte[5] { 0, 0, 0, 0, Driver.EndMarker };
    }
    
    public void playVib()
    {
        driver.SetMessage(new byte[5] { vibIntensity1, vibIntensity2, vibIntensity3, vibIntensity4, Driver.EndMarker });
    }



    // example of a function that will play a vibration on one motor
    // this function is asynchronous, meaning that it will not block the main thread
    public void endVib()
    {
        driver.SetMessage(getDefaultMessage());
    }
}


// this is the editor script that will be used to display buttons in the inspector
[CustomEditor(typeof(VibrationManager_minimal))]
public class VibrationManagerEditor : Editor
{


    AnimationCurve curve1 = new AnimationCurve();
    AnimationCurve curve2 = new AnimationCurve();
    AnimationCurve curve3 = new AnimationCurve();
    AnimationCurve curve4 = new AnimationCurve();



    static float directionValue;



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

        directionValue = EditorGUILayout.Slider("Direction Slider", directionValue, -10, 10);

        curve1 = EditorGUILayout.CurveField(
            "Vibration en 1",
            curve1,
            Color.cyan,
            new Rect(-10, 0, 20, 255)
            );

        instance.vibIntensity1 = (byte) curve1.Evaluate(directionValue);

        curve2 = EditorGUILayout.CurveField(
            "Vibration en 2",
            curve2,
            Color.cyan,
            new Rect(-10, 0, 20, 255)
            );

        instance.vibIntensity2 = (byte) curve2.Evaluate(directionValue);

        curve3 = EditorGUILayout.CurveField(
            "Vibration en 3",
            curve3,
            Color.cyan,
            new Rect(-10, 0, 20, 255)
            );

        instance.vibIntensity3 = (byte) curve3.Evaluate(directionValue);

        curve4 = EditorGUILayout.CurveField(
            "Vibration en 4",
            curve4,
            Color.cyan,
            new Rect(-10, 0, 20, 255)
            );

        instance.vibIntensity4 = (byte) curve4.Evaluate(directionValue);

        if (GUILayout.Button("New setting"))
        {
            curve1 = new AnimationCurve();
            VibSettings vibSettings = VibSettings.CreateInstance(curve1, curve2, curve3, curve4);

            VibSettingsManager.SaveCurve(vibSettings);
        }

        if (GUILayout.Button("Load settings 1"))
        {
            curve1 = VibSettingsManager.Instance.vibSettingsList[0].vibIntensityCurves[0];
        }

    }
}
