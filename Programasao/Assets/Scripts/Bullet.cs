using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rbBullet;
    public float speedBullet;

    // Start is called before the first frame update
    void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rbBullet.velocity = transform.right * speedBullet;

        Destroy(this.gameObject, 4);
    }

}
