using NUnit.Framework;
using UnityEngine;

using System.Collections.Generic;

public class DrawingsInfoController : MonoBehaviour, IOnTrigger, IInteractable
{
    [Header("Database")]
    [SerializeField] private DrawingsDatabaseManager _databaseMgr;
    [SerializeField] private DrawingsDatabase _database;


    [Header("Drawing infos")]
    [SerializeField] private int id;
    [SerializeField] private Sprite _mySprite;

    private List<DrawingsData> _data;


    private DrawingsData _myDrawing;

    private bool _inTrigger;


    private void Awake()
    {
        UpdateMyDrawing(); 
    }

    private void Start()
    {
        //_data = FindFirstObjectByType<DrawingsDatabase>().drawingsData;


    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Debug.Log(_databaseMgr.GoodEndingUnlock());
        }
    }

    //private void Reset()
    //{
    //    //Debug.Log("reset");
    //}


    private void OnValidate()
    {
        UpdateMyDrawing();
        //Debug.Log("valid");
    }

    private void UpdateMyDrawing()
    {
        if(_databaseMgr !=null) _myDrawing = _databaseMgr.GetDrawing(id);
        //_mySprite = 
    }

    public void CollectDrawing()
    {
        //update drawing data in database
        _myDrawing.collectablesInfos.inInventory = true; 

        Destroy(gameObject);
    }


    //Interact
    public void Interact()
    {
        if(_inTrigger) CollectDrawing();
    }


    #region OnTrigger Interface

    //on enter
    public void OnEnter()
    {
        _inTrigger = true ;
    }


    //on exit
    public void OnExit()
    {
        _inTrigger = false;
    }

    #endregion
}
