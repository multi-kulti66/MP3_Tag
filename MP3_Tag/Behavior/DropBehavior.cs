// ///////////////////////////////////
// File: DropBehavior.cs
// Last Change: 24.09.2016  20:12
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Behavior
{
    using System.Windows;
    using System.Windows.Input;



    /// <summary>
    ///     This is an Attached Behavior and is intended for use with
    ///     XAML objects to enable binding a drag and drop event to
    ///     an ICommand.
    /// </summary>
    public static class DropBehavior
    {
        #region  Static Fields and Constants

        /// <summary>
        ///     The Dependency property. To allow for Binding, a dependency
        ///     property must be used.
        /// </summary>
        private static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.RegisterAttached("DropCommand", typeof(ICommand), typeof(DropBehavior), new PropertyMetadata(DropCommandPropertyChangedCallBack));

        #endregion



        #region Methods

        /// <summary>
        ///     The setter. This sets the value of the DropCommandProperty
        ///     Dependency Property. It is expected that you use this only in XAML
        ///     This appears in XAML with the "Set" stripped off.
        ///     XAML usage:
        ///     <Grid mvvm:DropBehavior.DropCommand="{Binding DropCommand}" />
        /// </summary>
        /// <param name="inUIElement">
        ///     A UIElement object. In XAML this is automatically passed
        ///     in, so you don't have to enter anything in XAML.
        /// </param>
        /// <param name="inCommand">An object that implements ICommand.</param>
        public static void SetDropCommand(this UIElement inUIElement, ICommand inCommand)
        {
            inUIElement.SetValue(DropCommandProperty, inCommand);
        }

        /// <summary>
        ///     Gets the DropCommand assigned to the DropCommandProperty
        ///     DependencyProperty. As this is only needed by this class, it is private.
        /// </summary>
        /// <param name="inUIElement">A UIElement object.</param>
        /// <returns>An object that implements ICommand.</returns>
        private static ICommand GetDropCommand(UIElement inUIElement)
        {
            return (ICommand)inUIElement.GetValue(DropCommandProperty);
        }

        /// <summary>
        ///     The OnCommandChanged method. This event handles the initial binding and future
        ///     binding changes to the bound ICommand
        /// </summary>
        /// <param name="inDependencyObject">A DependencyObject</param>
        /// <param name="inEventArgs">A DependencyPropertyChangedEventArgs object.</param>
        private static void DropCommandPropertyChangedCallBack(DependencyObject inDependencyObject, DependencyPropertyChangedEventArgs inEventArgs)
        {
            UIElement uiElement = inDependencyObject as UIElement;

            if (uiElement == null)
            {
                return;
            }

            uiElement.Drop += (sender, args) =>
            {
                GetDropCommand(uiElement).Execute(args.Data);
                args.Handled = true;
            };
        }

        #endregion
    }
}