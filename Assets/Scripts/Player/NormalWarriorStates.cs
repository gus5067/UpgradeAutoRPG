using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NormalWarriorStates
{
    public class BaseState : State<NormalWarrior>
    {
        public override void Enter(NormalWarrior Owner)
        {
        }

        public override void Update(NormalWarrior Owner)
        {
        }

        public override void Exit(NormalWarrior Owner)
        {

        }

        public override void HandleStateChange(NormalWarrior Owner)
        {

        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
      
        }

        public override void Update(NormalWarrior Owner)
        {
     

        }

        public override void Exit(NormalWarrior Owner)
        {

        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {

        }

        public override void Update(NormalWarrior Owner)
        {
          
        }

        public override void Exit(NormalWarrior Onwer)
        {

        }
    }

    public class AttackState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
      
        }

        public override void Update(NormalWarrior Owner)
        {

        }

        public override void Exit(NormalWarrior Owner)
        {
        }

        IEnumerator AttackTime(NormalWarrior Owner)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }

    public class RunAwayState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
    
        }

        public override void Update(NormalWarrior Owner)
        {
            

        }

        public override void Exit(NormalWarrior Owner)
        {

        }
    }

    public class HitState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
        }

        public override void Update(NormalWarrior Owner)
        {

        }

        public override void Exit(NormalWarrior Owner)
        {

        }
    }

    public class DieState : BaseState
    {
        public override void Enter(NormalWarrior Owner)
        {
        }

        public override void Update(NormalWarrior Owner)
        {

        }

        public override void Exit(NormalWarrior Owner)
        {

        }
    }
}
