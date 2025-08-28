using UnityEngine;

public class WwiseEventManager : MonoBehaviour
{
    [Header("Wwise Events :")]
    [SerializeField] private AK.Wwise.Event _walkEvent;
    private bool IsFootstepPlaying = false;
    
    public void PlayFootstepSound()
    {
        _walkEvent.Post(gameObject);
    }
}