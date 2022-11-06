using UnityEngine;

namespace Assets.Code.Infrastrucrure
{
    public class LoadLevelState : IState
    {
        private readonly FactoryHero _factoryHero;

        public LoadLevelState(FactoryHero factoryHero)
        {
            _factoryHero = factoryHero;
        }

        public void Enter()
        {
            CreateHero();
        }

        public void Exit()
        {
        }

        private void CreateHero()
        {
            Vector3 initialHeroSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;


            _factoryHero.BuildHero(initialHeroSpawnPoint);
        }
    }
}

