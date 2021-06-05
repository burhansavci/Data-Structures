namespace DataStructures.Queue
{
    public interface IQueue<T>
    {
        // Add an element to the back of the queue
        public void Enqueue(T elem);

        // Remove an element from the front of the queue
        public T Dequeue();
        
        // Peek the element at the front of the queue
        public T Peek();

        // Return the size of the queue
        public int Size();
        
        // Returns whether or not the queue is empty
        public bool IsEmpty();
    }
}