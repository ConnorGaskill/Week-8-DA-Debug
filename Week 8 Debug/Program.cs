namespace Week_8_Debug
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

    }

    class BrokenHashTable
    {
        private List<int>[] buckets;

        public BrokenHashTable(int size)
        {
            buckets = new List<int>[size];
        }

        private int Hash(int key)
        {
            return key % buckets.Length;
        }

        public void Insert(int key)
        {
            int index = Hash(key);
            if (buckets[index] == null)
            {
                buckets[index] = new List<int>();
            }

            // 🚨 Problem: resets the bucket every time, erasing old values
            if (!buckets[index].Contains(key))
            {
                buckets[index] = new List<int>();
                buckets[index].Add(key);
            }
        }

        public bool Contains(int key)
        {
            int index = Hash(key);
            return buckets[index] != null && buckets[index].Contains(key);
        }
    }

}
