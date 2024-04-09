using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : MonoBehaviour
{
    public float speed = 4f;

    private Rigidbody2D myBBD2;

    private void Start()
    {
        myBBD2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        myBBD2.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"|| collision.tag == "LIMITE")
        {
            Destroy(this.gameObject);
        }

    }

    public void SetDirection(Vector3 direction)
    {
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
