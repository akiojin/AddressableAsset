using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    [SerializeField]
    Button Button;
    [SerializeField]
    GameObject Parent;

    int _count = 0;

    void Start()
    {
        ResourceManager.ExceptionHandler = (handle, ex) => {
            Debug.Log($"Resrouce Manager Exception: Name={handle.DebugName}, Status={handle.Status}");
        };

        Addressables.InitializeAsync()
            .WaitForCompleted()
            .Subscribe();

        Button.OnClickAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ => {
                Node.Create($"Node - {_count++}", Parent)
                    .Subscribe();
            });
    }
}
