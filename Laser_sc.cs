using UnityEngine;

public class Laser_sc : MonoBehaviour
{
[SerializeField]
private int speed = 5;

// Update is called once per frame
void Update()
{
this.transform.Translate(Vector3.up * speed * Time.deltaTime);

if(this.transform.position.y > 2.5f)
{
Destroy(this.gameObject);
}

}
}