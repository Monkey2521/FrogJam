using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private GameObject _UI;
    [SerializeField] private RestartMenu _restartMenu;

    [SerializeField][Range(0.001f, 0.05f)] private float _difficultyUpgrade;

    private float _timer = 0;
    public float Timer => _timer;

    private static float _difficultyMultiplier = 1f;
    public static float DifficultyMultiplier => _difficultyMultiplier;

    private static GameManager _instance;

    [SerializeField] private FrogKing _player;
    [SerializeField] private MosquitoSpawner _mosquitoSpawner;
    
    private EventManager _eventManager;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        else if (_isDebug)
        {
            Debug.Log("GameManager already created!");
        }
    }

    public static GameManager GetGameManager() => _instance;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnEnemyDeath.AddListener(UpgradeDifficulty);
        _eventManager.OnPlayerDeath.AddListener(StopGame);
    }

    private void UpgradeDifficulty ()
    {
        _difficultyMultiplier += _difficultyUpgrade;
    }

    private void Update()
    {
        _player.AttackControl();
    }

    private void FixedUpdate()
    {
        _mosquitoSpawner.MoveMosquitos();
    }

    [ContextMenu("Restart")]
    public void StartGame()
    {
        _restartMenu.HideRestart();
        _UI.SetActive(true);

        _eventManager.OnStartGame?.Invoke();
    }

    private void StopGame()
    {
        _restartMenu.ShowRestart() ;
        _UI.SetActive(false);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
