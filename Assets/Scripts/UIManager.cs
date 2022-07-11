using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Text nameText;

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
