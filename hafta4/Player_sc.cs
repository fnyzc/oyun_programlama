
using UnityEngine;

public class Player_sc : MonoBehaviour
{
    public float hareketHizi = 5f;
    public GameObject laserPrefab;

    private float nextFire = 0;

    [SerializeField]
    private float fireRate = 2f;

    [SerializeField]
    private int lives = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    void CalculateMovement()
    {

        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        Vector3 hareket = new Vector3(yatay, dikey, 0) * hareketHizi * Time.deltaTime;
        transform.Translate(hareket);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        //üstteki satır ifelse görevini üstlendi

        /*if (transform.position.y >= 0.7)
        {
        transform.position = new Vector3(transform.position.x, 0.7f, 0);
        }
        else if (transform.position.y <= -3.6f)
        {
        transform.position = new Vector3(transform.position.x, -3.6f, 0);
        }*/

        if (transform.position.x >= 11.7f)
        {
            transform.position = new Vector3(11.7f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.7f)
        {
            transform.position = new Vector3(-11.7f, transform.position.y, 0);
        }

    }
    public void Damage()
    {
        lives--;

        if(lives == 0)
        {
            SpawnManager_sc spawnManager_Sc = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager_sc>();

            if(spawnManager_Sc!= null)
            {
                spawnManager_Sc.OnPlayerDeath();
            }
            else
            {
                Debug.LogError("Player_sc::Damage spawnManager is NULL");
            }
            Destroy(this.gameObject);
          
        }
    }
}
