using Unity.VisualScripting;
using UnityEngine;

public class Slot_Farm : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount;//qtd de escavaçao
    [SerializeField] private bool detecting;
    [SerializeField] private float waterAmount;//total de agua pra nascer uma cenoura

    private int initialDigAmount;
    private float currentWater;
    private bool dugHole;
    private bool plantedCarrot;//checar se a carrot esta plantada
    private bool isPlayer;//player encostando

    PlayerItens playerItens;
    private void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
    }
    private void Start()
    {
        initialDigAmount = digAmount;
        
    }
    private void Update()
    {
        if (dugHole) {
            if (detecting)
            {
                currentWater += 0.1f;
            }
            if (currentWater >= waterAmount && !plantedCarrot)
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRenderer.sprite = carrot;
                plantedCarrot = true;
                
            }
            if (Input.GetKeyDown(KeyCode.E) && plantedCarrot && isPlayer)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRenderer.sprite = hole;
                playerItens.TotalCarrot++;
                currentWater = 0;
            }
        }
        
    }


    public void OnHit()
    {
        digAmount--;
        if(digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;

        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("dig"))
        {
            OnHit();
        }
        if (collision.CompareTag("water"))
        {
            detecting = true;
        }
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
        {
            detecting = false;
        }
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
