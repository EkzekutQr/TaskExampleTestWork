using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Progress;

public class TaskController : MonoBehaviour
{
    [SerializeField] private List<IMission> _tasks = new List<IMission>();
    [SerializeField] private Transform _tasksParentTransform;

    [SerializeField] private TaskLayoutGroup _taskLayoutGroup;
    [SerializeField] private List<TaskView> _taskViews = new List<TaskView>();

    [SerializeField] private List<IMission> _currentTasks = new List<IMission>();

    [SerializeField] private GameObject _TaskViewPrefab;

    private void Awake()
    {
        SetAllTasksFromChilds();
        SetAllTaskViewsFromChilds();
    }

    private void Start()
    {
        SetFirstTask();
    }

    private void SetFirstTask()
    {
        bool isMultipleTask = true;

        while (isMultipleTask)
        {
            IMission currentTask = _tasks[0];
            _tasks.RemoveAt(0);
            _currentTasks.Add(currentTask);
            isMultipleTask = currentTask.IsMultypleTask;

            CreateNewTaskView(currentTask);
            currentTask.OnFinished += RemoveTaskFromCurrentTasks;

            currentTask.MissionStart();
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

        for (int i = 0; i < _currentTasks.Count; i++)
        {
            IMission item = _currentTasks[i];
            item.MissionUpdate();
        }
    }

    private void RemoveTaskFromCurrentTasks(IMission taskBase)
    {
        taskBase.OnFinished -= RemoveTaskFromCurrentTasks;
        //Debug.Log($"{taskBase.ToString()} is completed");
        if (_currentTasks.Contains(taskBase))
        {
            _currentTasks.Remove(taskBase);
        }

        if(_currentTasks.Count == 0)
        {
            for (int i = 0; i < _taskViews.Count; i++)
            {
                TaskView item = _taskViews[i];
                item.gameObject.SetActive(false);
            }
            _taskViews.Clear();
        }

        if (_currentTasks.Count == 0)
            SetNextCurrentTask();
    }

    private void SetNextCurrentTask()
    {
        if (_tasks.Count == 0)
            return;

        bool isMultipleTask = true;

        while (isMultipleTask)
        {
            IMission currentTask = _tasks[0];
            _tasks.RemoveAt(0);
            _currentTasks.Add(currentTask);
            isMultipleTask = currentTask.IsMultypleTask;

            CreateNewTaskView(currentTask);
            currentTask.OnFinished += RemoveTaskFromCurrentTasks;

            currentTask.MissionStart();
        }
    }

    private void CreateNewTaskView(IMission taskBase)
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
            _tasks.Add(((Transform)item).GetComponent<IMission>());
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

public class MissionThread
{
    [SerializeField] private List<IMission> _tasks = new List<IMission>();
    [SerializeField] private List<IMission> _currentTasks = new List<IMission>();
    [SerializeField] private List<TaskView> _taskViews = new List<TaskView>();
}
