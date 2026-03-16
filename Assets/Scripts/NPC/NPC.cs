using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    private int index;
    public List<Transform> paths = new List<Transform>();
    private float initialSpeed;
    private Animator anim;

    
    void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        
        if(DialogueControl.instance.IsShowing == true)
        {
            speed = 0f;
            anim.SetBool("isWalking",false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, paths[index].position)< 0.1f)
        {
            if (index < paths.Count - 1) {
                index++;
            }
            else
            {
                index = 0;
            }
        }
        Vector2 direction = paths[index].position - transform.position;
        if (direction.x > 0) {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (direction.x < 0) {
            transform.eulerAngles = new Vector2(0, 180);
        }

    }
}
