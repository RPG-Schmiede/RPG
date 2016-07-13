using UnityEngine;
using System.Collections;

namespace UCFW
{
    public interface Updateable
    {
        /// <summary>
        /// Loop is called every frame when game is unpaused
        /// </summary>
        void Loop();

        /// <summary>
        /// OnUpdate is called every frame, even when game is paused
        /// </summary>
        void OnUpdate();
    }
}
