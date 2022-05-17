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
        _eventManager.OnCharacterTakeDamage.AddListener(ShowDamage);
    }

    private void ShowDamage (IAttackable dealer, IDamageable target, float damage)
    {
        if (target is FrogKing) UpdateUI();
    }

    public void UpdateUI()
    {
        _playerHealthBar.fillAmount = _player.HP / _player.MaxHP;
        _playerHealthPoints.text = (int)_player.HP + " / " + _player.MaxHP;
        
        if (_player.HP / _player.MaxHP <= 1) 
            _playerHealthPoints.color = Color.green;
        else if (_player.HP / _player.MaxHP <= 0.66 && _player.HP / _player.MaxHP <= 0.33)
            _playerHealthPoints.color = Color.yellow;
        else 
            _playerHealthPoints.color = Color.red;
    }
}
