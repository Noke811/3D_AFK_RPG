public interface IState
{
    public void OnEnter();
    public void OnRun();
    public void OnExit();
}

public class StateMachine
{
    protected IState curState;

    // 상태 변환
    public void ChangeState(IState state)
    {
        curState?.OnExit();
        curState = state;
        curState?.OnEnter();
    }

    // 현재 상태에서 매 프레임마다 업데이트
    public void Run()
    {
        curState?.OnRun();
    }
}
