using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanChangeTarget
{
    public Collider ChangeTarget(Collider[] targets, bool isTargetMode);
}
