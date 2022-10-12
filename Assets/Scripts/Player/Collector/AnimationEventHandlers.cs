using UnityEngine;
using UnityEngine.Events;

namespace Player.Collector
{
    [RequireComponent(typeof(Animator))]
    public class AnimationEventHandlers : MonoBehaviour
    {
        public event UnityAction OnDig;

        public void RaiseOnDig()
        {
            OnDig?.Invoke();
        }
    }
}