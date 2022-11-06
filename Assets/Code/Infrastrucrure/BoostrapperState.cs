using UnityEngine;

namespace Assets.Code.Infrastrucrure
{
    public class BoostrapperState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        public BoostrapperState(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }
        public void Enter()
        {
            _stateSwitcher.SwitchState<LoadLevelState>();
        }


        public void Exit()
        {
        }

    }
}

