using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Skeleton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;
    private Player player;
    [Header("Stats")]
    [SerializeField] private float currentHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private float totalHealth;
    private bool isDead;
    private bool detectPlayer;
    
    public Image HealthBar { get => healthBar; set => healthBar = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float TotalHealth { get => totalHealth; set => totalHealth = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    
    void Update()
    {
        if (!IsDead && detectPlayer)
        {
            agent.SetDestination(player.transform.position);

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                //chegou no limt de dist
                animControl.PlayAnim(2);
            }
            else
            {
                //skel seguindo player
                animControl.PlayAnim(1);
            }
            float posX = player.transform.position.x - transform.position.x;
            if (posX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        
    }
    private void FixedUpdate()
    {
        DetectPlayer();
    }
    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);
        if (hit != null) {
            detectPlayer = true;
            agent.isStopped = false;

        }
        else
        {
            detectPlayer = false;
            animControl.PlayAnim(0);
            agent.isStopped = true;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
