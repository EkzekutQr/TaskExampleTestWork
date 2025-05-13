using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Progress;

public class TaskController3 : MonoBehaviour
{
    [SerializeField] private List<TaskBase> _tasks = new List<TaskBase>();
    [SerializeField] private Transform _tasksParentTransform;

    [SerializeField] private TaskLayoutGroup _taskLayoutGroup;
    [SerializeField] private List<TaskView> _taskViews = new List<TaskView>();

    [SerializeField] private List<TaskBase> _currentTask = new List<TaskBase>();

    [SerializeField] private GameObject _TaskViewPrefab;

    private void Awake()
    {
        SetAllTasksFromChilds();
        SetAllTaskViewsFromChilds();
        SetFirstTask();
    }

    private void SetFirstTask()
    {
        bool isMultipleTask = true;

        while (isMultipleTask)
        {
            TaskBase currentTask = _tasks[0];
            _tasks.RemoveAt(0);
            _currentTask.Add(currentTask);
            isMultipleTask = currentTask.IsMultypleTask;

            CreateNewTaskView(currentTask);
            currentTask.onTaskCompleted += RemoveTaskFromCurrentTasks;
        }
    }

    private void Update()
    {
        CheckCurrentTaskCompleteClause();
        UpdateTaskProgress();
    }

    private void UpdateTaskProgress()
    {
        foreach (var view in _taskViews)
        {
            view.UpdateProgressBar(view.TaskBase.GetProgress());
        }
    }

    private void CheckCurrentTaskCompleteClause()
    {
        //if (_isLastTaskCompleted)
        //    return;

        for (int i = 0; i < _currentTask.Count; i++)
        {
            TaskBase item = _currentTask[i];
            item.CompleteClause();
        }
    }

    private void RemoveTaskFromCurrentTasks(TaskBase taskBase)
    {
        _currentTask.Remove(taskBase);

        if(_currentTask.Count == 0)
        {
            for (int i = 0; i < _taskViews.Count; i++)
            {
                TaskView item = _taskViews[i];
                item.gameObject.SetActive(false);
            }
            _taskViews.Clear();
        }

        if (_currentTask.Count == 0)
            SetNextCurrentTask();
    }

    private void SetNextCurrentTask()
    {
        if (_tasks.Count == 0)
            return;

        bool isMultipleTask = true;

        while (isMultipleTask)
        {
            TaskBase currentTask = _tasks[0];
            _tasks.RemoveAt(0);
            _currentTask.Add(currentTask);
            isMultipleTask = currentTask.IsMultypleTask;

            CreateNewTaskView(currentTask);
            currentTask.onTaskCompleted += RemoveTaskFromCurrentTasks;
        }
    }

    private void CreateNewTaskView(TaskBase taskBase)
    {
        TaskView newTaskView = Instantiate(_TaskViewPrefab,
            _taskLayoutGroup.transform).GetComponent<TaskView>();

        newTaskView.Init(taskBase.TaskText, taskBase.GetProgress(), taskBase);
        _taskViews.Add(newTaskView);
    }

    void SetAllTasksFromChilds()
    {
        foreach (var item in _tasksParentTransform)
        {
            _tasks.Add(((Transform)item).GetComponent<TaskBase>());
        }
    }
    void SetAllTaskViewsFromChilds()
    {
        foreach (var item in _taskLayoutGroup.transform)
        {
            _taskViews.Add(((Transform)item).GetComponent<TaskView>());
        }
    }
}
