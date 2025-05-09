using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskLayoutGroup : MonoBehaviour
{
    [SerializeField] private List<TaskView> _tasksBodies;
    [SerializeField] private GameObject taskBodyPrefab;

    public TaskView CreateNewTaskView()
    {
        TaskView newTaskBody =
            Instantiate(taskBodyPrefab, transform).GetComponent<TaskView>();
        _tasksBodies.Add(newTaskBody);
        return newTaskBody;
    }
}
