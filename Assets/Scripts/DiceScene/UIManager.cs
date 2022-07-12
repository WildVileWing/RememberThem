using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button startBtn, endBtn, homeBtn, chartBtn, chartCloseBtn;
    [SerializeField] private GameObject menuPanel, gamePanel, chartPanel;
    [SerializeField] private TMP_Text endTxt, estNumTxt, currentNumTxt;

    private void Start()
    {
        startBtn.onClick.AddListener(StartGame);
        endBtn.onClick.AddListener(EndGame);
        homeBtn.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));

        chartBtn.onClick.AddListener(ChangeChartState);
        chartCloseBtn.onClick.AddListener(ChangeChartState);

        EventManager.inst.GameCompleted += GameCompleted;
        EventManager.inst.EstimatedNumUpdate += EstimatedNumUpdate;
        EventManager.inst.CurrentNumUpdate += CurrentNumUpdate;

        EstimatedNumUpdate(1);

        menuPanel.SetActive(true);
    }

    private void EstimatedNumUpdate(int value)
    {
        estNumTxt.text = $"Your number: {value}";
    }
    private void CurrentNumUpdate(int value)
    {
        currentNumTxt.text = $"Current number: {value}";
    }
    private void GameCompleted(bool value, float chance)
    {
        if (value) endTxt.text = $"Awesome, You just win with {chance:F4}% chance";
        else endTxt.text = $"Loser, but don't give up))";

        endBtn.gameObject.SetActive(true);
    }
    private void StartGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        EventManager.inst.StartGameInv();
    }
    private void EndGame()
    {
        gamePanel.SetActive(false);
        menuPanel.SetActive(true);
        endBtn.gameObject.SetActive(false);
        endTxt.text = null;
        currentNumTxt.text = null;

        EventManager.inst.EndGame();
    }

    private void ChangeChartState()
    {
        chartPanel.SetActive(!chartPanel.activeSelf);

        if (chartPanel.activeSelf) menuPanel.SetActive(false);
        else menuPanel.SetActive(true);
    }
}
