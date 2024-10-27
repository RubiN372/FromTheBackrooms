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

    speed = rb.linearVelocity.sqrMagnitude;
    animator.SetFloat("Speed", speed);
    animator.SetFloat("Velocity X", rb.linearVelocity.x);
    animator.SetFloat("Velocity Y", rb.linearVelocity.y);
  }
}