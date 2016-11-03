// ///////////////////////////////////
// File: RowMoveBehavior.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Behavior
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;



    public static class RowMoveBehavior
    {
        #region  Static Fields and Constants

        // EnableRowsMoveProperty is used to enable rows moving by mouse drag and move in data grid
        // the only requirement is to ItemsSource collection of datagrid be a ObservableCollection or at least IList collection
        public static readonly DependencyProperty EnableRowsMoveProperty =
            DependencyProperty.RegisterAttached("EnableRowsMove", typeof(bool), typeof(RowMoveBehavior), new PropertyMetadata(false, EnableRowsMoveChanged));

        // Private DraggedItemProperty attached property used only for EnableRowsMoveProperty
        private static readonly DependencyProperty DraggedItemProperty =
            DependencyProperty.RegisterAttached("DraggedItem", typeof(object), typeof(RowMoveBehavior), new PropertyMetadata(null));

        #endregion



        #region Methods

        public static bool GetEnableRowsMove(DataGrid obj)
        {
            return (bool)obj.GetValue(EnableRowsMoveProperty);
        }

        public static void SetEnableRowsMove(DataGrid obj, bool value)
        {
            obj.SetValue(EnableRowsMoveProperty, value);
        }

        /// <summary>Finds a parent of a given item on the visual tree.</summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="iChild">A direct or indirect child of the queried item.</param>
        /// <returns>The first parent item that matches the submitted type parameter. If not matching item can be found, a null reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject iChild) where T : DependencyObject
        {
            // Get parent item.
            DependencyObject parentObject = GetParentObject(iChild);

            // We've reached the end of the tree.
            if (parentObject == null)
            {
                return null;
            }

            // Check if the parent matches the type we're looking for.
            // Else use recursion to proceed with next level.
            T parent = parentObject as T;
            return parent ?? TryFindParent<T>(parentObject);
        }

        /// <summary>
        ///     This method is an alternative to WPF's <see cref="VisualTreeHelper.GetParent" /> method, which also
        ///     supports content elements. Keep in mind that for content element, this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="iChild">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject iChild)
        {
            if (iChild == null)
            {
                return null;
            }

            // Handle content elements separately.
            ContentElement contentElement = iChild as ContentElement;
            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);

                if (parent != null)
                {
                    return parent;
                }

                FrameworkContentElement frameworkContentElement = contentElement as FrameworkContentElement;

                if (frameworkContentElement != null)
                {
                    return frameworkContentElement.Parent;
                }

                return null;
            }

            // Also try searching for parent in framework elements (such as DockPanel, etc).
            FrameworkElement frameworkElement = iChild as FrameworkElement;

            if (frameworkElement != null)
            {
                DependencyObject parent = frameworkElement.Parent;

                if (parent != null)
                {
                    return parent;
                }
            }

            // If it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper.
            return VisualTreeHelper.GetParent(iChild);
        }

        /// <summary>Tries to locate a given item within the visual tree, starting with the dependency object at a given position.</summary>
        /// <typeparam name="T">The type of the element to be found on the visual tree of the element at the given location.</typeparam>
        /// <param name="iReference">The main element which is used to perform hit testing.</param>
        /// <param name="iPoint">The position to be evaluated on the origin.</param>
        public static T TryFindFromPoint<T>(this UIElement iReference, Point iPoint) where T : DependencyObject
        {
            DependencyObject element = iReference.InputHitTest(iPoint) as DependencyObject;

            if (element == null)
            {
                return null;
            }

            if (element is T)
            {
                return (T)element;
            }

            return TryFindParent<T>(element);
        }

        private static void EnableRowsMoveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as DataGrid;

            if (grid == null)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                grid.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
                grid.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
                grid.PreviewMouseMove += OnMouseMove;
            }
            else
            {
                grid.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
                grid.PreviewMouseLeftButtonUp -= OnMouseLeftButtonUp;
                grid.PreviewMouseMove -= OnMouseMove;
            }
        }

        private static object GetDraggedItem(DependencyObject obj)
        {
            return (object)obj.GetValue(DraggedItemProperty);
        }

        private static void SetDraggedItem(DependencyObject obj, object value)
        {
            obj.SetValue(DraggedItemProperty, value);
        }

        private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // find datagrid row by mouse point position
            var row = TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(sender as DataGrid));

            if ((row == null) || row.IsEditing)
            {
                return;
            }

            SetDraggedItem(sender as DataGrid, row.Item);
        }

        private static void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var draggeditem = GetDraggedItem(sender as DependencyObject);

            if (draggeditem == null)
            {
                return;
            }

            ExchangeItems(sender, (sender as DataGrid).SelectedItem);

            // select the dropped item
            (sender as DataGrid).SelectedItem = draggeditem;

            // reset
            SetDraggedItem(sender as DataGrid, null);
        }

        private static void OnMouseMove(object sender, MouseEventArgs e)
        {
            var draggeditem = GetDraggedItem(sender as DependencyObject);
            if (draggeditem == null)
            {
                return;
            }

            var row = TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(sender as DataGrid));
            if ((row == null) || row.IsEditing)
            {
                return;
            }

            ExchangeItems(sender, row.Item);
        }

        private static void ExchangeItems(object sender, object targetItem)
        {
            var draggeditem = GetDraggedItem(sender as DependencyObject);

            if (draggeditem == null)
            {
                return;
            }

            if ((targetItem != null) && !ReferenceEquals(draggeditem, targetItem))
            {
                var list = (sender as DataGrid).ItemsSource as IList;

                if (list == null)
                {
                    throw new ApplicationException("EnableRowsMoveProperty requires the ItemsSource property of DataGrid to be at least IList inherited collection. Use ObservableCollection to have movements reflected in UI.");
                }

                // get target index
                var targetIndex = list.IndexOf(targetItem);

                // remove the source from the list
                list.Remove(draggeditem);

                // move source at the target's location
                list.Insert(targetIndex, draggeditem);
            }
        }

        #endregion
    }
}