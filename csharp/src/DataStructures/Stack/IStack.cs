namespace DataStructures.Stack
{
    public interface IStack<T>
    {
        // return the number of elements in the stack
        public int Size();

        // return if the stack is empty
        public bool IsEmpty();

        // push the element on the stack
        public void Push(T elem);

        // pop the element off the stack
        public T Pop();

        public T Peek();
    }
}