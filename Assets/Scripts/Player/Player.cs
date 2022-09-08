using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State { Idle, Trace, Attack, Stun, Die }

    protected State curState;
}
