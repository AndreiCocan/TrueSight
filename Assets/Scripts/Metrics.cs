using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class Metrics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*

    public void export()
    {
        List<string[]> dataToExport = new List<string[]>();
        dataToExport.Add(new string[] { "Header1", "Header2", "Header3" });
        dataToExport.Add(new string[] { "Row1Col1", "Row1Col2", "Row1Col3" });
        // Add more rows as needed

        string filePath = Path.Combine(Application.persistentDataPath, "exportedData.csv");
        Debug.Log("filePath : "+filePath);

        CSVExporter exporter = new CSVExporter();
        exporter.ExportCSV(dataToExport, filePath);

    }

    public class CSVExporter : MonoBehaviour
    {
        public void ExportCSV(List<string[]> data, string filePath)
        {
            StringBuilder csvContent = new StringBuilder();

            // Add data to CSV
            foreach (var row in data)
            {
                string line = string.Join(",", row);
                csvContent.AppendLine(line);
            }

            // Write to file
            File.WriteAllText(filePath, csvContent.ToString());
        }
    }
    */

}
