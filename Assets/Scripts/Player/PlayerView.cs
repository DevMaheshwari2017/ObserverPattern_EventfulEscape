﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private PlayerController playerController;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.stickyCursorLock = true;
#endif
    }
    private void Start() => playerRigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        playerController.Move(playerRigidbody, transform);
        playerController.Jump(playerRigidbody, transform);
        playerController.Interact();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            GameService.Instance.GetInstructionView().ShowInstruction(InstructionType.Interact);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        IInteractable interactable;
        if (other.TryGetComponent(out interactable) && playerController.IsInteracted)
        {
            playerController.IsInteracted = false;
            interactable.Interact();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            GameService.Instance.GetInstructionView().HideInstruction();
        }
    }
    public void SetController(PlayerController _playerController) => playerController = _playerController;

}