using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerData playerData = new PlayerData();
        playerData.data.LastPlayerPosition.LastCheckpointPositionX = transform.position.x;
        playerData.data.LastPlayerPosition.LastCheckpointPositionY = transform.position.y;
        playerData.data.LastPlayerPosition.LastCheckpointPositionZ = transform.position.z;
        playerData.data.LastPlanetName = SceneManager.GetActiveScene().name;
        SaveSystem.SavePlayer(playerData);
    }
}