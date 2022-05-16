using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private Text _text;
    private int _score;
    private EventManager _eventManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnEnemyDeath.AddListener(UpdateCounter);
        _eventManager.OnStartGame.AddListener(Restart);
    }

    public void Restart()
    {
        _score = 0;
        _text.text = "000";

        if (_isDebug) Debug.Log("Reset counter");
    }

    private void UpdateCounter()
    {
        _score++;
        int scoreLen = _score.ToString().Length;
        _text.text = (scoreLen == 1 ? "00" : scoreLen == 2 ? "0" : "" ) + _score;
    }
}
