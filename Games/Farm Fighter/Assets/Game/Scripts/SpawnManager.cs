using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] PowerUpPrefabs;
    [SerializeField] private GameObject EnemyPrefab;

    public float spawnTime = 5.0f;
    public bool spawning = false;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.gameOver)
        {
            StopAllCoroutines();
        }
    }

    public IEnumerator EnemySpawn()
    {
        float xPos = Random.Range(-7.5f, 7.5f);
        Instantiate(EnemyPrefab, new Vector3(xPos, 6.0f, 0), Quaternion.identity);
        while (!GM.gameOver)
        {
            yield return new WaitForSeconds(spawnTime);
            xPos = Random.Range(-7.5f, 7.5f);
            Instantiate(EnemyPrefab, new Vector3(xPos, 6.0f, 0), Quaternion.identity);
            if (spawnTime > 3.0)
            {
                spawnTime -= 0.5f;
            } else if (spawnTime > 2.0f)
            {
                spawnTime -= 0.25f;
            } else if (spawnTime > 1.0f)
            {
                spawnTime -= 0.1f;
            }
        } //end while

    } //end EnemySpawn

    public IEnumerator PowerUpSpawn()
    {
        while (!GM.gameOver)
        {
            yield return new WaitForSeconds(8.0f);
            int powerUp = Random.Range(0, 3);
            float xPos = Random.Range(-7.5f, 7.5f);
            Instantiate(PowerUpPrefabs[powerUp], new Vector3(xPos, 6.0f, 0), Quaternion.identity);
        }
    } //end PowerUpSpawn

    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerUpSpawn());
    } //end StartSpawnRoutines
}
