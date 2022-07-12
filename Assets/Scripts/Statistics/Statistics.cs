using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Statistics
{
    private static Dictionary<string, string> statistic = new Dictionary<string, string>();
    private static string path = Application.persistentDataPath + 
        "/Statistics/Statistics.stat";

    public static string GetStatistic(string name)
    {
        string value;
        statistic.TryGetValue(name, out value);

        return value;
    }
    public static void SetStatistic(string key, string value)
    {
        if (statistic.ContainsKey(key)) statistic[key] = value;
        else statistic.Add(key, value);
    }

    public static void SaveData()
    {
        string save = JsonConvert.SerializeObject(statistic);

        if (!Directory.Exists(path)) Directory.CreateDirectory(Path.GetDirectoryName(path));

        File.WriteAllText(path, save);
    }
    public static void LoadData()
    {
        if (File.Exists(path))
        {
            statistic = JsonConvert.DeserializeObject<Dictionary<string, string>>
                (File.ReadAllText(path));
        }
    }
}
