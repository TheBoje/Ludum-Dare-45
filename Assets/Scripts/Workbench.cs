﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{

    public GameObject player;

    public List<GameObject> modules;

    [SerializeField] private float radiusDetect;

    private void Update()
    {
        Detect();
        EquipModule();
    }

    private void Detect()
    {
        Collider2D[] result = Physics2D.OverlapCircleAll(transform.position, radiusDetect);
        bool playerPresent = false;
        for (int i = 0; i < result.Length - 1; i++)
        {
            if (result[i].tag == "Player")
            {
                player = result[i].gameObject;
                playerPresent = true;
            }
            if (result[i].tag == "Module" && !result[i].GetComponent<Module>().equiped && !modules.Contains(result[i].gameObject))
            {
                modules.Add(result[i].gameObject);
            }

        }
        if (!playerPresent)
        {
            player = null;
            modules = new List<GameObject>();
        }
    }

    private void EquipModule()
    {
        if(modules.Count == 0 || player == null)
        {
            return;
        }
        Debug.Log("Equip?");
        Vector3 axis = player.GetComponent<RobotMovement>().input;
        //Arme
        if(modules[modules.Count - 1].GetComponent<Arme>())
        {
            if (axis.x > 0)
            {
                if (player.GetComponent<RobotModules>().arme1 != null)
                {
                    Destroy(player.GetComponent<RobotModules>().arme1.gameObject);
                }
                modules[modules.Count - 1].GetComponent<ItemFollow>().enabled = false;
                player.GetComponent<RobotModules>().arme1 = modules[modules.Count - 1];
                modules[modules.Count - 1].transform.parent = player.transform;
                modules[modules.Count - 1].GetComponent<Module>().equiped = true;
                player.GetComponent<RobotMovement>().followed = false;
                modules.RemoveAt(modules.Count - 1);
            }
            if (axis.x < 0)
            {
                if (player.GetComponent<RobotModules>().arme2 != null)
                {
                    Destroy(player.GetComponent<RobotModules>().arme2.gameObject);
                }
                modules[modules.Count - 1].GetComponent<ItemFollow>().enabled = false;
                player.GetComponent<RobotModules>().arme2 = modules[modules.Count - 1];
                modules[modules.Count - 1].transform.parent = player.transform;
                modules[modules.Count - 1].GetComponent<Module>().equiped = true;
                player.GetComponent<RobotMovement>().followed = false;
                modules.RemoveAt(modules.Count - 1);
            }
        }
        //Propulseur
        if (axis.y > 0 && modules[modules.Count - 1].GetComponent<Propulseur>())
        {
            if(player.GetComponent<RobotModules>().propulseur != null)
            {
                Destroy(player.GetComponent<RobotModules>().propulseur.gameObject);
            }
            modules[modules.Count - 1].GetComponent<ItemFollow>().enabled = false;
            player.GetComponent<RobotModules>().propulseur = modules[modules.Count - 1];
            modules[modules.Count - 1].transform.parent = player.transform;
            modules[modules.Count - 1].GetComponent<Module>().equiped = true;
            player.GetComponent<RobotMovement>().followed = false;
            modules.RemoveAt(modules.Count - 1);
        }
        //Protection
        if (axis.y < 0 && modules[modules.Count - 1].GetComponent<Protection>())
        {
            if (player.GetComponent<RobotModules>().protection != null)
            {
                Destroy(player.GetComponent<RobotModules>().protection.gameObject);
            }
            modules[modules.Count - 1].GetComponent<ItemFollow>().enabled = false;
            player.GetComponent<RobotModules>().protection = modules[modules.Count - 1];
            modules[modules.Count - 1].transform.parent = player.transform;
            modules[modules.Count - 1].GetComponent<Module>().equiped = true;
            player.GetComponent<RobotMovement>().followed = false;
            modules.RemoveAt(modules.Count - 1);
        }
    }

}
