using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private float projectileSpeed = 10.0f;
    private Vector3 shootDirection;

    public void Setup(Vector3 shootDirection) {
        this.shootDirection = shootDirection;
    }

    private void Update() {
        transform.position += shootDirection * Time.deltaTime * 10.0f;
    }
}
