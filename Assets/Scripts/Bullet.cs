﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float portee;
    public float speed;
    public float damage;

    private Rigidbody2D rb;

    public GameObject owner;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        portee--;
        rb.velocity = transform.right * speed;
        Debug.DrawRay(transform.position, transform.right);
        if(portee <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform != transform.parent)
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.GetComponent<RobotHP>())
        {
            collision.gameObject.GetComponent<RobotHP>().Healt_edit(-damage / collision.gameObject.GetComponent<RobotHP>().resistance);
        }
    }
}
