using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeUI : ClickableObject
{
    [SerializeField] private Text _upgradeText;

    private EventManager _eventManager;
    private int _index;

    public void Init(int index)
    {
        _eventManager = EventManager.GetEventManager();

        _index = index;
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);

        _eventManager.OnUpgradeUIClicked?.Invoke(_index);

        _soundManager.PlaySound(SoundTypes.OnClickUI);
    }

    public void SetText (string text)
    {
        _upgradeText.text = text;
    }
}
