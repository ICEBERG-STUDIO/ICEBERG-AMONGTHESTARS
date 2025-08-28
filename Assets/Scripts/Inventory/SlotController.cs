using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour, IPointerClickHandler
{
    public ItemData Data { get; set; }
    public int Quantity { get; private set; }
    public event Action OnClick;

    [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

    // INFOS 
    public Image imgBcg;
    [SerializeField] private Image _imgIcon;
    [SerializeField] private TMP_Text _txtQty;

    public ItemData _defaultData { get; private set ; }

    private void Awake()
    {
        if (TryGetComponent(out CanvasGroup cg))
            CanvasGroup = cg;

        _defaultData = new ItemData();
    }

    private void Start()
    {
        // INIT
        Data = _defaultData ;
        _txtQty.text = string.Empty;
    }

    // called in ItemsController
    public void Add(ItemData data)
    {
        if (Quantity == 0)
        {
            Data = data;
            _imgIcon.sprite = data.genericData.icon;
            //_imgIcon.color = data.genericData.color;
        }

        Quantity++;
        _txtQty.text = Quantity <= 1 ? string.Empty : Quantity.ToString();
    }

    public void Drop()
    {
        Quantity = Mathf.Max(0,Quantity-1);
        _txtQty.text = Quantity <= 1 ? string.Empty : Quantity.ToString();

        if (Quantity <= 0)
        {
            Data = _defaultData;

            _imgIcon.sprite = _defaultData.genericData.icon;
        }
    }

    //public void SetDisplay(bool value)
    //{
    //    CanvasGroup.alpha = value ? 1 : 0.5f;
    //    CanvasGroup.blocksRaycasts = !value;
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
