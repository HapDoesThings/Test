using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float distancia;
    float posicionInicialX;
    public float velocidad;

    private float posicionActual;
    private float posicionAnterior;

    private SpriteRenderer enemySR;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicialX = transform.position.x;
        velocidad = 1;

        enemySR = GetComponent<SpriteRenderer>();
        enemySR.flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(posicionInicialX + Mathf.PingPong(velocidad * Time.time, distancia), transform.position.y);
        
        //COMPRUEBA LA DIRECCIÓN DEL ENEMIGO PARA CAMBIAR HACIA DONDE MIRA
        posicionActual = transform.position.x;

        if (posicionActual < posicionAnterior)
        {
            enemySR.flipX = true;
        }

        if (posicionActual > posicionAnterior)
        {
            enemySR.flipX = false;
        }

        posicionAnterior = transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
