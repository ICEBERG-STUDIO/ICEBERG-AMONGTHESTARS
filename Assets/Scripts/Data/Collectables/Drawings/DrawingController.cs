using UnityEngine;

using UnityEngine.Events;
using NaughtyAttributes;

public class DrawingController : MonoBehaviour, IInteractable
{
    #region Variables
    [Header("Database")]
    [SerializeField] private DrawingsDatabaseManager _databaseMgr;
    [SerializeField] private DrawingsDatabase _database;

    [Header("Drawing infos")]
    [SerializeField, Tooltip("Id of the first drawing is 0, 2nd is 1 etc..."), ValidateInput("IsIDValid")] private int _id;

    [Header("Events")]
    [SerializeField] UnityEvent _OnCollect;

    // To add according to the requests of the GD
    //[SerializeField] private Sprite _mySprite;
    //private List<DrawingsData> _data;


    private DrawingData _myDrawing;

    // check if ID is valid
    private bool IsIDValid()
    {
        if(_id >=0 && _id <= _database.drawingDatas.Count-1) return true;

        Debug.LogError("Drawing Id must be between 0 and the number of drawings - 1.");
        return false;
    }

    #endregion

    #region Event Functions

    private void Awake()
    {
        UpdateMyDrawing();
    }

    private void Start()
    {
        _database = _databaseMgr.drawingsDatabase;

    }


    void Update()
    {
        // -- DEBUG --
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{

        //    Debug.Log(_databaseMgr.GoodEndingUnlock());
        //}
    }

    // update current star in editor 
    private void OnValidate()
    {
        UpdateMyDrawing();
    }

    #endregion

    // update data according to the current id
    private void UpdateMyDrawing() 
    {
        if(_databaseMgr !=null)  _myDrawing = _databaseMgr.GetDrawing(_id);
    }

    public void CollectDrawing()
    {
        //collected
        _myDrawing.isCollected = true;

        //collect behaviour
        _OnCollect?.Invoke();

        Destroy(gameObject);
    }


    #region Interfaces

    //Interact
    public void Interact()
    {
        CollectDrawing();
    }

    #endregion
}
