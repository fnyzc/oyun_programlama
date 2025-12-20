using UnityEngine;

public class Brain_sc : MonoBehaviour
{

    int DNALength = 2;
    public float timeAlive =0;
    public DNA_sc dna_sc;
    
    [SerializeField]
    GameObject eyes;

    bool isAlive= true;
    bool canSeeGround = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) return;
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward *10, Color.red, 1);
        canSeeGround = false;
        RaycastHit hit;

        // ışın oluştur ve kesişme var mı kontrol et
        if(Physics.Raycast(eyes.transform.position, eyes.transform.forward *10, out hit))
        {
            //ışın kesişmiştir. Neyle kesişti kontrolü
            if(hit.collider.gameObject.tag == "platform")
            {
                canSeeGround=true;
            }
        }
        timeAlive = PopulationManager_sc.elapsed;

        //DNA'dan oku ve ona göre hareket et 
        float turn=0;  //sağ/sol- y ekseni
        float move=0; // forward- z ekseni

        if (canSeeGround)
        {
            Debug.Log("!!");
            if(dna_sc.GetGene(0) == 0) move =1;
            else if (dna_sc.GetGene(0) == 1) turn = -90;
            else if (dna_sc.GetGene(0) == 2) turn = 90;
        }
        else
        {
            if(dna_sc.GetGene(1) == 0) move =1;
            else if (dna_sc.GetGene(1) == 1) turn = -90;
            else if (dna_sc.GetGene(1) == 2) turn = 90;
        }

        //Hareket ettir
        this.transform.Translate(0,0,move * 0.1f);
        this.transform.Rotate(0,turn,0);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "dead")
        {
            isAlive = false;
        }
    }

    public void Init()
    {
        Debug.Log("Brain_sc Init");
        dna_sc = new DNA_sc(DNALength, 3);
        timeAlive = 0;
        isAlive = true;
    }
}
