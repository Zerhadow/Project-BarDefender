using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float rate; //amount of time in between mob spawns
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start() {
        if(spawnPoints.Length == 0) {
            Debug.Log("No spawn points referenced");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update() {
        if(state == SpawnState.WAITING) {
            //check if enemies are still alive
            if(!EnemyIsAlive()) {
                Debug.Log("Wave completed");
                WaveCompleted();
            } else {
                return;
            }
        }

        if(waveCountdown <= 0) {
            if(state != SpawnState.SPAWNING) {
                //start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } else {
            waveCountdown -=Time.deltaTime;
        }
    }

    void WaveCompleted() {
        //begin new round
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            Debug.Log("Spawn state: " + state);
            Debug.Log("All waves complete! Looping. . .");
        } else {
            nextWave++;
        }
    }

    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;

        if(searchCountdown <= 0f) {

            if(GameObject.FindGameObjectsWithTag("Enemy") == null) {
                Debug.Log("No enemies alive");
                return false;
            }

            searchCountdown = 1f;
        }        

        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy) {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        //spawn enemy; change from random
        Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
