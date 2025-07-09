using UnityEngine;

public class SonnarPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _sonnarPrefab;
    [SerializeField] private Transform _posSonnar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("hjhbj");
            Instantiate(_sonnarPrefab, _posSonnar.position, Quaternion.identity);
        }
    }
}
