using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private GameInput gameInput;

    private int coinsCount = 0;
    private int healthPoints = 5;
    private float moveSpeed = 10;

    private void Update() {
        HandleMovement();
        
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetNormalizedMovementVector();

        Debug.Log(inputVector);

        Vector3 moveDirection = new Vector2(inputVector.x, inputVector.y);
        Vector3 capsuleSecondPoint = transform.position + Vector3.up * 2;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.5f;

        bool canWalk = !Physics2D.CircleCast(transform.position, playerRadius, moveDirection, moveDistance);

        if (!canWalk) {
            // Cannot move towards Move Direction

            // Check if can move towards X direction

            Vector3 moveDirectionX = new Vector2(inputVector.x, 0);
            canWalk = !Physics2D.CircleCast(transform.position, playerRadius, moveDirectionX, moveDistance);

            if (canWalk) {
                // Can walk towards X direction
                moveDirection = moveDirectionX;
            } else {
                // Cannot move towards X direction

                // Check if can move towards Y direction

                Vector3 moveDirectionY = new Vector2(0, inputVector.y);
                canWalk = !Physics2D.CircleCast(transform.position, playerRadius, moveDirectionY, moveDistance);

                if (canWalk) {
                    // Can move towards Y direction
                    moveDirection = moveDirectionY;
                }
            }
        }

        if (canWalk) {
            //Can move towards Move Direction
            transform.position += moveDirection * moveDistance;
        }
    }
}
