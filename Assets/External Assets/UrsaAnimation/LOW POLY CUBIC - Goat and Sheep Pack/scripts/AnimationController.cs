using UnityEngine;

namespace Ursaanimation.CubicFarmAnimals
{
    public class AnimationController : MonoBehaviour
    {
        public Animator animator;
        public string walkForwardAnimation = "walk_forward";
        public string runForwardAnimation = "run_forward";
        public string standtositAnimation = "stand_to_sit";

        public EnemyStatus enemy;
        void Start()
        {
            animator = GetComponent<Animator>();
            animator.Play(walkForwardAnimation);
        }

        public void dieAnimation()
        {
            animator.Play(standtositAnimation);
        }

        void Update()
        {
            if (enemy._state == EnemyStatus.State.Die)
            {
                dieAnimation();
                Debug.Log("die!");
                enemy.dieFlag = 2;
            }
        }
    }
}
