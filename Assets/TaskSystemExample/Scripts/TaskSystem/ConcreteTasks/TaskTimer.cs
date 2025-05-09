using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTimer : TaskBaseWithProgressBar
{
    [SerializeField] private int seconds = 120;

    private float _startTime;
    private bool _isStartTimeSetted;

    public override void CompleteClause()
    {
        SetStartTime();
        CheckCompleteClause();
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
            onTaskCompleted.Invoke(this);
    }

    private float UpdateProgress()
    {
        float progress = Time.time - _startTime / seconds;
        if(progress > 1f)
            progress = 1f;

        return progress;
    }
}
