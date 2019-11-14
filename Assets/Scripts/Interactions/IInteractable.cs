namespace Yumemonogatari.Interactions {
    /// <summary>
    /// Represents a character or an object that can be interacted with
    /// </summary>
    public interface IInteractable {
        
        /// <summary>
        /// Start the interaction script.
        /// </summary>
        void Interact();
    }
}