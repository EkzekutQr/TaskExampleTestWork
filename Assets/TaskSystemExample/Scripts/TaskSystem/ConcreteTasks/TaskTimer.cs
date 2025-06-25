using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTimer : TaskBase
{
    [SerializeField] private int seconds = 120;

    private float _startTime;

    public override void MissionStart()
    {
        base.MissionStart();

        SetStartTime();

        RaiseOnStarted(this);
    }

    public override void MissionUpdate()
    {
        CheckCompleteClause();
    }

    private void SetStartTime()
    {
            _startTime = Time.time;
    }

    private void CheckCompleteClause()
    {
        if (Time.time - _startTime >= seconds)
        {
            //onTaskCompleted.Invoke(this);
            _isComleted = true;
            RaiseOnMissionPointReached(this);
            RaiseOnTaskFinished(this);
        }
    }

    public override float GetProgress()
    {
        float progress = (Time.time - _startTime) / seconds;
        if (progress > 1f)
            progress = 1f;

        return progress;
    }
}
