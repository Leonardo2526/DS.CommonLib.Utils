using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DS.ClassLib.DataStructure
{
    /// <summary>
    /// Represents a first-in, first-out collection of objects.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of elements in the queue.</typeparam>
    public class ObservableQueue<TItem> : Queue<TItem>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// Adds an object to the end of the <see cref="Queue{T}"/>.
        /// </summary>
        /// <param name="item"></param>
        new public void Enqueue(TItem item)
        {
            base.Enqueue(item);
            OnPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, this.Count - 1);
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the <see cref="Queue{T}"/>.
        /// </summary>
        /// <returns>The object that is removed from the beginning of the <see cref="Queue{T}"/>.
        new public TItem Dequeue()
        {
            TItem removedItem = base.Dequeue();
            OnPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, removedItem, 0);
            return removedItem;
        }

        /// <summary>
        ///  Removes all objects from the <see cref="Queue{T}"/>.
        /// </summary>
        new public void Clear()
        {
            base.Clear();
            OnPropertyChanged();
            OnCollectionChangedReset();
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, TItem item, int index)
          => this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, item, index));

        private void OnCollectionChangedReset()
          => this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        private void OnPropertyChanged() => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Count)));
    }
}
