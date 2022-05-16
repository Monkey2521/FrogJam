using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "New item", menuName = "Scriptable objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private string _description;
    public string Description => _description;

    [SerializeField] private Image _icon;
    public Image Icon => _icon;

}
