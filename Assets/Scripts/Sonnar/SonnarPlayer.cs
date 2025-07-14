using UnityEngine;

public class SonnarPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _sonnarPrefab;
    [SerializeField] private Transform _posSonnar;

    public void ActivateSonnar()
    {
        Instantiate(_sonnarPrefab, _posSonnar.position, Quaternion.identity);
    }
}
