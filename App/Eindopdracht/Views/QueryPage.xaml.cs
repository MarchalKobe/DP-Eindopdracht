using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Eindopdracht.Models;
using Eindopdracht.Repositories;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eindopdracht.Views {
    public partial class QueryPage : ContentPage {
        public string Query { get; set; }
        public int PageNumber { get; set; }

        public QueryPage(string query) {
            if (Device.RuntimePlatform == Device.Android) {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            InitializeComponent();
            this.Query = query;
            this.PageNumber = 1;
            FillList();
        }

        private async Task FillList() {
            List<Recipe> recipes = await RecipeRepository.getRecipesAsync(this.Query, this.PageNumber);
            lvwRecipes.ItemsSource = recipes;
        }

        void Button_Previous(System.Object sender, System.EventArgs e) {
            if(this.PageNumber != 1) {
                this.PageNumber--;
                FillList();
                lblPageNumber.Text = Convert.ToString(this.PageNumber);
            }
        }

        void Button_Next(System.Object sender, System.EventArgs e) {
            this.PageNumber++;
            FillList();
            lblPageNumber.Text = Convert.ToString(this.PageNumber);
        }

        void lvwRecipes_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            if(lvwRecipes.SelectedItem != null) {
                Recipe recipeSelected = (Recipe)lvwRecipes.SelectedItem;
                Navigation.PushAsync(new DetailPage(recipeSelected));
                lvwRecipes.SelectedItem = null;
            }
        }
    }
}
