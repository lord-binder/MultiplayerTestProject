using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour {

    public static GameInput Instance {
        get; 
        private set;
    }

    public event EventHandler OnFireAction;

    private PlayerInputActions playerInputActions;

    private void Awake() {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Fire.performed += Fire_performed;
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnFireAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy() {
        playerInputActions.Dispose();
    }

    private void Update() {
        Debug.Log(playerInputActions.Player.Move.ReadValue<Vector2>());
    }

    public Vector2 GetNormalizedMovementVector() {

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }


}
