using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class StartupManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button confirmButton;

    private void Start()    
    {
        //Может быть if(DataManager.Instance.data != null) ? 
        if(!string.IsNullOrWhiteSpace(DataManager.Instance.data.name))
        {
            SceneManager.LoadScene("MenuScene");
        }
        inputField.onValueChanged.AddListener(SetName);
        confirmButton.onClick.AddListener(OpenMenuScene);
    }

    public void SetName(string name)
    {
        if(name.Length >= 3 ||
            name.Length <= 12)
        {
            DataManager.Instance.data.name = name;
        }
        
    }

    public void OpenMenuScene()
    {
        if ((DataManager.Instance.data.name.Length < 3) ||
            (DataManager.Instance.data.name.Length > 12))
        {
            return;
        }
        SceneManager.LoadScene("MenuScene");
    }
}
