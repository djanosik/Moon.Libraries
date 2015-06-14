using System.Collections.Generic;

namespace Moon.Collections
{
    /// <summary>
    /// Provides a base class for implementing dictionaries that map a single key onto multiple items.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class MultiDictionary<TKey, TValue>
        : Dictionary<TKey, List<TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiDictionary{TKey,TValue}" /> class.
        /// </summary>
        public MultiDictionary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiDictionary{TKey,TValue}" /> class.
        /// </summary>
        /// <param name="comparer">The key comparer.</param>
        public MultiDictionary(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        /// <summary>
        /// Adds a new item to the collection associated with the given key.
        /// </summary>
        /// <param name="key">The key of the collection to add value to.</param>
        /// <param name="value">The value to add to the collection.</param>
        public void Add(TKey key, TValue value)
        {
            Requires.NotNull(key, nameof(key));

            List<TValue> list;
            if (!TryGetValue(key, out list))
            {
                this[key] = list = new List<TValue>();
            }
            list.Add(value);
        }

        /// <summary>
        /// Adds a series of values to the collection associated with the given key.
        /// </summary>
        /// <param name="key">The key of the collection to add values to.</param>
        /// <param name="values">The values to add to the collection.</param>
        public void AddRange(TKey key, IEnumerable<TValue> values)
        {
            Requires.NotNull(key, nameof(key));
            Requires.NotNull(values, nameof(values));

            List<TValue> list;
            if (!TryGetValue(key, out list))
            {
                this[key] = list = new List<TValue>();
            }
            list.AddRange(values);
        }

        /// <summary>
        /// Determines whether the collection associated with the given key contains the given item.
        /// </summary>
        /// <param name="key">The key of the collection.</param>
        /// <param name="value">The value to locate in the collection.</param>
        public bool Contains(TKey key, TValue value)
        {
            Requires.NotNull(key, nameof(key));

            List<TValue> list;
            return TryGetValue(key, out list) && list.Contains(value);
        }

        /// <summary>
        /// Attempts to remove an item from the collection associated with the given key. Returns
        /// true if the item was found and removed, and false if it was not found.
        /// </summary>
        /// <param name="key">The key of the collection.</param>
        /// <param name="value">The value to remove from the collection.</param>
        public bool Remove(TKey key, TValue value)
        {
            Requires.NotNull(key, nameof(key));

            List<TValue> list;
            return TryGetValue(key, out list) && list.Remove(value);
        }
    }
}