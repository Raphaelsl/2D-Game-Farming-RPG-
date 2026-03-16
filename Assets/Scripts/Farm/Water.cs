using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private bool detectingPlayer;
    [SerializeField] private int waterValue;

    private PlayerItens player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerItens>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            player.WaterLimit(waterValue);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            detectingPlayer = false;
        }

    }
}
