using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Button bildButton;

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
        bildButton.gameObject.SetActive(unlocked);
    }
}
