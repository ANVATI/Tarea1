using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float normalVelocityModifier = 1.25f;
    [SerializeField] private float playerCollisionVelocityModifier = 6f;
    [SerializeField] private float velocityModifier;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    public LayerMask playerLayerMask;
    public HealthBarController healthBarController;

    private void Start()
    {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
        velocityModifier = normalVelocityModifier;
    }

    private void Update()
    {
        CheckNewPoint();

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }

    private void CheckNewPoint()
    {
        Debug.DrawRay(transform.position, myRBD2.velocity.normalized * 3f, Color.yellow);
        if (Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25)
        {
            patrolPos = (patrolPos + 1) % checkpointsPatrol.Length;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, myRBD2.velocity.normalized, 3f, playerLayerMask);
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            Debug.DrawRay(transform.position, myRBD2.velocity.normalized * 3f, Color.red);
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * playerCollisionVelocityModifier;
        }
        else
        {
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized * normalVelocityModifier;
        }
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Proyectil")
        {
            if (healthBarController != null)
            {
                healthBarController.UpdateHealth(-10);
            }
        }
    }
}
