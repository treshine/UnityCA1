
//This abstract class will make it easier to handle selectable actions.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public abstract void OnSelect();
    public abstract void OnDeselect();
    public abstract void OnUpdate();
}