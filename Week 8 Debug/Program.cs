using System;
using System.Collections.Generic;

namespace Week_8_Debug
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test case: multiple keys hash to the same bucket
            var table = new BrokenHashTable(5);

            table.Insert(12); // 12 % 5 = 2
            table.Insert(22); // 22 % 5 = 2
            table.Insert(37); // 37 % 5 = 2

            Console.WriteLine(table.Contains(12)); // True
            Console.WriteLine(table.Contains(22)); // True
            Console.WriteLine(table.Contains(37)); // True
            Console.WriteLine(table.Contains(2));  // False

            // Insert duplicate
            table.Insert(22);
            Console.WriteLine(table.Contains(22)); // True (still only one copy)
        }
    }

    /// <summary>
    /// A simple hash table that uses chaining with lists to handle collisions.
    /// </summary>
    class BrokenHashTable
    {
        private List<int>[] buckets;

        /// <summary>
        /// Initializes the hash table with a given number of buckets.
        /// </summary>
        /// <param name="size">Number of buckets</param>
        public BrokenHashTable(int size)
        {
            buckets = new List<int>[size];
        }

        /// <summary>
        /// Computes the hash index for a key.
        /// </summary>
        /// <param name="key">Key to hash</param>
        /// <returns>Index of the bucket</returns>
        private int Hash(int key)
        {
            return key % buckets.Length;
        }

        /// <summary>
        /// Inserts a key into the hash table.
        /// If multiple keys hash to the same bucket, they are stored together.
        /// </summary>
        /// <param name="key">Key to insert</param>
        public void Insert(int key)
        {
            int index = Hash(key);

            // Initialize bucket if empty
            if (buckets[index] == null)
            {
                buckets[index] = new List<int>();
            }

            // Original bug:
            // The original code was overwriting the bucket every time a new key was inserted:
            // if (!buckets[index].Contains(key)) {
            //     buckets[index] = new List<int>(); // erased previous values
            //     buckets[index].Add(key);
            // }
            //
            // This caused collisions to be lost; only the most recently inserted key remained.

            // Fix:
            // Only create a new list if the bucket is null.
            // Then add the key to the existing list if it is not already present.
            if (!buckets[index].Contains(key))
            {
                buckets[index].Add(key);
            }
        }

        /// <summary>
        /// Checks if a key exists in the hash table.
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if the key exists, false otherwise</returns>
        public bool Contains(int key)
        {
            int index = Hash(key);
            return buckets[index] != null && buckets[index].Contains(key);
        }
    }
}
