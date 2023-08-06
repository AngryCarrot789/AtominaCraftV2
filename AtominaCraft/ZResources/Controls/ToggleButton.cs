using System;

namespace AtominaCraft.ZResources.Controls {
    /// <summary>
    ///     A simple thing used for "toggling" for the use a game loop, which calls a callback function when toggled.
    ///     <para>
    ///         A toggle is called when you release the key, not when you press
    ///     </para>
    /// </summary>
    public class ToggleButton {
        /// <summary>
        ///     A state=
        /// </summary>
        private bool CanToggleChange;

        public bool IsToggled;

        /// <summary>
        ///     Called when the button toggles, passing a boolean that states if the button is toggled down or up
        /// </summary>
        public Action<bool> OnToggled;

        public ToggleButton(bool isToggled = false) {
            this.CanToggleChange = false;
            this.IsToggled = isToggled;
        }

        public ToggleButton(Action<bool> onToggled, bool isToggled = false) {
            this.CanToggleChange = false;
            this.IsToggled = isToggled;
            this.OnToggled = onToggled;
        }

        /// <summary>
        ///     Call while the "button" is down
        /// </summary>
        public void ButtonDown() {
            if (!this.CanToggleChange)
                this.CanToggleChange = true;
        }

        /// <summary>
        ///     Call while the "button" is up
        /// </summary>
        public void ButtonUp() {
            if (this.CanToggleChange) {
                Toggle();
                this.CanToggleChange = false;
            }
        }

        /// <summary>
        ///     Toggles the button :)))
        /// </summary>
        public void Toggle() {
            this.IsToggled = !this.IsToggled;
            this.OnToggled?.Invoke(this.IsToggled);
        }
    }
}