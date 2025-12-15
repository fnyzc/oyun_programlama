using UnityEngine;

public class Bonus_sc : MonoBehaviour
{
    [SerializeField]
    float speed = 3;

    [SerializeField]
    int bonusId;

    [SerializeField]
    AudioClip audioClip;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (this.transform.position.y < -5.8f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")     
        {

            Player_sc player_sc = other.transform.GetComponent<Player_sc>();
            if (player_sc != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, this.transform.position, 0.9f);
                switch (bonusId)
                {
                    case 0:
                    player_sc.TripleShotActive();
                    break;

                    case 1:
                    player_sc.SpeedBonusActive();
                    break;

                    case 2:
                    player_sc.ShieldBonusActive();
                    break;

                    default:
                    Debug.Log("Hata Durumu!");
                    break;
                }
                
            }

            Destroy(this.gameObject);   
        }
    }
}
