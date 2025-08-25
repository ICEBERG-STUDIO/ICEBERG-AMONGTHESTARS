using UnityEngine;

using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    #region Variables
    public ItemData equipedItem { get; private set; }

    [Header("UI :")]
    [SerializeField] private Image _imgEquipment;

    [Header("INFO :")]
    [SerializeField] private Image _imgIcon;
    [SerializeField] private TMP_Text _txtLabel;
    [SerializeField] private TMP_Text _txtCaption;
    [SerializeField] private GameObject _btnsParent;

    [Header("SLOT :")]
    [SerializeField] private int _nbSlots = 6;
    [SerializeField] private int _quantityMax = 5;
    [SerializeField] private GameObject _slotDefault;
    [SerializeField] private GameObject _slotsParent;

    [Header("MANAGER :")]
    [SerializeField] ItemsDatabaseManager _databaseMgr;

    private ItemsDatabase _database;

    private ItemData _currentItemData;
    private SlotController _currentSc;

    private readonly List<SlotController> _slotControllers = new();

    #endregion

    #region Unity Event

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _database = _databaseMgr.itemsDatabase;

        // -- INIT --
        InitSlots();
        InitView();
    }

    #endregion

    private void InitSlots()
    {
        for (int i = 0; i < _slotsParent.transform.childCount; i++)
            Destroy(_slotsParent.transform.GetChild(i).gameObject);

        for (int i = 0; i < _nbSlots; i++)
        {
            var newSlot = Instantiate(_slotDefault, _slotsParent.transform);
            if (newSlot.TryGetComponent(out SlotController sc))
            {
                sc.OnClick += () => DisplayView(sc);
                _slotControllers.Add(sc);
            }
        }
    }

    private void InitView()
    {
        ItemData data = new ItemData();

        // -- UPDATE CURRENT VIEW --

        _imgIcon.sprite = data.genericData.icon;
        _imgEquipment.sprite = data.genericData.icon; 
        //_imgIcon.color = _currentItemData.color;
        _txtLabel.text = data.genericData.label;
        _txtCaption.text = data.genericData.caption;
        _btnsParent.SetActive(false);
    }

    public void DisplayView(SlotController sc)
    {
        // - SLOT -
        // color or move a frame above the slot
        if(_currentSc !=null) _currentSc.imgBcg.color = Color.white;
        sc.imgBcg.color = Color.red;

        // -- UPDATE CURRENT DATA --

        _currentSc = sc;
        _currentItemData = sc.Data;

        // -- UPDATE CURRENT VIEW --

        // - INFOS -
        _imgIcon.sprite = _currentItemData.genericData.icon;
        //_imgIcon.color = _currentItemData.color;
        _txtLabel.text = _currentItemData.genericData.label;
        _txtCaption.text = _currentItemData.genericData.caption;
        _btnsParent.SetActive(!(sc.Data == sc._defaultData)); // disable or enable btns
    }

    #region Buttons

    // -- BUTTONS ---
    public void Add(string id)
    {
        ItemData data;

        try
        {
            data = _databaseMgr.GetItem(id);

            SlotController sc = null;

            sc = _slotControllers.FirstOrDefault(x => x.Data.genericData.ID == data.genericData.ID && x.Quantity < _quantityMax);
            if (sc == default)
                sc = _slotControllers.FirstOrDefault(x => x.Quantity == 0);

            sc?.Add(data);
        }
        catch
        {
            Debug.LogError($"The item {id} doesn't exist in the database");
        }

    }

    public void Drop()
    {
        _currentSc?.Drop();

        DisplayView(_currentSc);
    }

    #endregion

    public void Equip()
    {
        //-- DEBUG --
        Debug.Log("equip " + _currentItemData.genericData.label);

        // -- EDIT --
        _currentItemData.Equip();
        equipedItem = _currentItemData;

        _imgEquipment.sprite = equipedItem.genericData.icon;

        //Drop();

    }

}
