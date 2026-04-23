using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "SkillsDatabase", menuName = "Datas/Collectables/SkillsDatabase")]
public class SkillsDatabase : ScriptableObject
{
    public List<SkillData> skillsData = new List<SkillData>();


#if UNITY_EDITOR

    // reset database
    [Button]
    public void ResetData()
    {
        foreach (SkillData skill in skillsData)
        {
            skill.isUnlocked = false;
        }
    }
    #endif
}
