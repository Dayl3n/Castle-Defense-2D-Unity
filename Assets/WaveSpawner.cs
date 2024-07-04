using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;



public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {Spawning,Waiting,Counting }


    public Wave[] waves;
    private int waveIndex = 0;
    public float timeBetweenWaves = 5f;
    private SpawnState state = SpawnState.Counting;
    [field: SerializeField] public float waveCountdown { get; private set; }

    private float searchCountDown = 1f;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }


    private void Update()
    {
        if(state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else return;
        }

        if(waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[waveIndex]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave " + wave.name);
        state = SpawnState.Spawning;

        for(int i =0; i < wave.amount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void WaveCompleted()
    {
        state = SpawnState.Counting;

        waveCountdown = timeBetweenWaves;


        if (waveIndex + 1 > waves.Length - 1)
        {
            Debug.Log("You win");
        }
        else
        {
            waveIndex++;
        }

        
    }

    void SpawnEnemy(Enemy enemy)
    {
        float offset = 2f * waveIndex; // Dostosuj tê wartoœæ wed³ug potrzeb

        // Instancjonuj przeciwnika z przesuniêciem pozycji
        Instantiate(enemy, transform.position + new Vector3(0f, offset, 0f), transform.rotation);
        Debug.Log("Spawning enemy");
    }

    private bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

}

[Serializable]
public class Wave
{
    public string name;
    public Enemy enemy;
    public int amount;
    public float spawnRate;


}