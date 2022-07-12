﻿using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public Data data;
    private string DataPath;

    public void DataSave()
    {
        File.WriteAllText(DataPath, JsonUtility.ToJson(data));
    }

    public void DataLoad()
    {
        if (File.Exists(DataPath))
        {
            data = JsonUtility.FromJson<Data>(File.ReadAllText(DataPath));
            return;
        }
        data = new Data();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            DataSave();
    }

    private void OnApplicationQuit()
    {
        DataSave();
    }
    
    private void Awake()
    {
        Instance = this;
        DataPath = Application.persistentDataPath + "/Data.json";

        if (!File.Exists(DataPath))
        {
            DataSave();
            return;
        }
        DontDestroyOnLoad(gameObject);
        DataLoad();

    }


    public class Data
    {
        public string name = "";
        public int level = 0;
        public int money = 0;
        public Data() { }
        public Data(string _name, int _level, int _money)
        {
            name = _name;
            level = _level;
            money = _money;
        }
    }
}
