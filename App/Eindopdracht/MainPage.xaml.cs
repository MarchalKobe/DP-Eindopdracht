using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eindopdracht.Models;
using Eindopdracht.Repositories;
using Xamarin.Forms;

namespace Eindopdracht
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            TestRepository();
        }

        private async Task TestRepository() {
            List<Recipe> recipes = await RecipeRepository.getRecipesAsync("curry", 2);
            foreach(Recipe recipe in recipes) {
                Debug.WriteLine(recipe.Title);
            }
        }
    }
}
