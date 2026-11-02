using UnityEngine;

public class StateMachine
{
    public State currentState;

    public void ChangeState(State newState)
    {
        //Ejecutar función de salir del estado del estado actual
        if(currentState != null)
        {
            currentState.OnExit();
        }

        //Asignar el nuevo estado
        currentState = newState;

        //Llamar al OnEnter del nuevo estado actual
        currentState.OnEnter();
    }
    
    // Update is called once per frame
    public void Update()
    {
        currentState.OnUpdate();
    }
}
