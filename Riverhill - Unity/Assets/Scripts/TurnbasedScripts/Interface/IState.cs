public interface IState
{
    void Enter(); //enter state

    void Execute(); //execute content in state

    void Exit(); //exit state
}