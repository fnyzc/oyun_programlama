using UnityEngine;

public class Enemy_sc : MonoBehaviour
{
    [SerializeField]
    int speed = 4;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (this.transform.position.y < -5.5f)
        {
            this.transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 7.4f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
           Player_sc player_sc = other.transform.GetComponent<Player_sc>();
           player_sc.Damage();


            Destroy(this.gameObject);
        }else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
