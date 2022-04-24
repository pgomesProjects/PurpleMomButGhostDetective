using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SelectableObject : MonoBehaviour
{
    protected bool isHighlighted;

    public bool IsObjectSelected() { return isHighlighted; }
}
