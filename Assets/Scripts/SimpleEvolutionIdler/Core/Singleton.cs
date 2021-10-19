using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleEvolutionIdler.Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetComponent<T>();
            }
            else
            {
                Debug.LogError($"Singleton [{typeof(T)}] is already exist! This controller was destroyed.");
                Destroy(this.gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
