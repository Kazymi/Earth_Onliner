using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListRoomMenu : MonoBehaviour
{
   [SerializeField] private Button toTitleButton;

   private MainMenuSystem _mainMenuSystem;

   private void OnEnable()
   {
      toTitleButton.onClick.AddListener(ToTitle);
   }

   private void OnDisable()
   {
      toTitleButton.onClick.RemoveListener(ToTitle);
   }

   private void Start()
   {
      _mainMenuSystem = ServiceLocator.GetService<MainMenuSystem>();
   }

   private void ToTitle()
   {
      _mainMenuSystem.OpenMenu(MainMenuCanvasType.Title);
   }

}
