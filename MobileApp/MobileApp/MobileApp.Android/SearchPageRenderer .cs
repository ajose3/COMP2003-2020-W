using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MobileApp.Droid;
using MobileApp.Interfaces;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HomePage), typeof(SearchPageRenderer))]
namespace MobileApp.Droid
{
    class SearchPageRenderer : PageRenderer
    {
        public SearchPageRenderer(Context context) : base(context)
        {

        }

        //Add the Searchbar once Xamarin.Forms creates the Page
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is ISearchPage && e.NewElement is Page page && page.Parent is NavigationPage navigationPage && navigationPage.CurrentPage is ISearchPage)
                AddSearchToToolbar(page.Title);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (Element is ISearchPage && Element is Page page && page.Parent is NavigationPage navigationPage)
            {
                //Workaround to re-add the SearchView when navigating back to an ISearchPage, because Xamarin.Forms automatically removes it
                navigationPage.Popped += HandleNavigationPagePopped;
                navigationPage.PoppedToRoot += HandleNavigationPagePopped;
            }
        }

        //Adding the SearchBar in OnSizeChanged ensures the SearchBar is re-added after the device is rotated, because Xamarin.Forms automatically removes it
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            if (Element is ISearchPage && Element is Page page && page.Parent is NavigationPage navigationPage && navigationPage.CurrentPage is ISearchPage)
            {
                AddSearchToToolbar(page.Title);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (GetToolbar() is Toolbar toolBar)
                toolBar.Menu?.RemoveItem(Resource.Menu.MainMenu);

            base.Dispose(disposing);
        }

        //Workaround to re-add the SearchView when navigating back to an ISearchPage, because Xamarin.Forms automatically removes it
        void HandleNavigationPagePopped(object sender, NavigationEventArgs e)
        {
            if (sender is NavigationPage navigationPage
                && navigationPage.CurrentPage is ISearchPage)
            {
                AddSearchToToolbar(navigationPage.CurrentPage.Title);
            }
        }

        void AddSearchToToolbar(string pageTitle)
        {
            if (GetToolbar() is Toolbar toolBar
                && toolBar.Menu?.FindItem(Resource.Id.search_bar)?.ActionView?.JavaCast<SearchView>()?.GetType() != typeof(SearchView))
            {
                toolBar.Title = pageTitle;
                toolBar.InflateMenu(Resource.Menu.MainMenu);

                if (toolBar.Menu?.FindItem(Resource.Id.search_bar)?.ActionView?.JavaCast<SearchView>() is SearchView searchView)
                {
                    searchView.QueryTextChange += HandleQueryTextChange;
                    //searchView.ImeOptions = (int)ImeAction.Search;
                    //searchView.InputType = (int)InputTypes.TextVariationFilter;
                    //searchView.MaxWidth = int.MaxValue; //Set to full width - http://stackoverflow.com/questions/31456102/searchview-doesnt-expand-full-width
                    //searchView.Elevation = Resources?.GetDimension(Resource.Dimension.mtrl_toolbar_default_height) ?? 6;
                }
            }
        }

        void HandleQueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (Element is ISearchPage searchPage)
                searchPage.OnSearchBarTextChanged(e.NewText);
        }

        Toolbar? GetToolbar() => Xamarin.Essentials.Platform.CurrentActivity.FindViewById<Toolbar>(Resource.Id.toolbar);
    }
}
