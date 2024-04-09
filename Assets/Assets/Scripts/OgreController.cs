using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool playerInsideArea = false;
    public HealthBarController healthBarController;

    private void Start()
    {
        initialPosition = new Vector3(0.86f, -4.45f, 0f);
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInsideArea = true;
            targetPosition = other.transform.position;
            Debug.Log("El player entro en el rango de vision");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag =="Player")
        {
            playerInsideArea = false;
            targetPosition = initialPosition;
            Debug.Log("El player salio del rango de vision");
        }
        if (other.tag == "Proyectil")
        {
            if (healthBarController != null)
            {
                healthBarController.UpdateHealth(-10); 
            }
        }
    }
}
