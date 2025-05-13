using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBase : MonoBehaviour
{
    [SerializeField] protected TaskBase nextTask;
    [SerializeField] protected string taskText;
    [SerializeField] private bool isMultypleTask;

    [SerializeField] protected bool _isComleted;
    [SerializeField] protected Transform progressBar;

    public Action<TaskBase> onTaskCompleted;

    public TaskBase NextTask { get => nextTask; set => nextTask = value; }
    public string TaskText { get => taskText; set => taskText = value; }
    public bool IsComleted { get => _isComleted; set => _isComleted = value; }
    public bool IsMultypleTask { get => isMultypleTask; set => isMultypleTask = value; }

    public abstract void CompleteClause();

    public abstract float GetProgress();

    public virtual void ShowText(TMPro.TextMeshProUGUI taskText)
    {
        taskText.text = this.taskText;
    }

    //public void SimpleMethod()
    //{
    //    onTaskCompleted.
    //}
}
