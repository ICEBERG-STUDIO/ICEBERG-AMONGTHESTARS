using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public InputActionAsset NewControls;

    public LocalizationManager LocManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Pass NewControls to the plain C# manager
        LocManager = new LocalizationManager(NewControls);
    }
    public static string GetJsonTextValue(string key, bool isWithInput)
    {
       string text = LocalizationManager.GetContentByKey(key, isWithInput);
       return text;
    }
}