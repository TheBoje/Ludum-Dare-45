﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Création du type énuméré relatif au type de protection du bouclier
public enum TypeProtection { protect, reflect };

public class Protection : Module
{

    public TypeProtection type;
    public float defence;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!equiped)
            return;
        if(this.type == TypeProtection.reflect && collision.transform.tag == "Bullet")
        {
            GameObject bul = GameObject.Instantiate(collision.gameObject, transform.position, transform.rotation);
            bul.GetComponent<Bullet>().portee = collision.transform.GetComponent<Bullet>().portee;
            bul.GetComponent<Bullet>().speed = collision.transform.GetComponent<Bullet>().speed * (-1);
            bul.GetComponent<Bullet>().damage = collision.transform.GetComponent<Bullet>().damage / 2;
        }
    }


}
