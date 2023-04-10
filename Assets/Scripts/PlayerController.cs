using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /*[SerializeField] private GameInput gaminput*/

    private int coinsCount = 0;
    private int healthPoints = 5;
    private float playerSpeed = 10;

    private void Update() {
        HandleMovement();
        
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetNormalizedMovementVector() * Time.deltaTime * playerSpeed;

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = +1 * Time.deltaTime * playerSpeed;
        }

        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1 * Time.deltaTime * playerSpeed;
        }

        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1 * Time.deltaTime * playerSpeed;
        }

        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = +1 * Time.deltaTime * playerSpeed;
        }

        transform.position += (Vector3)inputVector;

        Debug.Log(inputVector);
    }

}
