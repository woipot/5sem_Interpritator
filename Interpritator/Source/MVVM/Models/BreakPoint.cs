using Microsoft.Practices.Prism.Mvvm;

namespace Interpritator.Source.MVVM.Models
{
    internal class BreakPoint : BindableBase
    {
        public bool IsEnabled { get; set; } = false;

        public int Line { get; set; } = -1;

        public BreakPoint()
        {
        }
    }
}
