using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int[] numberArray = new int[40];
    public Button[] allButtons = new Button[40];
    private Button[] correctButtons;
    private List<int> uniqueNumbers = new List<int>();
    int counter = 0;
    private void Awake()
    {
        DataManager.Level = 10;
        correctButtons = new Button[DataManager.Level];
        StartCoroutine(ShowAndHideCells());
        addListenerToAllButtons();

    }

    public void ShowCells()
    {
        for(int i = 0; i < allButtons.Length; i++)
        {
            uniqueNumbers.Add(i);
        }
        for(int i = 0; i < DataManager.Level; i++)
        {
            int randomNumber = uniqueNumbers[Random.Range(0, uniqueNumbers.Count)];
            allButtons[randomNumber].image.color = Color.white;
            allButtons[randomNumber].gameObject.GetComponentInChildren<Text>().text = i.ToString();
            allButtons[randomNumber].gameObject.GetComponentInChildren<Text>().enabled = true;
            correctButtons[i] = allButtons[randomNumber];

            uniqueNumbers.Remove(randomNumber);
        }
    }

    public void HideCells()
    {
        for (int i = 0; i < DataManager.Level; i++)
        {
            correctButtons[i].image.color = Color.gray;
            correctButtons[i].gameObject.GetComponentInChildren<Text>().enabled = false;
        }
    }

    public void addListenerToAllButtons()
    {
        for (int i = 0; i < allButtons.Length; i++)
        {
            int CopyI = i;
            allButtons[CopyI].onClick.AddListener(() => CheckCell(allButtons[CopyI])); 
        }

    }

    public void CheckCell(Button button)
    {
        if (button.GetComponentInChildren<Text>().text == counter.ToString() && (counter < DataManager.Level-1))
        {
            button.image.color = new Color(37, 150, 190);
            button.GetComponentInChildren<Text>().enabled = true;
            button.enabled = false;
            counter++;
        }
        else if (counter == DataManager.Level-1)
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
        yield return new WaitForSeconds(3);
        HideCells();
        for(int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].enabled = true;
        }
    }
}
