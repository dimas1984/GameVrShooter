using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name; //name of wave
        public Transform enemy; //set the location
        public int count; //set the number of enemies to spawn
        public float rate; //how soon after the last enemy does a new enemy appear
    }

    [SerializeField] Wave[] waves; //this will set number of different waves
    [SerializeField] Transform[] spawnPoints; // take the x, y, z values of a spawn point
    [SerializeField] float timeBetweenWaves = 5f;

    private int nextWave = 5;
    private float waveCountdown;
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING; //set the initial state to countdown


    // Start is called before the first frame update
    void Start()
    {
        // Error Checking - checking if spawn points have been placed

        if (spawnPoints.Length == 0) {
            print(" no spawn point referenced");
        }
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //check if enemy are still alive
            if (!EnemyIsAlive())
            {
                //begin next round
                WaveCompleted();
            }
            else
            {
                return; //wait until the if statement return true
            }
        }

        //if it's time to start spawning waves
        if (waveCountdown <= 0) {
            if (state != SpawnState.SPAWNING)
            {
                //start the spawning waves
                StartCoroutine(SpawnWave(waves[nextWave]));

            }
            else {
                waveCountdown -= Time.deltaTime;
            }

        }

        IEnumerator SpawnWave(Wave wave) {
            print("spawning wave:" + wave.name);
            state = SpawnState.SPAWNING;

            // spawn
            for (int i = 0; i < wave.count; i++)
            {
                //spawan an enemy, wait and the spawan another
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
            state = SpawnState.WAITING;
        }

        void SpawnEnemy(Transform enemy)
        {
            //choose random spawn point
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            //spawn enemy
            Instantiate(enemy, randomSpawnPoint.position, randomSpawnPoint.rotation);

            print("spawning Enemy" + enemy.name);

        }

        bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;

            if (searchCountdown <= 0)
            {
                //reset searchCountdown
                searchCountdown = 1f;
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    return false;
                }
            }
            return true;
        }

        void WaveCompleted()
        {
            print("wave completed");

            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;


            if (nextWave + 1 > waves.Length - 1)
            {
                // todo go to next level
                print("level Complete");
            }
        }
    }
}
