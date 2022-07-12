using System.IO;
using UnityEngine;

// будет замечательно, если переделать в статичный класс/SO для простоты использования
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    // переделать в приватные поля
    public static string Name = "";
    public static int Level;
    public float DisappearingTime;

    private string DataPath;

    public void DataSave()
    {
        Data data = new Data(Name, Level, DisappearingTime);
        File.WriteAllText(DataPath, JsonUtility.ToJson(data));
    }

    public void DataLoad()
    {
        // поменяй JsonUtility на JsonConvert. Он работает лучше - возможности шире
        Data data = JsonUtility.FromJson<Data>(File.ReadAllText(DataPath));
        Name = data.name;
        Level = data.level;
        DisappearingTime = data.disappearingTime;
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

        DataPath = Path.Combine(Application.persistentDataPath + "/Data.json");
        if (!File.Exists(DataPath))
        {
            DataSave();
            return;
        }
        // не надо сохранять данные, когда их нет - сохранит пустые,
        // лучше сделать отдельный метод для такого случай
        DataLoad();

        Statistics.LoadData();
    }
}
