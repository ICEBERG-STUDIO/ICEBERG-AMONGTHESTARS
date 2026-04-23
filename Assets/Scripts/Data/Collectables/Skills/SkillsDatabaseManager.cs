
using UnityEngine;

public class SkillsDatabaseManager : MonoBehaviour
{
    public SkillsDatabase skillsDatabase;

    public SkillData GetSkill(string id) => skillsDatabase.skillsData.Find(x => x.genericData.ID == id);
}
