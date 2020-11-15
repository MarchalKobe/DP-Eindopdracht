using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Eindopdracht.Views {
    public partial class FavoritePage : ContentPage {
        public FavoritePage() {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e) {
            Navigation.PushAsync(new DetailPage());
        }
    }
}
