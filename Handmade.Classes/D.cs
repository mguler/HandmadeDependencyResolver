namespace Handmade.Classes
{
    public class D : C
    {
        private readonly E _e;
        public D(E e)
        {
            _e = e;
        }
        public int Age { get; set; }
    }
}
