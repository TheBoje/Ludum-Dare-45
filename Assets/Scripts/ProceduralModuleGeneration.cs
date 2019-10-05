﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralModuleGeneration : MonoBehaviour
{
    public GameObject prefabArme;
    public GameObject prefabProp;
    [Header("Valeur Pour les armes")]
    [SerializeField] private Vector2Int damageBounds;
    [SerializeField] private Vector2 porteeBounds;
    [SerializeField] private Vector2 vitessBounds;
    [SerializeField] private Vector2 cadanceBounds;
    [SerializeField] private List<GameObject> bullets;

    [Header("Valeur Pour les propulseur")]
    [SerializeField] private Vector2 speedBounds;
    [SerializeField] private Vector2 slerpBounds;

    [Header("Couleur de rareté")]
    [SerializeField] Color quarter;
    [SerializeField] Color half;
    [SerializeField] Color halfAndQuarter;
    [SerializeField] Color full;

    private Arme GenerateArme()
    {
        Arme arme = new Arme();
        //random
        arme.damage = (int)Random.Range(damageBounds[0], damageBounds[1]);
        arme.portee = Random.Range(porteeBounds[0], porteeBounds[1]);
        arme.vitesseProjectile = Random.Range(vitessBounds[0], vitessBounds[1]);
        arme.cadance = Random.Range(cadanceBounds[0], cadanceBounds[1]);

        arme.bullet = bullets[(int)Random.Range(0, bullets.Count - 1)];

        //definition de la rarete
        float averageValue = (arme.damage + arme.portee + arme.vitesseProjectile + arme.cadance) / 4;

        averageValue = averageValue * 100 / ((damageBounds[1] + porteeBounds[1] + vitessBounds[1] + cadanceBounds[1])/2);

        if(averageValue <= 25)
        {
            arme.rarete = quarter;
        }
        else if(averageValue <= 50)
        {
            arme.rarete = half;
        }else if(averageValue <= 75)
        {
            arme.rarete = halfAndQuarter;
        }
        else
        {
            arme.rarete = full;
        }

        return arme;
    }

    private Propulseur GeneratePropulseur()
    {
        Propulseur prop = new Propulseur();

        prop.speedMultiplicator = Random.Range(speedBounds[0], speedBounds[1]);
        prop.speedRotaMultiplicator = Random.Range(slerpBounds[0], slerpBounds[1]);

        float averageValue = (prop.speedMultiplicator + prop.speedRotaMultiplicator) / 2;
        averageValue = averageValue * 100 / ((speedBounds[1] + slerpBounds[1]) / 2);

        if (averageValue <= 25)
        {
            prop.rarete = quarter;
        }
        else if (averageValue <= 50)
        {
            prop.rarete = half;
        }
        else if (averageValue <= 75)
        {
            prop.rarete = halfAndQuarter;
        }
        else
        {
            prop.rarete = full;
        }
        return prop;
    }

    public void CreateModuleObject(Vector3 pos, string type)
    {
        if(type == "Arme")
        {
            Debug.Log("Generate Arme");
            GameObject mod = GameObject.Instantiate(prefabArme, pos, Quaternion.identity);
            Arme a = GenerateArme();
            mod.GetComponent<Arme>().damage = a.damage;
            mod.GetComponent<Arme>().portee = a.portee;
            mod.GetComponent<Arme>().vitesseProjectile = a.vitesseProjectile;
            mod.GetComponent<Arme>().cadance = a.cadance;
            mod.GetComponent<Arme>().rarete = a.rarete;
            mod.GetComponent<Arme>().bullet = a.bullet;
            mod.GetComponent<Arme>().SetColor();


        }
        else if(type == "Propulseur")
        {
            Debug.Log("Generate Propulseur");
            GameObject mod = GameObject.Instantiate(prefabProp, pos, Quaternion.identity);
            Propulseur p = GeneratePropulseur();
            mod.GetComponent<Propulseur>().speedMultiplicator = p.speedMultiplicator;
            mod.GetComponent<Propulseur>().speedRotaMultiplicator = p.speedRotaMultiplicator;
            mod.GetComponent<Propulseur>().rarete = p.rarete;
            mod.GetComponent<Propulseur>().SetColor();
        }
    }
}