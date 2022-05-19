using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoSpawner : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private int _maxMosquitos;
    [SerializeField] private EnemyMosquitoController _mosquitoPrefab;
    [Tooltip("Delay in seconds")][Range(0.1f, 5f)]
    [SerializeField] private float _spawnDelay;

    private List<EnemyMosquitoController> _mosquitos = new List<EnemyMosquitoController>();
    private List<EnemyMosquitoController> _pull = new List<EnemyMosquitoController>();

    [SerializeField] private GameObject _target;

    private EventManager _eventManager;

    #region Spawn area vars
    private const float MAX_X = 1000f;
    private const float MIN_X = -1000f;
    private const float MIN_MAX_X = 960f;
    private const float MAX_MIN_X = -960f;

    private const float MAX_Y = 600f;
    private const float MIN_Y = -600f;
    private const float MIN_MAX_Y = 560f;

    private int _sideCounter = 0;
    #endregion

    private void Start ()
    {
        for (int i = 0; i < _maxMosquitos; i++)
        {
            EnemyMosquitoController enemy = (EnemyMosquitoController)Instantiate(_mosquitoPrefab, transform);
            enemy.transform.position = GetRandomPosition();
            enemy.gameObject.SetActive(false);

            _pull.Add(enemy);
        }

        if (_isDebug) Debug.Log("Spawned " + _pull.Count + " mosquitos");

        _eventManager = EventManager.GetEventManager();
        _eventManager.OnEnemySpawned.AddListener(AutoSpawner);
        _eventManager.OnStartGame.AddListener(SpawnMosuito);
    }

    private async void AutoSpawner ()
    {
        if (_mosquitos.Count > 0) 
            await Task.Delay((int)(_spawnDelay * 1000f / GameManager.DifficultyMultiplier));
        
        if (_pull.Count > 0)
            SpawnMosuito();
    }

    private void SpawnMosuito()
    {
        int index = Random.Range(0, _pull.Count);

        EnemyMosquitoController enemy = _pull[index];
        enemy.gameObject.SetActive(true);

        _mosquitos.Add(enemy);
        _pull.Remove(enemy);

        if (_isDebug) Debug.Log("Mosquito with ID: " + enemy.GetInstanceID() + " is active");

        _eventManager.OnEnemySpawned?.Invoke();
    }

    public void MoveMosquitos()
    {
        Vector3 targetPos = _target.transform.position;

        foreach(EnemyMosquitoController mosquito in _mosquitos)
        {
            mosquito.Move(targetPos);
        }
    }

    public void AddToPull(EnemyMosquitoController mosquito)
    {
        if (_mosquitos.Contains(mosquito))
        {
            if (_isDebug) Debug.Log("Destroy mosquito with ID: " + mosquito.GetInstanceID());
            _mosquitos.Remove(mosquito);
        }
        else if (_isDebug) Debug.Log("Mosquito with ID: " + mosquito.GetInstanceID() + " not found!");
        
        _pull.Add(mosquito);

        mosquito.gameObject.transform.position = GetRandomPosition();
        mosquito.gameObject.SetActive(false);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;

        switch (_sideCounter)
        {
            case 0:
                randomPosition.x = Random.Range(MIN_X, MAX_X);
                break;
            case 1:
                randomPosition.x = Random.Range(MIN_X, MAX_MIN_X);
                break;
            case 2:
                randomPosition.x = Random.Range(MIN_MAX_X, MAX_X);
                break;
            default:
                if (_isDebug) Debug.Log("?????");
                _sideCounter = 0;
                break;
        }
        
        randomPosition.y = randomPosition.x < MAX_MIN_X || randomPosition.x > MIN_MAX_X ?
                            Random.Range(MIN_Y, MAX_Y) : Random.Range(MIN_MAX_Y, MAX_Y);

        _sideCounter = _sideCounter == 2 ?  0 : _sideCounter + 1;

        return randomPosition;
    }
}
