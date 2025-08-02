using UnityEngine;

public class StarsDatabaseManager : MonoBehaviour
{
    [SerializeField] private StarsDatabase _starsDatabase;
    public StarsData GetStar(int id) => _starsDatabase.starsData[id];


    // Bool needed to finish game
    public bool AreAllStarsCollected()
    {
        foreach (StarsData star in _starsDatabase.starsData)
        {
            if (star.collectablesData.inInventory == false) return false;
        }

        return true;
    }

    // reset inventory
    public void RemoveAllStarsFromInventory()
    {
        foreach (StarsData star in _starsDatabase.starsData)
        {
            star.collectablesData.inInventory = false;
        }
    }

}
