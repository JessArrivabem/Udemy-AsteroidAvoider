using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera mainCamera;
    private float timer;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; // keeps going down every single frame until reaching zero and reseting

        if (timer <= 0) // spawns new asteroid
        {
            SpawnAsteroid();

            timer += secondsBetweenAsteroids; // reset timer
        }
    }

    private void SpawnAsteroid()
    {
        int sideScreen = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (sideScreen)
        {
            case 0:
                //Left
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                //Right
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                //Bottom
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                //Top
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint); // converts the world space to the device based on the size and shape
        worldSpawnPoint.z = 0;

        GameObject selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]; // picks random asteroid on the list (Red or Blue)

        GameObject asteroidInstance = Instantiate(selectedAsteroid, worldSpawnPoint, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))); 

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);


    }
}
