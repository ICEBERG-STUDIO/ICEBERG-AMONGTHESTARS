using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckpointUIManager : MonoBehaviour
{
    [Header("Menus :")]
    [SerializeField] private GameObject checkpointMenu;
    [SerializeField] private GameObject fastTravelMenu;
    [Space]
    [Header("Attributes :")]
    [SerializeField] private GameObject FastTravelScrollBarContent;
    [SerializeField] private GameObject FastTravelButtonPrefab;

    /// <summary>
    /// Close UI and manage some things just in case
    /// </summary>
    public void CloseCheckpoint()
    {
        this.gameObject.SetActive(false);
        fastTravelMenu.SetActive(false);
        checkpointMenu.SetActive(true);
        foreach (Transform child in FastTravelScrollBarContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void GetFoundCheckpoints()
    {
        List<CheckpointsData> checkpointsList = CheckpointManager.checkpoints;
        
        foreach (var checkpoint in checkpointsList)
        {
            if (checkpoint.IsCheckpointFound)
            {
                Button button = Instantiate(FastTravelButtonPrefab, FastTravelScrollBarContent.transform)
                    .GetComponent<Button>();
                button.GetComponentInChildren<TMP_Text>().text = checkpoint.checkpointName;
                button.onClick.AddListener(() => {
                    CheckpointManager.TeleportTo(checkpoint.checkpointName);
                    CloseCheckpoint();
                });
            }
        }
    }
}