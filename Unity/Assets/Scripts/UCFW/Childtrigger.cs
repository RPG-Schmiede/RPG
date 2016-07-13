using UnityEngine;
using System;

namespace UCFW
{
    public class Childtrigger : BetterBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        public event Action<Collider2D> TriggerEnter2D;
        public event Action<Collider2D> TriggerExit2D;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter2D.SafeCall(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExit2D.SafeCall(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter.SafeCall(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit.SafeCall(other);
        }
    }
}
