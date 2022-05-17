using UnityEngine;
using UnityEngine.UI;

public class RestartMenu : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void ShowRestart()
    {
        this.gameObject.SetActive(true);
        _text.text = "Total score: " + ScoreCounter.Score;
    }

    public void HideRestart()
    {
        this.gameObject.SetActive(false);
    }
}
