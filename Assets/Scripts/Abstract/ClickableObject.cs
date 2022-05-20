using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SoundManager))]
public abstract class ClickableObject : MonoBehaviour, IClickable
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected SoundManager _soundManager;

    public virtual void OnPointerClick (PointerEventData pointerEventData)
    {
        if (_isDebug) Debug.Log("Click on " + name);
    }
}
