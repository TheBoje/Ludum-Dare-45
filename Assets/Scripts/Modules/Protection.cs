﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Création du type énuméré relatif au type de protection du bouclier
public enum TypeProtection { protect, reflect };

public class Protection : Module
{

    public TypeProtection type;
    public float defence;   


}
