using UnityEngine;

public class Parasol : MonoBehaviour
{
    [SerializeField] private ItemsDatabaseManager _databaseMgr;

    [SerializeField] private InventoryManager _inventoryMgr;
    private ItemData _equipedItem;

    [SerializeField] private string _id;

    private ItemData _myItem;
    private bool _isUsingItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_myItem = _databaseMgr.GetItem(_id);
    }

    // Update is called once per frame
    void Update()
    {
        //// -- EDIT --
        //// if item key pressed
        //if (Input.GetKey(KeyCode.J))
        //{
        //    if (_myItem.isEquiped)
        //    {
        //        _isUsingItem = !_isUsingItem;

        //        if (_isUsingItem)
        //        {
        //            UseItem();
        //        }
        //    }
        //}
    }

    public void UseItem()
    {
        _equipedItem = _inventoryMgr.equipedItem;
        if (_equipedItem != null)
        {
            Debug.Log($"I'm currently using {_equipedItem.genericData.ID}.");

            _equipedItem.OnUse?.Invoke();
        }


    }
}
