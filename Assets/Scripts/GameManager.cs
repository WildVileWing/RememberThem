using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    // добавить префаб и массив наполнять динамически
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject gridPanel;
    [SerializeField] private GameObject resultPanels;
    [SerializeField] private TMP_Text resultText;

    public Button[] allButtons = new Button[36];
    private Button[] correctButtons;
    private List<int> uniqueNumbers = new List<int>();
    [SerializeField] private Button homeButton;
    private int counter = 0;
    private float startTime = 0;
    private float currentTime = 0;
    private bool timerGoing = false;
    private void Awake()
    {
        //buttonPrefab.transform.parent = gridPanel.transform;
        DataManager.Instance.data.level = 10;
        correctButtons = new Button[DataManager.Instance.data.level];
        homeButton.onClick.AddListener(ReturnToMenu);
        StartCoroutine(ShowAndHideCells());
        addListenerToAllButtons();
    }
    private void Update()
    {
        if (timerGoing)
            currentTime += Time.deltaTime;

    }
    public void ShowCells()
    {
        // цикл и размер массива по значению из DataManager
        for(int i = 0; i < allButtons.Length; i++)
        {
            uniqueNumbers.Add(i);
        }
        for(int i = 0; i < DataManager.Instance.data.level; i++)
        {
            int randomUniqueNumber = uniqueNumbers[Random.Range(0, uniqueNumbers.Count)];
            allButtons[randomUniqueNumber].image.color = Color.white;

            allButtons[randomUniqueNumber].gameObject.
                GetComponentInChildren<TMP_Text>().text = i.ToString();

            allButtons[randomUniqueNumber].gameObject.
                GetComponentInChildren<TMP_Text>().enabled = true;

            correctButtons[i] = allButtons[randomUniqueNumber];
            correctButtons[i].image.enabled = true;
            uniqueNumbers.Remove(randomUniqueNumber);
        }
    }

    public void HideCells()
    {
        for (int i = 0; i < DataManager.Instance.data.level; i++)
        {
            correctButtons[i].image.color = Color.gray;
            correctButtons[i].gameObject.GetComponentInChildren<TMP_Text>().enabled = false;
        }
    }

    public void addListenerToAllButtons()
    {
        for (int i = 0; i < allButtons.Length; i++)
        {
            int copyI = i;
            allButtons[copyI].onClick.AddListener(() => CheckCell(allButtons[copyI])); 
        }

    }

    public void CheckCell(Button button)
    {
        if (button.GetComponentInChildren<TMP_Text>().text == counter.ToString()
            && (counter < DataManager.Instance.data.level - 1))
        {
            //next
            // new Color исправить на цвет из инспектора
            button.image.color = new Color(37, 150, 190);
            button.GetComponentInChildren<TMP_Text>().enabled = true;
            button.enabled = false;
            counter++;
        }
        else if (counter == DataManager.Instance.data.level - 1)
        {
            //win
            counter = 0;
            gridPanel.SetActive(false);
            resultPanels.SetActive(true);
            timerGoing = false;
            resultText.text = "You win! \n Your time " + currentTime + " seconds.";
            return;
        }
        else
        {
            //lose
            button.image.color = new Color(190, 77, 37);
            counter = 0;
            gridPanel.SetActive(false);
            resultPanels.SetActive(true);
            timerGoing = false;
            resultText.text = "You lose!";
            return;
        }
    }

    IEnumerator ShowAndHideCells()
    {
        ShowCells();
        // объявление new WaitForSeconds закешировать (тоже самое, что и с цветом)
        yield return new WaitForSeconds(3);
        HideCells();
        for(int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].enabled = true;
        }
        currentTime = 0;
        timerGoing = true;

    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
