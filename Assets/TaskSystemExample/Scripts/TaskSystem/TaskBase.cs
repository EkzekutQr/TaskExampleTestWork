using System;
using System.Net.Security;
using UnityEngine;

public abstract class TaskBase : MonoBehaviour, IMission
{
    [SerializeField] protected string taskText;
    [SerializeField] private bool isMultypleTask;

    [SerializeField] protected bool _isComleted;
    [SerializeField] protected Transform progressBar;
    [SerializeField] private int startDelayTime = 0;

    public string TaskText { get => taskText; }
    public bool IsComleted { get => _isComleted; set => _isComleted = value; }
    public bool IsMultypleTask { get => isMultypleTask; }
    public MissionThread ParentThread { get; set; }
    public int StartDelayTime { get => startDelayTime; }

    public event Action<TaskBase> OnStarted;
    public event Action<TaskBase> OnMissionPointReached;
    public event Action<TaskBase> OnFinished;

    public abstract void MissionUpdate();

    public abstract float GetProgress();


    public virtual void ShowText(TMPro.TextMeshProUGUI taskText)
    {
        taskText.text = this.taskText;
    }

    public virtual void MissionStart()
    {
        OnStarted += _ => { Debug.Log($"{this.ToString()} is Started"); };
        OnMissionPointReached += _ => Debug.Log($"{this.ToString()} is Mission Point Reached");
        OnFinished += _ => Debug.Log($"{this.ToString()} is Finished");
    }

    protected virtual void RaiseOnStarted(TaskBase taskBase)
    {
        var ev = OnStarted;
        if(ev != null)
        {
            ev(taskBase);
        }
    }
    protected virtual void RaiseOnMissionPointReached(TaskBase taskBase)
    {
        var ev = OnMissionPointReached;
        if (ev != null)
        {
            ev(taskBase);
        }
    }
    protected virtual void RaiseOnTaskFinished(TaskBase taskBase)
    {
        var ev = OnFinished;
        if (ev != null)
        {
            ev(taskBase);
        }
    }
}

public interface IMission
{
    MissionThread ParentThread { get; set; }
    bool IsMultypleTask { get; }
    string TaskText { get; }
    int StartDelayTime { get; }

    event Action<TaskBase> OnStarted;
    event Action<TaskBase> OnMissionPointReached;
    event Action<TaskBase> OnFinished;

    void MissionUpdate();
    float GetProgress();
    void MissionStart();
}
