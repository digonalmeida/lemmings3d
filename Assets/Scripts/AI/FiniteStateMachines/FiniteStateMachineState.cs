namespace FiniteStateMachines
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class State<AgentType>
    {
        private AgentType agent;
        private FiniteStateMachine<AgentType> stateMachine = null;
        private List<Transition<AgentType>> transitions = new List<Transition<AgentType>>();
        private List<int> triggeredEvents = new List<int>();

        public AgentType Agent
        {
            get
            {
                return agent;
            }

            set
            {
                agent = value;
            }
        }

        public FiniteStateMachine<AgentType> StateMachine
        {
            get
            {
                return stateMachine;
            }

            set
            {
                stateMachine = value;
            }
        }

        public List<Transition<AgentType>> Transitions
        {
            get
            {
                return transitions;
            }

            set
            {
                transitions = value;
            }
        }

        public List<int> TriggeredEvents
        {
            get
            {
                return triggeredEvents;
            }

            set
            {
                triggeredEvents = value;
            }
        }

        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void TriggerEvent(int triggerEvent)
        {
        }

        public void AddTransition(Transition<AgentType> transition)
        {
            Transitions.Add(transition);
        }

        public void AddTransition(
            int trigger,
            Transition<AgentType>.Condition condition,
            State<AgentType> nextState)
        {
            var transition = new Transition<AgentType>(trigger, condition, nextState);
            AddTransition(transition);
        }

        public void AddCondition(
            Transition<AgentType>.Condition condition,
            State<AgentType> nextState)
        {
            AddTransition(Transition<AgentType>.NoTrigger, condition, nextState);
        }

        public void AddTrigger(int trigger, State<AgentType> nextState)
        {
            AddTransition(trigger, null, nextState);
        }

        public void CheckTransitions()
        {
            foreach (var transition in Transitions)
            {
                if (transition.Check(TriggeredEvents))
                {
                    StateMachine.SetState(transition.NextState);
                    return;
                }
            }
        }
    }
}