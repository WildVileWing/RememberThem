using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // добавить префаб и массив наполнять динамически

    public Button[] allButtons = new Button[40];
    private Button[] correctButtons;
    private List<int> uniqueNumbers = new List<int>();
    int counter = 0;
    private void Awake()
    {
        DataManager.Instance.data.level = 10;
        correctButtons = new Button[DataManager.Instance.data.level];
        StartCoroutine(ShowAndHideCells());
        addListenerToAllButtons();

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
            int randomNumber = uniqueNumbers[Random.Range(0, uniqueNumbers.Count)];
            allButtons[randomNumber].image.color = Color.white;

            allButtons[randomNumber].gameObject.
                GetComponentInChildren<TMP_Text>().text = i.ToString();

            allButtons[randomNumber].gameObject.
                GetComponentInChildren<TMP_Text>().enabled = true;

            correctButtons[i] = allButtons[randomNumber];

            uniqueNumbers.Remove(randomNumber);
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
            // new Color исправить на цвет из инспектора

            button.image.color = new Color(37, 150, 190);
            button.GetComponentInChildren<TMP_Text>().enabled = true;
            button.enabled = false;
            counter++;
        }
        else if (counter == DataManager.Instance.data.level - 1)
        {
            counter = 0;
            SceneManager.LoadScene("GameScene");
            return;
        }
        else
        {
            button.image.color = new Color(190, 77, 37);
            counter = 0;
            SceneManager.LoadScene("MenuScene");
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
    }
}
