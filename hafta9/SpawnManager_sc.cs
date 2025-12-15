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
   
    //[SerializeField]
    //private GameObject tripleShotBonusPrefab;

    [SerializeField]
    GameObject[] bonusPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-7.5f, 7.5f), 7.4f, 0);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnBonusRoutine()
    {
        while (stopSpawning == false)
        {
            int waitTime = Random.Range(3,8);
            Debug.Log("Üçlü atış bekleme süresi: " + waitTime);
            yield return new WaitForSeconds((float) waitTime);
            Vector3 position = new Vector3(Random.Range(-7.5f, 7.5f), 7.4f, 0);

            int randomBonus = Random.Range(0,3);
            Instantiate(bonusPrefabs[randomBonus], position, Quaternion.identity);
        
            
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnBonusRoutine());
    }
    
    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
