using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class Extensions
{
    public static IObservable<AsyncOperationHandle<T>> WaitForCompleted<T>(this AsyncOperationHandle<T> handle)
        => Observable.Create<AsyncOperationHandle<T>>(observer => {
            handle.Completed += handle => {
                if (handle.Result == null) {
                    observer.OnError(new InvalidOperationException());
                } else if (handle.Status == AsyncOperationStatus.Succeeded) {
                    observer.OnNext(handle);
                    observer.OnCompleted();
                } else {
                    observer.OnError(handle.OperationException);
                }
            };

            return Disposable.Empty;
        });
}

public class Node : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Name;

    public static IObservable<AsyncOperationHandle<GameObject>> InstantiateAsync(string address, Transform parent, bool instantiateInWorldSpace, bool trackHandle)
        => Addressables.InstantiateAsync(address, parent, instantiateInWorldSpace, trackHandle)
            .WaitForCompleted();

    public static IObservable<Unit> Create(string name, GameObject parent)
	    => InstantiateAsync("Node", parent.transform, false, true)
            .Do(_ => _.Result.GetComponent<Node>().Name.text = name)
            .AsUnitObservable();
}
