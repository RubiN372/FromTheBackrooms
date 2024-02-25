using UnityEngine;
using Pathfinding;

public class MonsterEnemyGFX : MonoBehaviour
{
  [SerializeField] Animator animator;
  private float speed;
  MonsterEnemyAI monsterScript;
  Rigidbody2D rb;
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }


  private void Update()
  {

    speed = rb.velocity.sqrMagnitude;
    animator.SetFloat("Speed", speed);
    animator.SetFloat("Velocity X", rb.velocity.x);
    animator.SetFloat("Velocity Y", rb.velocity.y);
  }
}