﻿using System;
using Events;
using General.EventManager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameConfigurationMenu.Scripts
{
    public class GameConfigurationMenuController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameConfigurationMenuView view;

        private void Awake()
        {
            EventManager.Register<bool>(GameConfigurationMenuEvents.SetButtonsInteractable, SetButtonsInteractable);
            EventManager.Register<bool>(GameConfigurationMenuEvents.SetStartButtonInteractable, SetStartButtonInteractable);
        }
        
        private void OnDestroy()
        {
            EventManager.Unregister<bool>(GameConfigurationMenuEvents.SetButtonsInteractable, SetButtonsInteractable);
            EventManager.Unregister<bool>(GameConfigurationMenuEvents.SetStartButtonInteractable, SetStartButtonInteractable);
        }

        private void Start()
        {
            view.SetStartGameClickListener(StartGame);
            view.SetBackClickListener(Back);
            SetStartButtonInteractable(false);
        }

        private void StartGame()
        {
            view.Hide();
            EventManager.Trigger(PlayerCreationEvents.Hide);
            EventManager.Trigger(BoardEvents.Show);
            EventManager.Trigger(PlayerManagerEvents.PositionPlayers);
            EventManager.Trigger(CameraEvents.EnableFollowTransform, true);
        }

        private void Back()
        {
            //TODO: load menu
        }

        private void SetButtonsInteractable(bool interactable)
        {
            view.SetBackButtonInteractable(interactable);
            view.SetStartButtonInteractable(interactable);
        }

        private void SetStartButtonInteractable(bool interactable)
        {
            view.SetStartButtonInteractable(interactable);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            EventManager.Trigger(TextFieldPopupEvents.Hide);
        }
    }
}
