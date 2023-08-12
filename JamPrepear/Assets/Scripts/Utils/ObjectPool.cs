using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _objects = new();
        private readonly T _prefab;

        public ObjectPool(T prefab, int initialSize)
        {
            _prefab = prefab;

            for (var i = 0; i < initialSize; i++)
            {
                var newObject = UnityEngine.Object.Instantiate(prefab);
                newObject.gameObject.SetActive(false);
                _objects.Enqueue(newObject);
            }
        }

        public T Get()
        {
            var objectToReturn = AcquireObject();
            return objectToReturn;
        }

        public T Get(Action<T> initializationAction)
        {
            var objectToReturn = AcquireObject();

            initializationAction?.Invoke(objectToReturn);

            return objectToReturn;
        }

        public void Return(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            _objects.Enqueue(objectToReturn);
        }

        private T AcquireObject()
        {
            if (_objects.Count == 0)
            {
                var newObject = UnityEngine.Object.Instantiate(_prefab);
                newObject.gameObject.SetActive(false);
                _objects.Enqueue(newObject);
            }

            var objectToReturn = _objects.Dequeue();
            objectToReturn.gameObject.SetActive(true);

            return objectToReturn;
        }
    }
}