using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropdown : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject content, template;
    [SerializeField] private TMP_Text text;
    [SerializeField] private string[] Options;

    public Action<int> OnValueChanged;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < Options.Length; i++)
        {
            var obj = Instantiate(template, content.transform, false);
            obj.GetComponent<DropdownItem>().Init(this, Options[i], i);
            obj.SetActive(true);
        }

        text.text = Options[0];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Show();
    }

    public void SubmitChoice(int value)
    {
        Hide();

        text.text = Options[value];
        OnValueChanged?.Invoke(value);
    }

    private void Show()
    {
        if (content.activeSelf) return;

        content.SetActive(true);
    }
    private void Hide()
    {
        if (!content.activeSelf) return;

        content.SetActive(false);
    }
}
