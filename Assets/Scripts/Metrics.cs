using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class Metrics
{
    int collisionFrequency;

    private float startTime;
    private float duration;
    private bool isTimerRunning;


    private static Metrics instance;

    public static Metrics Instance
    {
        get
        {
            // Create the instance if it doesn't exist
            if (instance == null)
            {
                instance = new Metrics();
            }

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Reset the timer
    private void ResetTimer()
    {
        startTime = 0f;
        isTimerRunning = false;
    }


    // Call this method to start the timer
    public void StartTimer()
    {
        ResetTimer();
        startTime = Time.time;
        isTimerRunning = true;
    }

    // Call this method in the trigger event to calculate the duration
    public void CalculateDuration()
    {

        duration = Time.time;
        Debug.Log("Duration: " + duration + " seconds");

        ResetTimer();

    }

    public void incrementCollisionFrequency()
    {
        collisionFrequency++;
    }

    public void export()
    {
        CalculateDuration();

        List<string[]> dataToExport = new List<string[]>();
        string filePath = Path.Combine(Application.persistentDataPath, "exportedData.csv");
        Debug.Log("filePath : " + filePath);
       
        // Add header row if its a new file
        if (!File.Exists(filePath))
        {
            dataToExport.Add(new string[] { "Duration", "collisionNum" });
        }

        dataToExport.Add(new string[] { duration.ToString(), collisionFrequency.ToString() });

        CSVExporter exporter = new CSVExporter();
        exporter.ExportCSV(dataToExport, filePath);

    }

}
