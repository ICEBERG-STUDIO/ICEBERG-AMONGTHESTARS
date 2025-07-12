using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        LoadPlayerData();
    }

    public static string GetJsonTextValue(string key, bool isWithInput)
    {
        string text = LocalizationManager.GetContentByKey(key, isWithInput);
        return text;
    }

    private void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            SceneManager.LoadScene(data.data.LastPlanetName);
            
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    Vector3 LastPlayerPosition = new Vector3(
                        data.data.LastPlayerPosition.LastCheckpointPositionX,
                        data.data.LastPlayerPosition.LastCheckpointPositionY,
                        data.data.LastPlayerPosition.LastCheckpointPositionZ
                    );
                    
                    player.transform.position = LastPlayerPosition;
                }
                else
                {
                    Debug.LogError("Player object not found in scene!");
                }
            };
        }
    }
}