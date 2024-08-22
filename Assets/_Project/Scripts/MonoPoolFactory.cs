using Pool;
using UnityEngine;
namespace Factory
{
    public abstract class MonoPoolFactory<TPrefab, T> : ScriptableObject
        where TPrefab : MonoBehaviour
        where T : class
    {
        [SerializeField] protected TPrefab Prefab;
        [SerializeField] private int _initialSpawnCount;

        protected IPooler<T> Pooler;

        private Transform _parent;
        public void Construct(Transform parent)
        {
            _parent = parent;
            OnConstructed();
        }
        public T Get()
        {
            return Pooler.GetPooled();
        }
        public void Free(T obj)
        {
            Pooler.Free(obj);
        }
        private T CreateFunc()
        {
            var obj = Instantiate(Prefab, _parent);
            return obj as T;
        }
        protected Pooler<T>.Builder GetPoolerBuilder()
        {
            return new Pooler<T>.Builder(CreateFunc)
                .WithInitialCount(_initialSpawnCount);
        }
        protected abstract void OnConstructed();
    }
}
