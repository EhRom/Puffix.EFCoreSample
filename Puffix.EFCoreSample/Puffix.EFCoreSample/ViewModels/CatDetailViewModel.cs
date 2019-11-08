using System;

using Puffix.EFCoreSample.Models;

namespace Puffix.EFCoreSample.ViewModels
{
    public class CatDetailViewModel : BaseViewModel
    {
        public Cat Cat { get; set; }
        public CatDetailViewModel(Cat cat = null)
        {
            Title = cat?.Name;
            Cat = cat;
        }
    }
}
