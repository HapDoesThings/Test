using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Frog : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    private bool tocarHierba;
    private int apples;
    private bool saltoCond;
    private int contVidas;

    private TextMeshProUGUI textApples;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    //VIDAS
    public Image[] vidas = new Image[3];

    //DISPARO
    public GameObject bullet;
    public Transform posDisparo;

    //SONIDO
    public AudioSource audioEfectos;
    public AudioClip recolectarAudio, danoAudio, saltoAudio;

    // Start is called before the first frame update
    void Start()
    {
        textApples = GameObject.Find("AppleCount").GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        contVidas = vidas.Length;
    }

    // Update is called once per frame
    /*void Update()
    {
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
    */

    
    private void FixedUpdate()
    {
        //MOVIMIENTO HORIZONTAL
        float movementX = Input.GetAxis("Horizontal"); //En vez de GetAxis, puedes usar GetAxisRaw para usar valores enteros entre -1, 0 y 1 en vez de los decimales entre ellos
        rb.velocity = new Vector2(movementX * speed, rb.velocity.y);

        if (movementX > 0.1)
        {
            transform.eulerAngles = new Vector2(0, 0);
            //sr.flipX = false;
            //transform.localScale = new Vector2 (1,1);
        }

        if (movementX < -0.1)
        {
            transform.eulerAngles = new Vector2(0,180);
            //sr.flipX = true;
            //transform.localScale = new Vector2 (-1,1);
        }
        
        //MOVIMIENTO DE SALTO
        if (saltoCond)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

            /*
            //REPRODUCE AUDIO
            audioEfectos.clip = saltoAudio;
            audioEfectos.Play();

            //REPRODUCE AUDIO DE OTRA FORMA
            audioEfectos.PlayOneShot(saltoAudio);
            */

            saltoCond = false;
        }

    }

    private void Update()
    {
        

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && tocarHierba)
        {
            saltoCond = true;
        }

        Debug.Log(apples);

        //ANIMACIONES
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Suelo", tocarHierba);

        //DISPARO
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bullet, posDisparo.position, posDisparo.rotation);
        }

    }

    //ON COLLISION CON EL SUELO PARA PODER SALTAR
    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Suelo") 
        {
            tocarHierba = true;
        }
    }

    //SALIR DE LA COLISION CON EL SUELO PARA NO SALTAR INFINITO
    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Suelo")
        {
            tocarHierba = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Apple")
        {
            apples++;
            textApples.text = apples.ToString();

            //REPRODUCE AUDIO
            audioEfectos.clip = recolectarAudio;
            audioEfectos.Play();

            //REPRODUCE AUDIO DE OTRA FORMA
            audioEfectos.PlayOneShot(recolectarAudio);

            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "Flag" && apples >= 3)
        {
            SceneManager.LoadScene(3);
        }

        if(coll.gameObject.tag == "Enemy")
        {
            if (contVidas > 1)
            {
                contVidas--;
                vidas[contVidas].enabled = false;

                //REPRODUCE AUDIO
                audioEfectos.clip = danoAudio;
                audioEfectos.Play();

                //REPRODUCE AUDIO DE OTRA FORMA
                audioEfectos.PlayOneShot(danoAudio);
                Debug.Log("Vidas actuales " + contVidas);
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }

        if(coll.gameObject.tag == "Vida")
        {
            if (contVidas < vidas.Length)
            {
                contVidas++;
                vidas[contVidas - 1].enabled = true;
                Debug.Log("Vidas actuales " + contVidas);
                Destroy(coll.gameObject);
            }
        }

    }
}
