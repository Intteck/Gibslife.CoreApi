using System.Collections;

namespace Gibs.Domain
{
	[Serializable]
	public class ReadWriteList<T> : ICollection<T>, ICollection
	{
		private readonly List<T> _CurrentObjects;

        public ReadWriteList()
		{
			_CurrentObjects = [];
		}

		#region "Custom Members"

        public void AddRange(IEnumerable<T> items)
        {
            _CurrentObjects.AddRange(items);
        }

        public void ReplaceWith(IEnumerable<T> items)
        {
            _CurrentObjects.Clear();
            _CurrentObjects.AddRange(items);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            foreach (var item in _CurrentObjects)
            {
                _CurrentObjects.Remove(item);
            }
        }

        public T? Find(Predicate<T> predicate)
        {
            return _CurrentObjects.Find(predicate);
        }

		#endregion

		#region "ICollection<T> Members"

        public void Add(T item)
        {
            _CurrentObjects.Add(item);
        }

        public void Clear()
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
			get {
				return _CurrentObjects.Count;
			}
		}

        public bool IsReadOnly
        {
            get { return false; } 
		}

        public bool Remove(T item)
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
			get {
				throw new NotImplementedException();
			}
		}

		int ICollection.Count {
			get { return _CurrentObjects.Count; }
		}

		#endregion

	}
}
