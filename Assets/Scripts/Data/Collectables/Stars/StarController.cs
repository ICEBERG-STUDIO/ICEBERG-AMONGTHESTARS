using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class StarController : MonoBehaviour, IInteractable
{
    #region Variables

    [Header("Database")]
    [SerializeField] private StarsDatabaseManager _databaseMgr;
    private StarsDatabase _database;

    [Header("Star infos")]
    [SerializeField, Tooltip("Id of the first star is 0, 2nd is 1 etc..."), ValidateInput("IsIDValid")] private int _id;

    [Header("Events")]
    [SerializeField] UnityEvent _OnCollect;

    // To add according to the requests of the GD
        //[SerializeField] private Sprite _mySprite;
        //private List<StarsData> _data;

    private StarData _myStar;


    // Check if id is valid
    private bool IsIDValid()
    {
        if (_id >= 0 && _id <= _database.starDatas.Count - 1) return true;

        Debug.LogError("Star Id must be between 0 and the number of stars - 1.");
        return false;
    }

    #endregion

    #region Event Functions

    private void Awake()
    {
        UpdateMyStar();
    }

    void Start()
    {
        _database = _databaseMgr.starsDatabase;
    }

    // update current star in editor 
    private void OnValidate() 
    {
        UpdateMyStar();
    }

    #endregion

    // update data according to the current id
    private void UpdateMyStar()
    {
        if (_databaseMgr != null) _myStar = _databaseMgr.GetStar(_id);
        //_mySprite = 
    }

    private void CollectStar()
    {
        //collect
        _myStar.isCollected = true;

        //on collect behaviour
        _OnCollect?.Invoke();

        Destroy(gameObject);
    }



    #region Interfaces

    //Interact
    public void Interact()
    {
        CollectStar();
    }
    #endregion
}
