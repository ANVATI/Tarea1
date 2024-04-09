using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    private bool playerInsideArea = false;
    private float timeBetweenShots = 1.6f;
    private float elapsedTime = 0f;
    [SerializeField] private float moveSpeed = 2f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public HealthBarController healthBarController;

    private void Start()
    {
        initialPosition = new Vector3(5.87f, -1.01f, 0f);
        targetPosition = initialPosition;
    }
    private void Update()
    {
        if (playerInsideArea)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            Vector3 direction = (initialPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        if (playerInsideArea && elapsedTime >= timeBetweenShots)
        {
            ShootProjectile();
            elapsedTime = 0f; 
        }

        elapsedTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            playerInsideArea = true;
            targetPosition = other.transform.position;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInsideArea = false;
            targetPosition = initialPosition;
        }
        if (other.tag == "Proyectil")
        {
            if (healthBarController != null)
            {
                healthBarController.UpdateHealth(-10);
            }
        }
    }

    private void ShootProjectile()
    {
        Vector3 directionToPlayer = (PlayerPosition() - shootPoint.position).normalized;

        GameObject projectileObject = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);
        WizardProjectile projectile = projectileObject.GetComponent<WizardProjectile>();
        projectile.SetDirection(directionToPlayer); 
        projectile.speed = 10f;
    }

    private Vector3 PlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position;
    }

}
