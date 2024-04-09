using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilControler : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D myRBD2;

    void Awake()
    {
        myRBD2 = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        myRBD2.velocity = direction.normalized * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LIMITE")
        {
            Destroy(this.gameObject);
        }
    }

}
