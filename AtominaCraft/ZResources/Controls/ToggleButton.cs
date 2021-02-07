using System;

namespace AtominaCraft.ZResources.Controls
{
    /// <summary>
    /// A simple thing used for "toggling" for the use a game loop, which calls a callback function when toggled.
    /// <para>
    /// A toggle is called when you release the key, not when you press
    /// </para>
    /// </summary>
    public class ToggleButton
    {
        /// <summary>
        /// A state= 
        /// </summary>
        private bool CanToggleChange { get; set; }
        public bool IsToggled { get; set; }

        /// <summary>
        /// Called when the button toggles, passing a boolean that states if the button is toggled down or up
        /// </summary>
        public Action<bool> OnToggled { get; set; }

        public ToggleButton(bool isToggled = false) 
        {
            CanToggleChange = false;
            IsToggled = isToggled;
        }

        public ToggleButton(Action<bool> onToggled, bool isToggled = false)
        {
            CanToggleChange = false;
            IsToggled = isToggled;
            OnToggled = onToggled;
        }

        /// <summary>
        /// Call while the "button" is down
        /// </summary>
        public void ButtonDown()
        {
            if (!CanToggleChange)
            {
                CanToggleChange = true;
            }
        }

        /// <summary>
        /// Call while the "button" is up
        /// </summary>
        public void ButtonUp()
        {
            if (CanToggleChange)
            {
                Toggle();
                CanToggleChange = false;
            }
        }

        /// <summary>
        /// Toggles the button :)))
        /// </summary>
        public void Toggle()
        {
            IsToggled = !IsToggled;
            OnToggled?.Invoke(IsToggled);
        }
    }
}
