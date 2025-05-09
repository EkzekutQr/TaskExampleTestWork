using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour, IHP
{
    [SerializeField] private int _hPCount = 1;

    public int HPCount { get => _hPCount; }

    public void TakeDamage(int damage)
    {
        _hPCount = _hPCount - damage;

        if (_hPCount <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
