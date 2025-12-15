using UnityEngine;

public class Asteroid_sc : MonoBehaviour
{
    float rotateSpeed = 20.0f;

    [SerializeField]
    GameObject explosionPrefab;

    SpawnManager_sc spawnManager_sc;

    void Start()
    {
        spawnManager_sc = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager_sc>();
        if(spawnManager_sc == null)
        {
            Debug.Log("Asteroid_sc::Start, spawnManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            spawnManager_sc.StartSpawning();
            Destroy(this.gameObject);
        }
    }
}
