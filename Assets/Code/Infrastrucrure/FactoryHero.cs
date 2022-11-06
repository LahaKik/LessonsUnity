using UnityEngine;

namespace Assets.Code.Infrastrucrure
{
    public class FactoryHero : IService
    {
        private readonly GameObject _heroPrefab;

        public FactoryHero(GameObject heroPrefab)
        {
            _heroPrefab = heroPrefab;
        }

        public GameObject BuildHero(Vector3 at)
            => GameObject.Instantiate(_heroPrefab, at, Quaternion.identity);
    }
}
