using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject woodPrefab;
    private int totaWood;
    [SerializeField] private ParticleSystem leafs;

    private bool isCut;

    void Start()
    {

    }


    void Update()
    {

    }

    public void OnHit()
    {
        treeHealth--;
        anim.SetTrigger("isHit");
        leafs.Play();
        if (treeHealth <= 0)
        {
            totaWood = Random.Range(1, 5);// qtd de wood random
            for (int i = 0; i < totaWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), transform.rotation);
            }
            
            anim.SetTrigger("cut");
            isCut = true;   
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("axe")&& !isCut)
        {
            OnHit();
        }
    }
}
