using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Policy;

namespace DS.ClassLib.DataStructure
{
    /// <summary>
    /// Represents a first-in, first-out collection of objects.
    /// </summary>
    /// <typeparam name="TItem">Specifies the type of elements in the queue.</typeparam>
    public class ObservableQueue<TItem> : Queue<TItem>, INotifyCollectionChanged, INotifyPropertyChanged
    {

        /// <summary>
        /// Initializes a new instance of the objects that is empty and has the default initial capacity.
        /// </summary>        
        public ObservableQueue() { }

        /// <summary>
        /// Initializes a new instance of the object that
        /// contains elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ObservableQueue(IEnumerable<TItem> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            foreach (TItem item in collection)
            {
                Enqueue(item);
            }
        }



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
