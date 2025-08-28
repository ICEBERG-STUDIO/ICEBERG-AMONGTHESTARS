using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Checkpoint : MonoBehaviour, IInteractable
{
    public CheckpointsData data;
    [SerializeField] private GameObject checkpointUI;
    [SerializeField] private AK.Wwise.Event validateCheckpointEvent;
    
#if UNITY_EDITOR
    string folderPath = "Assets/Checkpoints";
    
    /// <summary>
    /// Execute on every Unity Load, changing field value...
    /// </summary>
    private void OnValidate()
    {
        if (data == null)
        {
            // Essayer de retrouver un asset existant
            string existingPath = $"{folderPath}/{gameObject.name}.asset";
            data = AssetDatabase.LoadAssetAtPath<CheckpointsData>(existingPath);

            if (data == null)
            {
                CreateData();
            }
        }

        SyncPosition();
    }

    private void Update()
    {
        if (!Application.isPlaying && data != null)
        {
            SyncPosition();
        }
    }

    private void SyncPosition()
    {
        if (data != null)
        {
            data.checkpointPosition = transform.position;
            EditorUtility.SetDirty(data);
        }
    }

    /// <summary>
    /// The bellow function make a scriptable object for the checkpoint prefab
    /// places in the scene so the designer don't have to make it manually
    /// </summary>
    private void CreateData()
    {
        data = ScriptableObject.CreateInstance<CheckpointsData>();
        data.checkpointName = gameObject.name;
        data.checkpointPosition = transform.position;
        
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Checkpoints");
        }

        string assetPath = $"{folderPath}/{gameObject.name}.asset";
        assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
        AssetDatabase.CreateAsset(data, assetPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Created CheckpointData for {gameObject.name} at {assetPath}");
    }
#endif
    public void Interact()
    {
        checkpointUI.SetActive(true);
        Cursor.visible = true;
        
        // Save Data from the current checkpoint informations
        PlayerData playerData = new PlayerData();
        playerData.data.LastPlayerPosition.LastCheckpointPositionX = transform.position.x;
        playerData.data.LastPlayerPosition.LastCheckpointPositionY = transform.position.y;
        playerData.data.LastPlayerPosition.LastCheckpointPositionZ = transform.position.z;
        playerData.data.LastPlanetName = SceneManager.GetActiveScene().name;
        SaveSystem.SavePlayer(playerData);
        
        // Update the Scriptable Object if it's the first time the player interact with it
        if(!data.IsCheckpointFound)
            data.IsCheckpointFound = true;
        
        validateCheckpointEvent.Post(gameObject);
    }
}