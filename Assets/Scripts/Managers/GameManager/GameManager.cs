using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [SerializeField] private GameObject _restartMenu;


    private EventManager _eventManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
