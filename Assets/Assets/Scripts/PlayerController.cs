using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public GameObject _Projectileprefab;
    public Vector2 upBoundary = new Vector2(-13.23f, 6.62f);
    public Vector2 downBoundary = new Vector2(13.38f, -6.84f);
    public HealthBarController healthBarController;
    public int life = 100;

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mousePosition.x);

        Debug.DrawRay(transform.position, mousePosition.normalized * rayDistance, Color.red);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            GameObject projectile = Instantiate(_Projectileprefab, transform.position, transform.rotation);
            ProyectilControler projectileController = projectile.GetComponent<ProyectilControler>();
            if (projectileController != null)
            {
                Vector2 shootingDirection = (mousePosition - (Vector2)transform.position).normalized;projectileController.SetDirection(shootingDirection);
            }
        }

        if (life <= 0)
        {
            SceneManager.LoadScene("Lose");

        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movementPlayer = context.ReadValue<Vector2>();

        Vector2 nextPosition = myRBD2.position + movementPlayer * velocityModifier * Time.fixedDeltaTime;

        if (nextPosition.x > upBoundary.x && nextPosition.x < downBoundary.x &&
            nextPosition.y > downBoundary.y && nextPosition.y < upBoundary.y)
        {
            myRBD2.velocity = movementPlayer * velocityModifier;
        }
        else
        {
            Vector2 newPosition = new Vector2(Mathf.Clamp(nextPosition.x, upBoundary.x, downBoundary.x),Mathf.Clamp(nextPosition.y, downBoundary.y, upBoundary.y));

            myRBD2.MovePosition(newPosition);

            myRBD2.velocity = Vector2.zero;
        }

        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }

    private void CheckFlip(float x_Position)
    {
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            if (healthBarController != null)
            {
                healthBarController.UpdateHealth(-10);
                life = life - 12;
                
            }
        }
    }
}
