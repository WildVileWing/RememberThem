using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button playButton, dicePlayBtn;
    [SerializeField] private TMP_Text nameText;

    private void Awake()
    {
        nameText.text = "Hello, " + DataManager.Name;
        playButton.onClick.AddListener(OpenGameScene);
        dicePlayBtn.onClick.AddListener(() => SceneManager.LoadScene("DiceScene"));
    }

    public void OpenGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }


}
