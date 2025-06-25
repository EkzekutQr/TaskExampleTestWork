using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Timer : MonoBehaviour
{
 private CancellationTokenSource _cts;


    public async UniTask StartAsync(int ms, Action onComplete = null)
    {
        _cts = new CancellationTokenSource();

        try
        {
            await UniTask.Delay(ms, cancellationToken: _cts.Token);

            onComplete?.Invoke();
        }
        catch (OperationCanceledException)
        {
        }
    }


    public void Cancel()
    {
        _cts?.Cancel();
    }
}
