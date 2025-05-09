using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController2 : MonoBehaviour
{
    [SerializeField] private List<TaskBase> _tasks;
    [SerializeField] private Transform _tasksParentTransform;
    [SerializeField] private List<TaskBase> _currentTasks;

    [SerializeField] private bool _isLastTaskCompleted;

    [SerializeField] private List<TaskView> _taskViews;
    [SerializeField] private TaskLayoutGroup _taskLayoutGroup;

    //[SerializeField] PauseMenuController pauseMenuController;

    private void Awake()
    {
        SetAllTasksFromChilds();
        SetFirstTask();
    }
    void SetAllTasksFromChilds()
    {
        foreach (var item in _tasksParentTransform)
        {
            //Debug.Log(((Transform)item).name);
            _tasks.Add(((Transform)item).GetComponent<TaskBase>());
        }
    }
    void SetFirstTask()
    {
        var firstTask = _tasks[0];
        _currentTasks.Add(firstTask);
        firstTask.onTaskCompleted += SetNextTask;
        ShowText(firstTask);

        if (firstTask.IsMultypleTask)
        {

        }
    }

    private void ShowText(TaskBase Task)
    {
        TaskView taskView = null;

        foreach (var taskBd in _taskViews)
        {
            if (taskBd.TaskBase == null)
                taskView = taskBd;
        }

        if (taskView == null)
            taskView = _taskLayoutGroup.CreateNewTaskView();

        
        Task.ShowText(taskView.Text);
    }

    private void Update()
    {
        CheckCurrentTaskCompleteClause();
    }
    void CheckCurrentTaskCompleteClause()
    {
        if (_isLastTaskCompleted)
            return;

        foreach (var item in _currentTasks)
            item.CompleteClause();
    }
    void SetNextTask(TaskBase currentTask)
    {
        TaskView currentTaskBody;
        foreach (var taskBd in _taskViews)
            if(taskBd.TaskBase == currentTask)
                currentTaskBody = taskBd;

        currentTask.onTaskCompleted -= SetNextTask;
        currentTask.IsComleted = true;

        if (_tasks.IndexOf(currentTask) >= _tasks.Count - 1)
        {
            Debug.Log("LastTaskIsCompleted");
            _isLastTaskCompleted = true;
            OnLastTaskComleted();

            return;
        }

        if (currentTask.NextTask != null)
            currentTask = currentTask.NextTask;
        else
            currentTask = _tasks[_tasks.IndexOf(currentTask) + 1];

        currentTask.onTaskCompleted += SetNextTask;
        ShowText(currentTask);
    }
    void OnLastTaskComleted()
    {
        //pauseMenuController.OpenEndGameWindow();
    }
}
