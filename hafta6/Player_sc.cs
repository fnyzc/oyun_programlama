using System;
using System.Collections;
using UnityEngine;

public class Player_sc : MonoBehaviour
{
    public float hareketHizi = 5f;
    public GameObject laserPrefab;

    private float nextFire = 0;

    float speedMultiplier = 2;

    [SerializeField] 
    bool isTripleShotActive = false;

    [SerializeField]
    bool isSpeedBonusActive= false;
    
     [SerializeField]
    bool isShieldBonusActive= false;

    [SerializeField] 
    GameObject tripleLaserPrefab; 

    [SerializeField]
    GameObject shieldVisualizer;

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
         if (!isTripleShotActive)
        {
            Instantiate(laserPrefab, (this.transform.position + new Vector3(0, 1.05f, 0)), Quaternion.identity);
        }
        else
        {
            Instantiate(tripleLaserPrefab, (this.transform.position), Quaternion.identity);
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
        if (isShieldBonusActive)
        {
            isShieldBonusActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }
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

      public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotCancelRoutine());
    }

    public void SpeedBonusActive()
    {
        isSpeedBonusActive= true;
        hareketHizi *= speedMultiplier;
        StartCoroutine(SpeedBonusCancelRoutine());
    }

    public void ShieldBonusActive()
    {
        isShieldBonusActive= true;
        shieldVisualizer.SetActive(true);
    }

    IEnumerator TripleShotCancelRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }
     IEnumerator SpeedBonusCancelRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBonusActive = false;
        hareketHizi /= speedMultiplier;
    }
}
