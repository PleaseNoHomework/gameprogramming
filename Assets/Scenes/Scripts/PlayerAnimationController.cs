using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public string idle = "idle"; //기본 서있는 상태인가
    public string walk = "walk";
    public string run = "run";
    public string pick_up = "pick_up";
    public player p;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(pick_up);
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
