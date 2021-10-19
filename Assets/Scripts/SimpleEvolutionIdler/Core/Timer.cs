using System.Collections;

using UnityEngine;
using UnityEngine.Events;

namespace SimpleEvolutionIdler.Core
{
    public class Timer : MonoBehaviour
    {
        public float Delay = 0;
        public bool StartOnLoad = true;
        public bool Loop = true;
        public bool FirstRunUndelayed = false;

        public UnityEvent OnComplete;

        public bool IsRunning { get; private set; }
        public float CurrentDelay { get; private set; } = 0;

        // Use this for initialization
        protected virtual void Start()
        {
            IsRunning = StartOnLoad;
            if (!FirstRunUndelayed) CurrentDelay = Delay;
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsRunning) return;

            CurrentDelay -= Time.deltaTime;
            if (CurrentDelay <= 0)
            {
                OnComplete.Invoke();

                if (Loop) Reset();
                else Stop();
            }
        }

        public void Play()
        {
            IsRunning = true;
        }
        public void Pause()
        {
            IsRunning = false;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Reset()
        {
            CurrentDelay = Delay;
        }

        private void OnDestroy()
        {
            OnComplete.RemoveAllListeners();
        }
    }
}