using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Input;
using Eindopdracht.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eindopdracht.Views {
    public partial class DetailPage : ContentPage {
        public Recipe DetailRecipe { get; set; }

        public DetailPage(Recipe recipe) {
            InitializeComponent();
            this.DetailRecipe = recipe;
            ShowDetails();
            BindingContext = this;
        }

        private void ShowDetails() {
            imgThumbnail.Source = this.DetailRecipe.ThumbnailUrl;
            lblTitle.Text = this.DetailRecipe.Title;
            lblIngredients.Text = "Ingredients: " + this.DetailRecipe.Ingredients;
        }

        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(this.DetailRecipe.RecipeUrl));

        void ImageButton_Clicked(System.Object sender, System.EventArgs e) {
            AddFavorite();
        }

        private async Task AddFavorite() {
            
        }
    }
}
