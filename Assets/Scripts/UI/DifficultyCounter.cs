using UnityEngine;
using UnityEngine.UI;

public class DifficultyCounter : MonoBehaviour
{
    [SerializeField] private Text _text;

    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnEnemyDeath.AddListener(UpdateCounter);
        _eventManager.OnStartGame.AddListener(Restart);
    }

    private void Restart ()
    {
        _text.text = "Difficulty:\n0";
    }

    private void UpdateCounter()
    {
        _text.text = "Difficulty:\n" + ((GameManager.DifficultyMultiplier - 1f) * 10f).ToString().Split(".")[0];
    }
}
