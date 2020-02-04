namespace csharttest.Pages
{
    public class SelectionStep
    {
        public string Selector { get; private set; }
        public int? N { get; private set; }

        public SelectionStep(string selector, int? n = null)
        {
            Selector = selector;
            N = n;
        }
    }
}
