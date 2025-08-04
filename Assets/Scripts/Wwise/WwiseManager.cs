using UnityEngine;

public class WwiseManager : MonoBehaviour
{
    [Header("Wwise events :")] [SerializeField]
    private AK.Wwise.Event _sandFootstepsEvent;
    [SerializeField] private AK.Wwise.Event _swimEvent;

    [Space]
    [Header("Emissive objects")]
    [SerializeField] GameObject _player;
    
    public void StartSandFootsteps()
    {
        _sandFootstepsEvent.Post(_player);
    }
}