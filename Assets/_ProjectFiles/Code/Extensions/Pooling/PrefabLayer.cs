using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Extensions
{
    /// <summary>
    /// Хранит информацию о типе префаба и об объекте-носителе.
    /// Также предоставляет доступ к экземплярам и занимается их обработкой.
    /// </summary>
    [Serializable]
    public class PrefabLayer
    {
        public readonly LifetimeMonoBehaviour Prefab;
        public readonly Func<LifetimeMonoBehaviour,LifetimeMonoBehaviour> PrefabInstantiateFunc;

        public readonly GameObject LayerParent;
        public readonly GameObject InstancesParent;
        public readonly Queue<LifetimeMonoBehaviour> Instances;

        public PrefabLayer(LifetimeMonoBehaviour prefab, Func<LifetimeMonoBehaviour,LifetimeMonoBehaviour> prefabInstantiateFunc, GameObject layerParent, int count = 0)
        {
            Prefab = prefab;
            PrefabInstantiateFunc = prefabInstantiateFunc;
            LayerParent = layerParent;

            InstancesParent = new GameObject($"[Pool] {Prefab.GetType().Name}");
            Instances = new Queue<LifetimeMonoBehaviour>();

            InstancesParent.transform.SetParent(LayerParent.transform);
            // Создаем экземпляры.
            CreatePrefabInstances(Prefab, count);
        }
        
        /// <summary>
        /// Создание экземпляров в заданном количестве.
        /// </summary>
        private void CreatePrefabInstances(LifetimeMonoBehaviour prefab, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var inst = PrefabInstantiateFunc(prefab);
                inst.gameObject.SetActive(false);
                inst.transform.SetParent(InstancesParent.transform);
                Instances.Enqueue(inst);
            }
        }

        public LifetimeMonoBehaviour GetInstance()
        {
            if (Instances.Count == 0)
                CreatePrefabInstances(Prefab, 1);

            var inst = Instances.Dequeue();
            PrepareInstance(inst);

            return inst;
        }

        private void PrepareInstance(LifetimeMonoBehaviour instance)
        {
            instance.transform.SetParent(null);
            instance.gameObject.SetActive(true);
            
            if (instance.Format == TerminateFormat.Deactivate)
                instance.Lifetime.OnTermination(() => ReturnInstance(instance));
        }
        
        private void ReturnInstance(LifetimeMonoBehaviour instance)
        {
            instance.transform.SetParent(InstancesParent.transform);
            Instances.Enqueue(instance);
        }

        /// <summary>
        /// Проверяет на наличие типа.
        /// </summary>
        public bool Is<T>()
        {
            return Prefab is T;
        }

        public bool Is<T>(Func<T, bool> predicate)
        {
            if (Prefab is T t)
            {
                return predicate(t);
            }

            return false;
        }
    }
}