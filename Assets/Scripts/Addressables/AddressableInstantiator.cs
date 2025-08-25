using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableInstantiator : MonoBehaviour, IOnTrigger
{
    [SerializeField] AssetReferenceGameObject _environment;
    private GameObject _instanceReference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void OnAddressableLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _instanceReference = handle.Result;
        }
        else
            Debug.LogError("Loading Asset Failded)");
    }

    private void OnValidate()
    {
        transform.position = Vector3.zero;
    }

    [Button]
    private void LoadLevel()
    {
        if (_environment != null) _environment.InstantiateAsync().Completed += OnAddressableLoaded;
    }

    [Button]
    private void UnloadLevel()
    {
        if (_instanceReference != null && _environment != null)
        {
            _environment.ReleaseInstance(_instanceReference);
        }
    }

    #region OnTrigger Interface

    //load on enter
    public void OnEnter()
    {
        LoadLevel();
    }


    //unload on exit
    public void OnExit()
    {
        UnloadLevel();
    }

    #endregion

}
