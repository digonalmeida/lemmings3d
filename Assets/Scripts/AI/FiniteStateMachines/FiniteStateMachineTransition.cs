namespace FiniteStateMachines
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Transition<AgentType>
    {
        public static readonly int NoTrigger = -1;

        private int trigger = -1;
        private Condition condition = null;
        private State<AgentType> nextState;
        
        public Transition(
            int trigger,
            Condition condition,
            State<AgentType> nextState)
        {
            this.trigger = trigger;
            this.condition = condition;
            this.nextState = nextState;
        }
        
        public delegate bool Condition();

        public State<AgentType> NextState
        {
            get
            {
                return nextState;
            }

            set
            {
                nextState = value;
            }
        }

        public bool Check(List<int> triggers)
        {
            if (triggers.Contains(trigger) || trigger == NoTrigger)
            {
                if (condition != null)
                {
                    if (condition())
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}