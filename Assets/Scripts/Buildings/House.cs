using System.Runtime.InteropServices;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;
    [SerializeField] private GameObject collider;

    private float timeCount;
    private bool isBegining;
    private AnimationPlayer animPlayer;
    

    private Player player;
    private bool detectingPlayer;
    private PlayerItens playerItens;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        animPlayer = player.GetComponent<AnimationPlayer>();
        playerItens = player.GetComponent<PlayerItens>();
    }
    void Start()
    {

    }


    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItens.totalWoods >= woodAmount)
        {
            //casa é inicializada
            isBegining = true;
            animPlayer.onHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
            player.IsPaused = true;
            playerItens.totalWoods -= woodAmount;
        }
        if (isBegining)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount)
            {
                //casa é finalizada
                animPlayer.onHammeringEnded();
                houseSprite.color = endColor;
                player.IsPaused = false;
                collider.SetActive(true);

            }
            
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
