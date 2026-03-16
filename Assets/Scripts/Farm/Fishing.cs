using UnityEngine;

public class Fishing : MonoBehaviour
{
    private bool detectingPlayer;
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private int percentage;//porcentagem de chance de pescar um peixe
    private AnimationPlayer animPlayer; 
    private PlayerItens player;


    private void Awake()
    {
        player = FindObjectOfType<PlayerItens>();
        animPlayer = player.GetComponent<AnimationPlayer>();
    }
    void Start()
    {

    }


    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            animPlayer.onFishingStarted();
        }

    }
    public void onFishing()
    {
        int randomValue = Random.Range(1,100);
        if (randomValue <= percentage)
        {
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-2.50f,-1),0f,0f), Quaternion.identity);
        }
        else { 
        
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }

    }
}
