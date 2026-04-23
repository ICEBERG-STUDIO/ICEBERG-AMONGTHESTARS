using UnityEngine;

public class SkillController : MonoBehaviour, IInteractable
{
    #region Variables

    [Header("MANAGER : ")]
    [SerializeField] private SkillsManager _skillsMgr;

    [Header("INFOS : ")]
    [SerializeField] string _id;

    #endregion

    #region Interfaces

    //Interact
    public void Interact()
    {
        //_skillsMgr.Add(_id);
        Debug.Log($" interact with the skill {_id}");
    }

    #endregion
}