public interface IState //interface IState mandates three methods --- Enter, Execute, and Exit
{
    void Enter(); //entering a state

    void Execute(); //executing the content in a state

    void Exit(); //exiting a state
}