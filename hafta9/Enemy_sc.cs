using UnityEngine;

public class Enemy_sc : MonoBehaviour
{
    [SerializeField]
    int speed = 4;

     Player_sc player_sc;

    Animator animator;

    AudioSource audioSource;

    void Start()
    {
      
        player_sc = GameObject.Find("Player").GetComponent<Player_sc>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
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
           if(player_sc != null)
            {
                 player_sc.Damage();
            }
            animator.SetTrigger("OnEnemyDeath"); //kendini yok etmeden önce animasyon çalışıyor
            //Hızı sıfırlayacağız
            speed=0;
            audioSource.Play();
            Destroy(this.gameObject, 2.3f); 

        }else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);


            if(player_sc != null)
            {
                player_sc.AddScore(10);
            }
            animator.SetTrigger("OnEnemyDeath");
            speed =0;
            audioSource.Play();
            Destroy(this.gameObject, 2.3f);
        }
    }
}
