using UnityEngine;
using UnityEngine.UI;

public class DifficultyCounter : MonoBehaviour
{
    [SerializeField] private Text _text;

    private EventManager _eventManager;

    [SerializeField] private SoundManager _soundManager;

    private int _difficulty = 0;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnEnemyDeath.AddListener(UpdateCounter);
        _eventManager.OnStartGame.AddListener(Restart);
    }

    private void Restart ()
    {
        _text.text = "Difficulty:\n0";
        _difficulty = 0;
    }

    private void UpdateCounter()
    {
        int difficulty = ToInt(((GameManager.DifficultyMultiplier - 1f) * 10f).ToString().Split(".")[0]);

        if (difficulty > _difficulty)
        {
            _difficulty++;
            _soundManager.PlaySound(SoundTypes.OnDifficultyUpgrade);

            _text.text = "Difficulty:\n" + _difficulty;
        }
    }
    private int ToInt(string str)
    {
        int x = 0, index = 0;

        foreach (char c in str)
        {
            x += ((int)c - (int)'0') * (index > 0 ? 10 * index : 1);
        }

        return x;
    }
}
