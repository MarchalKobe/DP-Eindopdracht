using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Eindopdracht.Models;
using Eindopdracht.Repositories;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eindopdracht.Views {
    public partial class DetailPage : ContentPage {
        public Recipe DetailRecipe { get; set; }

        public DetailPage(Recipe recipe) {
            if (Device.RuntimePlatform == Device.Android) {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            InitializeComponent();
            this.DetailRecipe = recipe;
            ShowDetails();
            BindingContext = this;
        }

        private async Task ShowDetails() {
            imgThumbnail.Source = this.DetailRecipe.ThumbnailUrl;
            lblTitle.Text = this.DetailRecipe.Title;
            lblIngredients.Text = "Ingredients: " + this.DetailRecipe.Ingredients;

            List<Recipe> favorite = await RecipeRepository.getFavoriteAsync(DetailRecipe.Title);
            foreach (Recipe fav in favorite) {
                if(fav.Title != "") {
                    btnFavorite.Source = "favorite.png";
                }
            }
        }

        public ICommand TapCommand => new Command<string>((url) => Navigation.PushAsync(new WebPage(this.DetailRecipe.RecipeUrl)));

        void ImageButton_Clicked(System.Object sender, System.EventArgs e) {
            AddOrDeleteFavorite();
        }

        private async Task AddOrDeleteFavorite() {
            if (btnFavorite.Source.ToString() == "File: favorite_border.png") {
                btnFavorite.Source = "favorite.png";
                RecipeRepository.addFavoriteAsync(DetailRecipe);
            } else {
                btnFavorite.Source = "favorite_border.png";
                RecipeRepository.deleteFavoriteAsync(DetailRecipe.Title);
            }
        }
    }
}
