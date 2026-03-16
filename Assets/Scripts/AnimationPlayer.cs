using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [Header("AttackSettings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;
    [Header("Recovery")]
    [SerializeField] private float recoveryTime;
    private Player player;
    private Animator anim;
    private Fishing fishing;
    private bool isHitting;
    private float timeCount;
    

    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();

        fishing = FindObjectOfType<Fishing>();
    }

    void Update()
    {
        OnMove();
        OnRun();
        if (isHitting)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0;
            }
        }

    }

    #region Movement 
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("rolling"))
                {
                    anim.SetTrigger("isRoll");
                }          
            }
            else if (!player.isRolling)
            {
                anim.SetInteger("transition", 1);
            }
        }
        else
        {
            anim.SetInteger("transition", 0);
        }
        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        if (player.IsCutting)
        {
            anim.SetInteger("transition", 3);
        }
        if (player.IsDigging)
        {
            anim.SetInteger("transition", 4);
        }
        if (player.IsWatering)
        {
            anim.SetInteger("transition", 5);
        }
    }

    void OnRun()
    {
        if(player.isRunning && player.direction.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 2);
        }
    }
    #endregion

    #region Attack

    public void onAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);
        if (hit != null) 
        {
            //atacou o inimigo
            hit.GetComponentInChildren<AnimationControl>().onHit();
        
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }




    #endregion
    //chamado quando termina de exe anim de pesca
    public void onFishingStarted()
    {
        anim.SetTrigger("isFish");
        player.IsPaused = true;
    }
    public void onFishingEnded()
    {
        fishing.onFishing();
        player.IsPaused = false;
    }
    public void onHammeringStarted()
    {
        anim.SetBool("hammering", true);
    }
    public void onHammeringEnded()
    {
        anim.SetBool("hammering", false);
        
    }
    public void onHit()
    {
        if (!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
       
    }
}
