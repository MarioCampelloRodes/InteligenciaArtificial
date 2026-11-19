using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;

    private List<StateTransition> transitions = new();

    private TransitionType currentTransition;

    public void ChangeState(State newState)
    {
        //Salir del estado en el que se encuentre
        if(currentState != null)
        {
            currentState.OnExit();
        }
        //Asignar el nuevo estado
        currentState = newState;
        //Llamar al OnEnter del nuevo estado actual
        currentState.OnEnter();
    }

    public void Update()
    {
        Debug.Log($"Current state: {currentState.name}");

        //comprueba las transiciones una a una
        currentTransition = currentState.OnUpdate();

        //si la transición no se cumple, no la intenta comprobar
        if (currentTransition == TransitionType.None)
        {
            return;
        }

        //comprueba si hay alguna transición que se haya cumplido
        TryTransition(currentTransition);
    }

    public void DrawGizmos()
    {
        currentState?.DrawGizmos();
    }

    //Añade una nueva transición indicando el estado del que parte, el estado al que va y el tipo de transición
    public void AddTransition(State from, State to, TransitionType type)
    {
        transitions.Add(new StateTransition(from, type, to));
    }

    private void TryTransition(TransitionType type)
    {
        foreach (StateTransition transition in transitions)
        {
            if (transition.From == currentState && transition.Type == type)
            {
                ChangeState(transition.To);
                return;
            }
        }
    }
}
