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


    #region OnTrigger Interface

    //load on enter
    public void OnEnter()
    {
        _environment.InstantiateAsync().Completed += OnAddressableLoaded;
    }


    //unload on exit
    public void OnExit()
    {
        _environment.ReleaseInstance(_instanceReference);
    }

    #endregion

}
