using UnityEngine;

public class ItemController : MonoBehaviour, IInteractable
{
    #region Variables

    [Header("MANAGER : ")]
    [SerializeField] private InventoryManager _inventoryMgr;

    [Header("INFOS : ")]
    [SerializeField] string _id;

    #endregion

    #region Interfaces

    //Interact
    public void Interact()
    {
        _inventoryMgr.Add(_id);
    }

    #endregion
}