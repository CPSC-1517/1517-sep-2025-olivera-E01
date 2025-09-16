using System.Reflection.Metadata.Ecma335;

namespace ObjectOrientedReview
{
    public class Employment
    {
        private string _Title;

        public string Title
        {
            get => _Title;           // same as get { return _Title; }
            set { _Title = value; }
        }
        
    }
}