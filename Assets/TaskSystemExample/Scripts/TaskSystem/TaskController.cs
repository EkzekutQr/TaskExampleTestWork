using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private List<MissionThread> missionThreads = new List<MissionThread>();

    [SerializeField] private TaskLayoutGroup _taskLayoutGroup;
    [SerializeField] private GameObject _TaskViewPrefab;

    private Timer _timer = new Timer();

    private void Awake()
    {
        foreach (var thread in missionThreads)
        {
            SetAllTasksFromChilds(thread);
            SetAllTaskViewsFromChilds(thread);
        }
    }

    private void Start()
    {
        SetFirstTask();
    }

    private void SetFirstTask()
    {
        foreach (var thread in missionThreads)
        {
            SetTask(thread);
        }
    }

    private async void SetTask(MissionThread thread)
    {
        bool isMultipleTask = true;

        while (isMultipleTask)
        {
            IMission currentTask = thread.Tasks[0];

            await Wait(currentTask);

            currentTask.ParentThread = thread;
            thread.Tasks.RemoveAt(0);
            thread.CurrentTasks.Add(currentTask);
            isMultipleTask = currentTask.IsMultypleTask;

            CreateNewTaskView(currentTask);
            currentTask.OnFinished += RemoveTaskFromCurrentTasks;

            currentTask.MissionStart();
        }
    }

    private async UniTask Wait(IMission task)
    {
        UniTask task1 = _timer.StartAsync(task.StartDelayTime);
        await task1;
    }

    private void Update()
    {
        CheckCurrentTaskCompleteClause();
        UpdateTaskProgress();
    }

    private void UpdateTaskProgress()
    {
        foreach (var thread in missionThreads)
        {
            foreach (var view in thread.TaskViews)
            {
                view.UpdateProgressBar(view.TaskBase.GetProgress());
            }
        }
    }

    private void CheckCurrentTaskCompleteClause()
    {
        //if (_isLastTaskCompleted)
        //    return;

        foreach (var thread in missionThreads)
        {
            for (int i = 0; i < thread.CurrentTasks.Count; i++)
            {
                IMission mission = thread.CurrentTasks[i];
                mission.MissionUpdate();
            }
        }
    }

    private void RemoveTaskFromCurrentTasks(IMission taskBase)
    {
        taskBase.OnFinished -= RemoveTaskFromCurrentTasks;

        MissionThread thread = taskBase.ParentThread;
        //Debug.Log($"{taskBase.ToString()} is completed");
        if (thread.CurrentTasks.Contains(taskBase))
        {
            thread.CurrentTasks.Remove(taskBase);
        }

        if (thread.CurrentTasks.Count == 0)
        {
            for (int i = 0; i < thread.TaskViews.Count; i++)
            {
                TaskView item = thread.TaskViews[i];
                item.gameObject.SetActive(false);
            }
            thread.TaskViews.Clear();
        }

        if (thread.CurrentTasks.Count == 0)
            SetNextCurrentTask(thread);
    }

    private void SetNextCurrentTask(MissionThread thread)
    {
        if (thread.Tasks.Count == 0)
            return;

        SetTask(thread);
    }

    private void CreateNewTaskView(IMission taskBase)
    {
        TaskView newTaskView = Instantiate(_TaskViewPrefab,
            _taskLayoutGroup.transform).GetComponent<TaskView>();

        newTaskView.Init(taskBase.TaskText, taskBase.GetProgress(), taskBase);
        taskBase.ParentThread.TaskViews.Add(newTaskView);
    }

    void SetAllTasksFromChilds(MissionThread thread)
    {
        foreach (var item in thread.TasksParentTransform)
        {
            thread.Tasks.Add(((Transform)item).GetComponent<IMission>());
        }
    }
    void SetAllTaskViewsFromChilds(MissionThread thread)
    {
        foreach (var item in _taskLayoutGroup.transform)
        {
            thread.Tasks.Add(((Transform)item).GetComponent<IMission>());
        }
    }
}

[Serializable]
public class MissionThread
{
    [SerializeField] private List<IMission> _tasks = new List<IMission>();
    [SerializeField] private List<IMission> _currentTasks = new List<IMission>();
    [SerializeField] private List<TaskView> _taskViews = new List<TaskView>();
    [SerializeField] private Transform _tasksParentTransform;

    public List<IMission> Tasks { get => _tasks; }
    public List<IMission> CurrentTasks { get => _currentTasks; }
    public List<TaskView> TaskViews { get => _taskViews; }
    public Transform TasksParentTransform { get => _tasksParentTransform; set => _tasksParentTransform = value; }
}
