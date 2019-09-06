using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Coursework.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDMain : MasterDetailPage
    {
        public MDMain()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MDMenuItem;
            if (item == null)
                return;
            Page page;

            page = (Page)Activator.CreateInstance(item.TargetType);

            if (item.Id != 0) page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}