using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Eindopdracht.Models;
using Eindopdracht.Repositories;
using Xamarin.Forms;

namespace Eindopdracht.Views {
    public partial class QueryPage : ContentPage {
        public string Query { get; set; }
        public int PageNumber { get; set; }

        public QueryPage(string query) {
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
    }
}
