using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private static string PLAYER_TAG = "Player"; 
    private float projectileSpeed = 20.0f;
    private Vector3 shootDirection;

    public void Setup(Vector3 shootDirection) {
        this.shootDirection = shootDirection;
    }

    private void Update() {
        float moveDistance = Time.deltaTime * projectileSpeed;

        transform.position += shootDirection * moveDistance;

        Transform objectHit = Physics2D.CircleCast(transform.position, 0.5f, shootDirection, moveDistance).transform;
        if (objectHit != null) {
            if(objectHit.CompareTag(PLAYER_TAG)) {
                objectHit.GetComponent<PlayerController>().TakeDamage(1);
                Debug.Log("Player has been damaged");
            }
            Destroy(gameObject);
        }
    }
}
