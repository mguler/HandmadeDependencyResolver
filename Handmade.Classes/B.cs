namespace Handmade.Classes
{
    public class B : A
    {
        private readonly C _c;
        public B(C c)
        {
            _c = c;
        }
        public string Name { get; set; }
    }
}
