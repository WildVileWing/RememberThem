using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Statistics
{
    private static Dictionary<string, string> statistic = new Dictionary<string, string>();
    private static string path = Application.persistentDataPath + 
        "/Statistics/Statistics.stat";
    private static string separator = "/";

    public static string GetStatistic(string name)
    {
        string value;
        statistic.TryGetValue(name, out value);

        return value;
    }
    public static void GetStatistic<T>(string name, out T outValue)
    {
        string value;
        statistic.TryGetValue(name, out value);

        if (string.IsNullOrEmpty(value))
        {
            outValue = default;
            return;
        }

        T returnValue = JsonConvert.DeserializeObject<T>(value);
        outValue = returnValue;
    }
    public static void SetStatistic(string key, string value)
    {
        statistic[key] = value;
    }
    public static void SetStatistic(string key, int[] value)
    {
        statistic[key] = JsonConvert.SerializeObject(value);
    }
    // Generic realisation net7.0
    /*public static void AddStatistic<T>(string key, T value)
    {
        if (!statistic.ContainsKey(key))
        {
            statistic[key] = JsonConvert.SerializeObject(value);
            return;
        }

        T oldValue = JsonConvert.DeserializeObject<T>(statistic[key]);

        oldValue = Enumerable.Sum(oldValue, value);
    }*/
    public static void AddStatistic(string key, int[] value)
    {
        if (!statistic.ContainsKey(key))
        {
            statistic[key] = JsonConvert.SerializeObject(value);
            return;
        }

        int[] oldValue = JsonConvert.DeserializeObject<int[]>(statistic[key]);

        for (int i = 0; i < oldValue.Length; i++)
        {
            oldValue[i] += value[i];
        }

        statistic[key] = JsonConvert.SerializeObject(oldValue);
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
