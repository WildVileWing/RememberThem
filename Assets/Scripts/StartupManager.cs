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
        inputField.onValueChanged.AddListener(NameChangeCheck);
        confirmButton.onClick.AddListener(OpenMenuScene);
    }

    public void NameChangeCheck(string name)
    {
        DataManager.Name = name;
    }

    public void OpenMenuScene()
    {
        if ((DataManager.Name.Length < 3) || (DataManager.Name.Length > 12))
        {
            return;
        }
        SceneManager.LoadScene("MenuScene");
    }
}
