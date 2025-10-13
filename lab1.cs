using UnityEngine;

public class KontrolScript : MonoBehaviour
{
    // Inspector'da gözüken değişken (public)
    public float speed = 2f; 

    // Inspector'da görünmeyen, sadece kod içinde kullanılan değişken (private)
    private float multiplier = 1f;

    void Start()
    {
        //1. adım: 
        transform.position = transform.position + new Vector3(0f, 1f, 0f);
    }

    void Update()
    {
        // 2 ve 3. adımlar: tek yönde ilerleme, zaman normalizasyonu 
        transform.position = transform.position + new Vector3(0f, 0f, speed * multiplier * Time.deltaTime);

        // 4. adım: klavye kontrol
        float vertical = Input.GetAxis("Vertical"); 
        float horizontal = Input.GetAxis("Horizontal"); 

        // Dikey eksenlerde hareket
        Vector3 move = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        // 5. adım: hem dikey hem yatay eksen 
        transform.position = transform.position + move;
    }
}
