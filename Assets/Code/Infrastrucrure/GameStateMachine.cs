using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Code.Infrastrucrure
{
    public class GameStateMachine : IStateSwitcher
    {
        private readonly List<IState> _states;
        private IState _currentState;
        public GameStateMachine(FactoryHero factoryHero)
        {
            _states = new List<IState>
            {
                new BoostrapperState(this),
                new LoadLevelState(factoryHero)
            };
        }

        public void SwitchState<TState>() where TState : IState
        {
            _currentState?.Exit();

            _currentState = _states.FirstOrDefault(state => state is TState);
            _currentState.Enter();
        }
    }
}

