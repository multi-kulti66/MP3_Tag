using MP3_Tag.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace MP3_Tag.Behavior
{
    public class SelectedItemsBehavior : Behavior<MultiSelector>
    {
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty
            .Register("SelectedItems", typeof(List<Mp3SongViewModel>), typeof(SelectedItemsBehavior),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public List<Mp3SongViewModel> SelectedItems
        {
            get { return (List<Mp3SongViewModel>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }


        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += AssociatedObjectSelectionChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= AssociatedObjectSelectionChanged;
        }

        void AssociatedObjectSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItems = AssociatedObject.SelectedItems.Cast<Mp3SongViewModel>().ToList();
        }

        
        
    }
}
