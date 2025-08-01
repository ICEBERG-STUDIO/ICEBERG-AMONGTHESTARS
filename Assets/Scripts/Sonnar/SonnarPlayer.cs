using UnityEngine;

public class SonnarPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _sonnarPrefab;
    [SerializeField] private Transform _posSonnar;

    //récup et fait spawn le prefab du sonnar
    public void ActivateSonnar()
    {
        Instantiate(_sonnarPrefab, _posSonnar.position, Quaternion.identity);
    }
}
