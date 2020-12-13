using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Eindopdracht.Views {
    public partial class WebPage : ContentPage {
        public WebPage(string url) {
            if (Device.RuntimePlatform == Device.Android) {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            InitializeComponent();
            webView.Source = url;
        }
    }
}
