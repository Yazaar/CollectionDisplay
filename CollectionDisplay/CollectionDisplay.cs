using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CollectionDisplay
{
    public class CollectionDisplay<T> : INotifyPropertyChanged
    {

        private ObservableCollection<T> _items = new ObservableCollection<T>();
        private int _index = 0;

        private NotifyCollectionChangedEventHandler _collectionChangedHandler;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _valid = false;
        private T _item;

        /// <summary>
        /// True if Item/Index is bound to a valid object, false if not
        /// </summary>
        public bool Valid
        {
            get => _valid;
            private set
            {
                _valid = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The currently selected item
        /// </summary>
        public T Item
        {
            get => _item;
            private set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The currently selected index
        /// </summary>
        public int Index
        {
            get => _index;
            set
            {
                int itemsCount = Items.Count;
                if (itemsCount == 0) _index = 0;
                else if (value >= itemsCount) _index = itemsCount - 1;
                else if (value < 0) _index = 0;
                else _index = value;
                OnPropertyChanged();
                UpdateCurrent();
            }
        }

        /// <summary>
        /// The items that are currently in rotation
        /// </summary>
        public ObservableCollection<T> Items
        {
            get => _items;
            set
            {
                if (value == null) return;
                _items.CollectionChanged -= _collectionChangedHandler;
                _items = value;
                OnPropertyChanged();
                OnNewItems();
                value.CollectionChanged += _collectionChangedHandler;
            }
        }

        public CollectionDisplay()
        {
            _collectionChangedHandler = new NotifyCollectionChangedEventHandler(CollectionChangedEvent);
            Items.CollectionChanged += _collectionChangedHandler;
        }

        /// <summary>
        /// Shift the selected index a set amount of indexes
        /// </summary>
        /// <param name="indexes">The amount of indexes to shift, can be both a positive or negative integer</param>
        public void ShiftIndex(int indexes)
        {
            int itemsCount = Items.Count;
            if (itemsCount == 0) Index = 0;
            else
            {
                // Eliminates full circular turns (if itemsCount is 5 and indexes 11, 10 is simply the index moving two full circular turns and then index+1)
                // indexes % itemsCount

                // Shifting the current index to the new correct index
                // Index + (indexes % itemsCount)

                // Prevents the index from going out of bounds after the index shifted
                // (Index + (indexes % itemsCount)) % itemsCount
                int nextIndex = (Index + (indexes % itemsCount)) % itemsCount;

                // If shifting a negative amount of indexes, it may end up in the negatives, mirrors the current index to the end of the items
                if (nextIndex < 0) Index = nextIndex + itemsCount;
                else Index = nextIndex;
            }
        }

        /// <summary>
        /// New item added to the collection
        /// </summary>
        /// <param name="e">The args object from the actual event</param>
        private void CollectionItemAdded(NotifyCollectionChangedEventArgs e)
        {
            if (!Valid) Index = 0;
        }

        /// <summary>
        /// Item removed from the collection
        /// </summary>
        /// <param name="e">The args object from the actual event</param>
        private void CollectionItemRemoved(NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems?.Count != 0 && Index == e.OldStartingIndex)
            {
                if (Index == Items.Count) Index = 0;
                else UpdateCurrent();
            }
        }

        /// <summary>
        /// Item replaced in the collection
        /// </summary>
        /// <param name="e">The args object from the actual event</param>
        private void CollectionItemReplaced(NotifyCollectionChangedEventArgs e)
        {
            if (Index == e.OldStartingIndex) UpdateCurrent();
        }

        /// <summary>
        /// Item swapped index within the collection
        /// </summary>
        /// <param name="e">The args object from the actual event</param>
        private void CollectionItemMoved(NotifyCollectionChangedEventArgs e)
        {
            if (Index == e.OldStartingIndex) UpdateCurrent();
        }

        /// <summary>
        /// All items of the collection removed throgh .Clear()
        /// </summary>
        /// <param name="e">The args object from the actual event</param>
        private void CollectionItemsReset(NotifyCollectionChangedEventArgs e)
        {
            if (Index != 0) Index = 0;
            else UpdateCurrent();
        }

        /// <summary>
        /// Triggered whenever the collection has been replaced
        /// </summary>
        private void OnNewItems()
        {
            Index = 0;
            UpdateCurrent();
        }

        /// <summary>
        /// Triggered whenever the selected item has changed
        /// </summary>
        private void UpdateCurrent()
        {
            Valid = Index >= 0 && Index < Items.Count;
            if (Valid) Item = Items[Index];
            else Item = default;
        }

        /// <summary>
        /// Triggered whenever a property calls for a changed event
        /// </summary>
        /// <param name="name">The name of the property which called for a changed event</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// The actual method bound to triggered whenever the collection has been changed
        /// </summary>
        /// <param name="sender">The object (collection) which triggered the event</param>
        /// <param name="e">The arguments related to the triggered event</param>
        private void CollectionChangedEvent(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    CollectionItemAdded(e);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    CollectionItemRemoved(e);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    CollectionItemReplaced(e);
                    break;
                case NotifyCollectionChangedAction.Move:
                    CollectionItemMoved(e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    CollectionItemsReset(e);
                    break;
            }
        }
    }
}
