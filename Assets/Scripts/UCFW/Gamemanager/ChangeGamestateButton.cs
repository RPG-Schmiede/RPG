using UnityEngine;
using UnityEngine.UI;

namespace UCFW
{
    /// <summary>
    /// A component which changes the gamestate on click
    /// </summary>
    public class ChangeGamestateButton : BetterBehaviour
    {
        [HideInInspector]
        public SerializableType gamestate = null;
        public Button button = null;

        private void Reset()
        {
            button = GetComponent<Button>();
        }

        protected override void OnAwake()
        {
            if(button != null || this.HasComponent(out button))
            {
                button.onClick.AddListener(ChangeGamestate);
            }

        }

        public void ChangeGamestate()
        {
            Gamemanager.instance.ChangeGamestate(gamestate);
        }
    }

}
