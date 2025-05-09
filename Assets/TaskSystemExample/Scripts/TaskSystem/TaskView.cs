using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image progressBar;
    [SerializeField] private TaskBase taskBase;

    public TextMeshProUGUI Text { get => text; set => text = value; }
    public Image ProgressBar { get => progressBar; set => progressBar = value; }
    public TaskBase TaskBase { get => taskBase; set => taskBase = value; }

    public void Init(string text, float progress, TaskBase taskBase)
    {
        UpdateText(text);
        UpdateProgressBar(progress);
        this.taskBase = taskBase;
    }

    public void UpdateProgressBar(float value)
    {
        if (value > 1) value = 1;
        if (value < 0) value = 0;

        progressBar.fillAmount = value;
    }

    public void UpdateText(string text)
    {
        this.text.text = text;
    }
}
