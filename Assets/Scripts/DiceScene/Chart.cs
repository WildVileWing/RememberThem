using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MonoBehaviour
{
    [SerializeField] private GameObject columnPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Button reloadBtn;

    private int currentChartNum = 6;

    private void Awake()
    {
        dropdown.OnValueChanged += OnValueChanged;
        reloadBtn.onClick.AddListener(ReloadChart);

        LoadChart(currentChartNum);
    }

    private void LoadChart(int num)
    {
        ClearParent();

        int[] stats;
        Statistics.GetStatistic($"{num}Edges", out stats);

        if (stats == null) stats = new int[num];

        int sum = Enumerable.Sum(stats);

        for (int i = 0; i < num; i++)
        {
            float percent = sum == 0 ? 0 : (float)stats[i] / sum * 100;

            var obj = Instantiate(columnPrefab, parent, false);
            obj.GetComponent<ChartColumn>().Init(i + 1, stats[i], percent);
        }

        currentChartNum = num;
    }
    private void ReloadChart()
    {
        LoadChart(currentChartNum);
    }
    private void ClearParent()
    {
        for (int i = parent.childCount; i > 0; i--)
        {
            Destroy(parent.GetChild(i - 1).gameObject);
        }
    }
    private void OnValueChanged(int value)
    {
        if (value == 0)
            LoadChart(4);
        else if (value == 1)
            LoadChart(6);
        else if (value == 2)
            LoadChart(8);
        else if (value == 3)
            LoadChart(10);
        else if (value == 4)
            LoadChart(12);
        else if (value == 5)
            LoadChart(20);
        else if (value == 6)
            LoadChart(100);
    }

    private void OnDestroy()
    {
        dropdown.OnValueChanged -= OnValueChanged;
    }
}
