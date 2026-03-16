using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    private AnimationPlayer player;
    
    
    private Animator anim;
    private Skeleton skeleton;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<AnimationPlayer>();
        skeleton = GetComponentInParent<Skeleton>();
    }
    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }
    public void Attack()
    {
        if (!skeleton.IsDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);
            if (hit != null)
            {
                //batendo no player
                player.onHit();
            }
            
        }
        
    }
    public void onHit()
    {
        
        if(skeleton.CurrentHealth <= 0)
        {
            skeleton.IsDead = true;
            anim.SetTrigger("death");

            Destroy(skeleton.gameObject, 1f);
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.CurrentHealth--;
            skeleton.HealthBar.fillAmount = skeleton.CurrentHealth / skeleton.TotalHealth;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
