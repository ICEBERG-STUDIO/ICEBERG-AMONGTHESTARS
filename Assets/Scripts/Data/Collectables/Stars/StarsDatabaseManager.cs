using UnityEngine;

public class StarsDatabaseManager : MonoBehaviour
{
    public StarsDatabase starsDatabase;
    public StarData GetStar(int id) => starsDatabase.starDatas[id];


    // Bool needed to finish game
    public bool AreAllStarsCollected()
    {
        foreach (StarData star in starsDatabase.starDatas)
        {
            if (star.isCollected == false) return false;
        }

        return true;
    }

}
