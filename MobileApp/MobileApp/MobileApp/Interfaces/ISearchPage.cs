using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Interfaces
{
    public interface ISearchPage
    {
        void OnSearchBarTextChanged(string text);
        event EventHandler<string> SearchBarTextChanged;
    }
}
