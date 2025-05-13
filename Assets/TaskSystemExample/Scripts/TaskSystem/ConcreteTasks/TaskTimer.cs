using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTimer : TaskBase
{
    [SerializeField] private int seconds = 120;

    private float _startTime;
    private bool _isStartTimeSetted;

    public override void CompleteClause()
    {
        SetStartTime();
        CheckCompleteClause();
        if(_isComleted)
            onTaskCompleted.Invoke(this);
    }

    private void SetStartTime()
    {
        if (!_isStartTimeSetted)
        {
            _startTime = Time.time;
            _isStartTimeSetted = true;
        }
    }

    private void CheckCompleteClause()
    {
        if (Time.time - _startTime >= seconds)
            //onTaskCompleted.Invoke(this);
            _isComleted = true;
    }

    public override float GetProgress()
    {
        float progress = (Time.time - _startTime) / seconds;
        if (progress > 1f)
            progress = 1f;

        return progress;
    }
}
