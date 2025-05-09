using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour
{
    [SerializeField] private List<TaskBase> tasks;
    [SerializeField] private Transform tasksParentTransform;
    [SerializeField] private TaskBase currentTask;

    [SerializeField] private bool isLastTaskCompleted;


    //[SerializeField] PauseMenuController pauseMenuController;

    private void Awake()
    {
        SetAllTasksFromChilds();
        SetFirstTask();
    }
    void SetAllTasksFromChilds()
    {
        foreach (var item in tasksParentTransform)
        {
            //Debug.Log(((Transform)item).name);
            tasks.Add(((Transform)item).GetComponent<TaskBase>());
        }
    }
    void SetFirstTask()
    {
        currentTask = tasks[0];
        currentTask.onTaskCompleted += SetNextTask;
        ShowText();
    }

    private void ShowText()
    {
        currentTask.ShowText(null);
    }

    private void Update()
    {
        CheckCurrentTaskCompleteClause();
    }
    void CheckCurrentTaskCompleteClause()
    {
        if (isLastTaskCompleted)
            return;
        currentTask.CompleteClause();
    }
    void SetNextTask(TaskBase currentTask)
    {
        currentTask.onTaskCompleted -= SetNextTask;
        currentTask.IsComleted = true;

        if (tasks.IndexOf(currentTask) >= tasks.Count - 1)
        {
            Debug.Log("LastTaskIsCompleted");
            isLastTaskCompleted = true;
            OnLastTaskComleted();

            return;
        }

        if (currentTask.NextTask != null)
            currentTask = currentTask.NextTask;
        else
            currentTask = tasks[tasks.IndexOf(currentTask) + 1];

        currentTask.onTaskCompleted += SetNextTask;
        ShowText();
    }
    void OnLastTaskComleted()
    {
        //pauseMenuController.OpenEndGameWindow();
    }
}


