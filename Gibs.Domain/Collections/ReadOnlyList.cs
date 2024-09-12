using System.Collections;

namespace Gibs.Domain
{
    [Serializable]
    public class ReadOnlyList<T> : ICollection<T>, ICollection
    {
        private readonly List<T> _CurrentObjects;

        public ReadOnlyList()
        {
            _CurrentObjects = [];
        }

        #region "Custom Members"

        public T? Find(Predicate<T> predicate)
        {
            return _CurrentObjects.Find(predicate);
        }

        #endregion

        #region "ICollection<T> Members"

        void ICollection<T>.Add(T item)
        {
            _CurrentObjects.Add(item);
        }

        void ICollection<T>.Clear()
        {
            _CurrentObjects.Clear();
        }

        public bool Contains(T item)
        {
            return _CurrentObjects.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _CurrentObjects.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return _CurrentObjects.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        bool ICollection<T>.Remove(T item)
        {
            return _CurrentObjects.Remove(item);
        }

        #endregion

        #region "IEnumerable<T> Members"

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _CurrentObjects.GetEnumerator();
        }

        #endregion

        #region "IEnumerable Members"

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _CurrentObjects.GetEnumerator();
        }

        #endregion

        #region "ICollection Members"

        void ICollection.CopyTo(Array array, int index)
        {
            _CurrentObjects.CopyTo(array.OfType<T>().ToArray(), index);
        }

        bool ICollection.IsSynchronized
        {
            get { return true; }
        }

        object ICollection.SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int ICollection.Count
        {
            get { return _CurrentObjects.Count; }
        }

        #endregion

    }
}
