namespace FiniteStateMachines
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FiniteStateMachine<AgentType>
    {
        private State<AgentType> currentState;
        private List<int> triggeredEvents = new List<int>();

        public FiniteStateMachine(AgentType agent)
        {
            Agent = agent;
        }

        public AgentType Agent { get; set; }

        public void SetState(State<AgentType> state)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = state;

            if (currentState != null)
            {
                currentState.Agent = Agent;
                currentState.StateMachine = this;
                currentState.Enter();
            }
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.TriggeredEvents = triggeredEvents;
                currentState.CheckTransitions();
                triggeredEvents.Clear();

                if (currentState != null)
                {
                    currentState.Update();
                }
            }
        }

        public void TriggerEvent(int triggerEvent)
        {
            if (currentState != null)
            {
                triggeredEvents.Add(triggerEvent);
                currentState.TriggerEvent(triggerEvent);
            }
        }
    }
}