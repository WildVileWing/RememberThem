using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    // будет замечательно, если переделать в статичный класс/SO для простоты использования
    public static DataManager Instance { get; private set; }

    public Data data;
    private string DataPath;

    public void DataSave()
    {
        if (data != null)
        {          
            File.WriteAllText(DataPath, JsonUtility.ToJson(data));
            return;
        }
        data = new Data();

    }

    public void DataLoad()
    {
      
        if (File.Exists(DataPath))
        {
            data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(DataPath));
            return;
        }
        data = new Data();

    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Statistics.SaveData();
            DataSave();
        }
    }

    private void OnApplicationQuit()
    {
        Statistics.SaveData();
        DataSave();
    }
    
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        DataPath = Application.persistentDataPath + "/Data.json";
        if (!File.Exists(DataPath))
        {
            DataSave();
            return;
        }
        DataLoad();
        Statistics.LoadData();
    }

    public class Data
    {
        public string name = null;
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
