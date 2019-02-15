﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public abstract void onSelect();
    public abstract void onDeselect();
    public abstract void update();
}