using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// T1 : ���� ������ Ÿ��
// T2 : ����Ÿ�Կ� �´� ����
public class StateMachine<T1, T2> where T2 : MonoBehaviour
{
    private T2 Owner;
    private State<T2> curState;
    private Dictionary<T1, State<T2>> states;

    public StateMachine(T2 Owner)
    {
        this.Owner = Owner;
        curState = null;
        states = new Dictionary<T1, State<T2>>();
    }

    public void Update()
    {
        if(curState != null)
        {
            curState.Update(Owner);
            curState.HandleStateChange(Owner);
        }
       

    }

    public void AddState(T1 type, State<T2> state)
    {
        states.Add(type, state);
    }

    public void ChangeState(T1 type)
    {
        if (curState != null)
        {
            curState.Exit(Owner);
            //Debug.Log("���� : " + curState + "����");
        }
        curState = states[type];
        curState.Enter(Owner);
        //Debug.Log("���� ���� : " + curState + "����");
    }
}

