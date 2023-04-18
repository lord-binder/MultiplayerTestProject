using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpawnerManager : MonoBehaviour {

    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private float coinTimerMax = 1f;
    private float coinTimer;

    private void Start() {
        coinTimer = coinTimerMax;
    }

    private void Update() {
        if(coinTimer < 0) {
            coinTimer = coinTimerMax;
            Debug.Log("Timer is out! Spawning coin!");
            Vector2 spawnPosition = new Vector2((Random.value * 10 - 5) * 2 * 0.9f, (Random.value * 10 - 5) * 2 * 0.9f);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }

        coinTimer -= Time.deltaTime;
    }
}


