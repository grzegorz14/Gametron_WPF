using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Gametron.GamesCollection;

namespace Gametron
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICollectionView view;
        List<Game> games;
        ObservableCollection<Game> observableGames;
        private string searchText = "";

        #region sort properties of CollectionListView
        public SortDescription CurrentSort
        {
            get { return (SortDescription)GetValue(CurrentSortProperty); }
            set { SetValue(CurrentSortProperty, value); }
        }

        public static readonly DependencyProperty CurrentSortProperty =
            DependencyProperty.Register("CurrentSort", typeof(SortDescription), typeof(MainWindow), new PropertyMetadata(null));
        #endregion

        #region initializing
        public MainWindow()
        {
            InitializeComponent();

            InitializeGamesList();

            view = CollectionViewSource.GetDefaultView(CollectionListView.ItemsSource);
            //CurrentSort = new SortDescription("Name", ListSortDirection.Ascending);
            //view.SortDescriptions.Add(CurrentSort);
        }

        public void InitializeGamesList()
        {
            games = GamesList.ImportGamesFromBGGJson();
            observableGames = new(games.OrderBy(g => g.Rank).ThenBy(g => g.Name).ToList());
            //GamesView list
            MainListBox.ItemsSource = observableGames;
            //CollectionView list
            CollectionListView.ItemsSource = observableGames;
            GamesInCollectionTextBlock.Text = games.Count.ToString();
            GamesOwnedTextBlock.Text = games.FindAll(g => g.Owned == true).Count.ToString();
        }

        #endregion

        #region MainWindow events
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";

            GamesViewContentControl.Visibility = Visibility.Collapsed;
            CollectionViewContentControl.Visibility = Visibility.Collapsed;

            var view = (sender as RadioButton).Name switch
            {
                "GamesButton" => GamesViewContentControl,
                "CollectionButton" => CollectionViewContentControl,
                _ => GamesViewContentControl
            };

            view.Visibility = Visibility.Visible;
        }
        //private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        //{
        //    string filterText = (sender as TextBox).Text;
        //    if (string.IsNullOrEmpty(filterText)) return;
        //    foreach (GamesCollection.Game game in observableGames)
        //    {
        //        if (!game.Name.ToLower().Contains(filterText.ToLower()))
        //        {
        //            observableGames.Remove(game);
        //        }
        //    }
        //    CollectionListView.ItemsSource = updatedGamesList;
        //    view = CollectionViewSource.GetDefaultView(CollectionListView.ItemsSource);
        //    CurrentSort = new SortDescription("Name", ListSortDirection.Ascending);
        //    view.SortDescriptions.Add(CurrentSort);
        //    view.Refresh();
        //}
        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Back)
            //{
            //    searchText = searchText.Length > 0 ? searchText.Remove(searchText.Length - 1) : "";
            //}
            //else if (e.Key == Key.Space)
            //{
            //    searchText += " ";
            //}
            //else
            //{
            //    searchText += e.Key.ToString();
            //}
            //FilterGamesCollection(searchText);
            searchText = SearchTextBox.Text;
            if (searchText == "")
            {
                for (int i = 0; i < games.Count(); i++)
                {
                    if (!observableGames.Contains(games[i]))
                    {
                        observableGames.Add(games[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < observableGames.Count(); i++)
                {
                    if (!observableGames[i].Name.ToLower().Contains(searchText.ToLower()))
                    {
                        observableGames.Remove(observableGames[i--]);
                    }
                }
                for (int i = 0; i < games.Count(); i++)
                {
                    if (games[i].Name.ToLower().Contains(searchText.ToLower()))
                    {
                        if (!observableGames.Contains(games[i]))
                        {
                            observableGames.Add(games[i]);
                        }
                    }
                }
            }
        }

        private void FilterGamesCollection(string searchText)
        {
            if (searchText == "")
            {
                for (int i = 0; i < games.Count(); i++)
                {
                    if (!observableGames.Contains(games[i]))
                    {
                        observableGames.Add(games[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < observableGames.Count(); i++)
                {
                    if (!observableGames[i].Name.ToLower().Contains(searchText.ToLower()))
                    {
                        observableGames.Remove(observableGames[i--]);
                    }
                }
                for (int i = 0; i < games.Count(); i++)
                {
                    if (games[i].Name.ToLower().Contains(searchText.ToLower()))
                    {
                        if (!observableGames.Contains(games[i]))
                        {
                            observableGames.Add(games[i]);
                        }
                    }
                }
            }

        }
        #endregion

        #region GamesView events
        //coś tu jeszcze będzie
        #endregion

        #region CollectionView events
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader header = sender as GridViewColumnHeader;
            string newSort = (header.Column.DisplayMemberBinding as Binding)?.Path?.Path ?? header.Tag.ToString();
            CurrentSort = new SortDescription(newSort, CurrentSort.PropertyName == newSort ? (CurrentSort.Direction == ListSortDirection.Descending ? ListSortDirection.Ascending : ListSortDirection.Descending) : ListSortDirection.Ascending);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(CurrentSort);
            view.Refresh();
        }
        #endregion
    }
}
