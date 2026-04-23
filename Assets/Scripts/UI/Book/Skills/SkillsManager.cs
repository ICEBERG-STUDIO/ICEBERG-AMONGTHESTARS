using UnityEngine;

using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SkillsManager : MonoBehaviour
{
    #region Variables
    public SkillData equipedItem { get; private set; }

    [Header("UI :")]
    [SerializeField] private List<Image> _imgSkill = new List<Image>();

    [Header("INFO :")]
    //[SerializeField] private Image _imgIcon;
    [SerializeField] private TMP_Text _txtLabel;
    [SerializeField] private TMP_Text _txtCaption;
    [SerializeField] private TMP_Text _txtControls;
    //[SerializeField] private GameObject _btnsParent;

    [Header("SLOT :")]
    //[SerializeField] private int _nbSlots = 6;
    //[SerializeField] private GameObject _slotDefault;
    //[SerializeField] private GameObject _slotsParent;

    [Header("MANAGER :")]
    [SerializeField] SkillsDatabaseManager _databaseMgr;

    private SkillsDatabase _database;
    private List<SkillData> _skillsData;

    private SkillData _currentSkillData;
    private SlotController _currentSc;

    private readonly List<SlotController> _slotControllers = new();

    #endregion

    #region Unity Event

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _database = _databaseMgr.skillsDatabase;
        _skillsData = _database.skillsData;

        // -- INIT --
        //InitSlots();
        InitView();
    }

    #endregion

    //private void InitSlots()
    //{
    //    for (int i = 0; i < _slotsParent.transform.childCount; i++)
    //        Destroy(_slotsParent.transform.GetChild(i).gameObject);

    //    for (int i = 0; i < _nbSlots; i++)
    //    {
    //        var newSlot = Instantiate(_slotDefault, _slotsParent.transform);
    //        if (newSlot.TryGetComponent(out SlotController sc))
    //        {
    //            sc.OnClick += () => DisplayView(sc);
    //            _slotControllers.Add(sc);
    //        }
    //    }
    //}

    private void InitSlots()
    {

        for (int i = 0; i < _imgSkill.Count; i++)
        {
            if (_imgSkill[i].TryGetComponent(out SlotController sc))
            {
                sc.OnClick += () => DisplayView(sc);
                //_slotControllers.Add(sc);
            }
        }
    }

    private void InitView()
    {
        for (int i = 0; i < _skillsData.Count; i++)
        {
            _imgSkill[i].sprite = _skillsData[i].genericData.icon;
            if (!_skillsData[i].isUnlocked) _imgSkill[i].color = Color.gray;
        }

        // -- DEFAULT VIEW --
        SkillData data = new SkillData();

        _txtLabel.text = data.genericData.label;
        _txtCaption.text = data.genericData.caption;
        _txtControls.text = data.controls;
    }

    public void DisplayView(SlotController sc)
    {
        // - SLOT -
        // color or move a frame above the slot
        if(_currentSc !=null) _currentSc.imgBcg.color = Color.white;
        sc.imgBcg.color = Color.red;

        // -- UPDATE CURRENT DATA --

        _currentSc = sc;
        //_currentSkillData = sc.Data;

        //// -- UPDATE CURRENT VIEW --

        //// - INFOS -
        ////_imgIcon.color = _currentItemData.color;
        //_txtLabel.text = _currentSkillData.genericData.label;
        //_txtCaption.text = _currentSkillData.genericData.caption;
        //_txtControls.text = _currentSkillData.controls;
    }

}
