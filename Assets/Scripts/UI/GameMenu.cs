using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button buildButton;

    private void OnEnable()
    {
        ServiceLocator.Subscribe<GameMenu>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<GameMenu>();
    }

    public void BuildButtonSetState(bool unlocked)
    {
        buildButton.gameObject.SetActive(unlocked);
    }
}