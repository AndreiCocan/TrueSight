using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVExporter
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
        File.AppendAllText(filePath, csvContent.ToString());
    }
}