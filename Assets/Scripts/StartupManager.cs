using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class StartupManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button confirmButton;

    private void Awake()
    {
        if(DataManager.Instance.data.name != null)
        {
            SceneManager.LoadScene("MenuScene");
        }
        inputField.onValueChanged.AddListener(NameChangeCheck);
        confirmButton.onClick.AddListener(OpenMenuScene);
    }

    public void NameChangeCheck(string name)
    {
        DataManager.Instance.data.name = name;
    }

    public void OpenMenuScene()
    {
        if ((DataManager.Instance.data.name.Length < 3) || (DataManager.Instance.data.name.Length > 12))
        {
            return;
        }
        SceneManager.LoadScene("MenuScene");
    }
}
