using System.Collections;
using UnityEngine;

public class SpawnManager_sc : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyContainer;

    [SerializeField]
    bool stopSpawning = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-7.5f, 7.5f), 7.4f, 0);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    
    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
