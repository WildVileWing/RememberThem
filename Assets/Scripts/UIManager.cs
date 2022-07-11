using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_Text nameText;

    private void Awake()
    {
        nameText.text = "Hello, " + DataManager.Name;
        playButton.onClick.AddListener(OpenGameScene);
        
    }

    public void OpenGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }


}
