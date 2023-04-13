using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    private const int LAYER_PLAYER = 3;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileFirePoint;

    private EventHandler OnDamageTaken;

    private int coinsCount;
    private int healthPoints;
    private float moveSpeed = 10;
    private Vector3 lastMoveDirection;

    private void Awake() {
        lastMoveDirection = Vector3.right;
        coinsCount = 0;
        healthPoints = 5;
    }

    private void Start() {
        GameInput.Instance.OnFireAction += GameInput_OnFireAction;
    }

    private void GameInput_OnFireAction(object sender, System.EventArgs e) {
        FireProjectile();
    }

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetNormalizedMovementVector();

        Vector3 moveDirection = new Vector2(inputVector.x, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.5f;

        bool canWalk = IsDirectionHasObstacle(moveDirection);

        // Try move in single direction when player move in diagonal direction
        if (!canWalk) {
            // Cannot move towards Move Direction

            Vector3 moveDirectionX = new Vector2(inputVector.x, 0);
            canWalk = IsDirectionHasObstacle(moveDirectionX);

            // Check if can move towards X direction
            if (canWalk) {
                // Can walk towards X direction
                moveDirection = moveDirectionX;
            } else {
                // Cannot move towards X direction

                Vector3 moveDirectionY = new Vector2(0, inputVector.y);
                canWalk = IsDirectionHasObstacle(moveDirectionY);

                // Check if can move towards Y direction
                if (canWalk) {
                    // Can move towards Y direction
                    moveDirection = moveDirectionY;
                }
            }
        }

        // Try move in Move Direction
        if (canWalk) {
            //Can move towards Move Direction
            transform.position += moveDirection * moveDistance;
        }

        // Set last move direction and FirePoint position if moveDirection is not zero
        // Last move direction should not be zero, otherwise it would not make sense
        if (moveDirection != Vector3.zero) { 
            lastMoveDirection = moveDirection;
            SetFirePointPosition();
        }

        RaycastHit2D objectHit = Physics2D.CircleCast(transform.position, playerRadius, moveDirection, moveDistance, LAYER_PLAYER);

        if (objectHit && objectHit.transform.CompareTag("Coin")) {
            Destroy(objectHit.transform.gameObject);
            AddCoins(1);
            Debug.Log(coinsCount);
        }
    }

    private bool IsDirectionHasObstacle(Vector3 moveDirection) {
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.5f; // Circle size is 1m, so radius = size / 2

        RaycastHit2D objectHit = 
            Physics2D.CircleCast(transform.position, playerRadius, moveDirection, moveDistance, LAYER_PLAYER);

        return !objectHit;
    }

    private void FireProjectile() {
        GameObject projectileFired = Instantiate(projectilePrefab, projectileFirePoint.position, Quaternion.identity);
        projectileFired.GetComponent<ProjectileController>().Setup(lastMoveDirection);
    }

    private void SetFirePointPosition() {
        projectileFirePoint.position = transform.position + lastMoveDirection;

    }

    public void TakeDamage(int damage) {
        OnDamageTaken?.Invoke(this, EventArgs.Empty);
        healthPoints -= damage;
        Debug.Log(healthPoints);
    }

    public void AddCoins(int coinAmount) {
        coinsCount++;
    }
}
