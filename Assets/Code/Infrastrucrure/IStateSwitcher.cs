namespace Assets.Code.Infrastrucrure
{
    public interface IStateSwitcher
    {
        public void SwitchState<TState>() where TState : IState;
    }
}

