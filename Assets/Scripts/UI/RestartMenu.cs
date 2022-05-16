using UnityEngine;

public class RestartMenu : MonoBehaviour
{
    private EventManager _eventManager;

    private void Start()
    {
        _eventManager = EventManager.GetEventManager();
        _eventManager.OnPlayerDeath.AddListener(ShowRestart);
        _eventManager.OnPlayerDeath.AddListener(HideRestart);
    }

    private void ShowRestart()
    {

    }

    private void HideRestart()
    {
        this.gameObject.SetActive(false);
    }
}
