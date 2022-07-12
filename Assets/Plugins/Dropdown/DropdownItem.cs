using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropdownItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text text;
    private Dropdown dropdown;
    private int value;

    public void Init(Dropdown caller, string option, int value)
    {
        dropdown = caller;
        this.value = value;

        text.text = option;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dropdown.SubmitChoice(value);
    }
}
