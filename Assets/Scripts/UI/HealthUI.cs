using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private FrogKing _player;
    [SerializeField] private Image _playerHealthBar;
    [SerializeField] private Text _playerHealthPoints;

    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnPlayerChangeHP.AddListener(UpdateUI);
    }

    public void UpdateUI()
    {
        _playerHealthBar.fillAmount = _player.HP / _player.MaxHP;
        _playerHealthPoints.text = (int)_player.HP + " / " + _player.MaxHP;
        
        if (_player.HP / _player.MaxHP <= 1f && _player.HP / _player.MaxHP >= 0.66f)
        {
            _playerHealthPoints.color = Color.green;
        }
        else if (_player.HP / _player.MaxHP <= 0.66f && _player.HP / _player.MaxHP >= 0.33f)
        {
            _playerHealthPoints.color = Color.yellow;
        }
        else
        {
            _playerHealthPoints.color = Color.red;
        }
    }
}
