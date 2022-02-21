using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrowTask : MonoBehaviour
{
    public void GrowUp(){GrowMethod();}
    protected abstract void GrowMethod();
}
