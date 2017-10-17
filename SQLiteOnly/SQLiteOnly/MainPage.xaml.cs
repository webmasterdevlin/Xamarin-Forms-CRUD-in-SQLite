using SQLite;
using SQLiteOnly.Annotations;
using SQLiteOnly.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SQLiteOnly
{
    public partial class MainPage : ContentPage
    {
        private readonly SQLiteAsyncConnection _databaseConnection;

        private ObservableCollection<Recipe> _observableRecipes;

        public MainPage()
        {
            InitializeComponent();

            _databaseConnection = DependencyService.Get<ISQLite>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            await _databaseConnection.CreateTableAsync<Recipe>();
            List<Recipe> recipes = await _databaseConnection.Table<Recipe>().ToListAsync();
            _observableRecipes = new ObservableCollection<Recipe>(recipes);
            MyListView.ItemsSource = _observableRecipes;
            base.OnAppearing();
        }

        private async void OnAdd(object sender, EventArgs e)
        {
            Recipe recipe = new Recipe { Name = $"Recipe {DateTime.UtcNow.Ticks}" };
            await _databaseConnection.InsertAsync(recipe);
            _observableRecipes.Insert(0, recipe);
        }

        private async void OnUpdate(object sender, EventArgs e)
        {
            Recipe recipe = _observableRecipes[0];
            recipe.Name += " (updated)";
            await _databaseConnection.UpdateAsync(recipe);
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            Recipe recipe = _observableRecipes[0];
            await _databaseConnection.DeleteAsync(recipe);
            _observableRecipes.Remove(recipe);
        }
    }

    public class Recipe : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _name;

        [MaxLength(255)]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}