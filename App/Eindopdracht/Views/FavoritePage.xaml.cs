using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eindopdracht.Models;
using Eindopdracht.Repositories;
using Xamarin.Forms;

namespace Eindopdracht.Views {
    public partial class FavoritePage : ContentPage {
        public FavoritePage() {
            InitializeComponent();
            FillList();
        }

        private async Task FillList() {
            List<Recipe> favorites = await RecipeRepository.getFavoritesAsync();
            lvwFavorites.ItemsSource = favorites;
        }

        void lvwFavorites_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            if (lvwFavorites.SelectedItem != null) {
                Recipe favoriteSelected = (Recipe)lvwFavorites.SelectedItem;
                Navigation.PushAsync(new DetailPage(favoriteSelected));
                lvwFavorites.SelectedItem = null;
            }
        }

        void lvwFavorites_Refreshing(System.Object sender, System.EventArgs e) {
            FillList();
            lvwFavorites.EndRefresh();
        }
    }
}
