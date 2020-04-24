using Cecs475.BoardGames.WpfView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cecs475.BoardGames.WpfApp {
	/// <summary>
	/// Interaction logic for GameChoiceWindow.xaml
	/// </summary>
	public partial class GameChoiceWindow : Window {
		public GameChoiceWindow() {
			InitializeComponent();

			Type iWpfGameFactory = typeof(IWpfGameFactory);
			Assembly tttAssembly = Assembly.LoadFrom("../../../../src/Cecs475.BoardGames.WpfApp/bin/Debug/games/Cecs475.BoardGames.Chess.Model.dll");
			tttAssembly = Assembly.LoadFrom("../../../../src/Cecs475.BoardGames.WpfApp/bin/Debug/games/Cecs475.BoardGames.Othello.Model.dll");

			var gameTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(t => iWpfGameFactory.IsAssignableFrom(t));

			foreach(var games in gameTypes)
			{
				var con = games.GetConstructor(Type.EmptyTypes);
				//how to invoke? 
				//var invoke = con.Invoke(new object[0]);
			}
			this.Resources.Add("GameTypes", gameTypes);



			/*foreach(var games in gameTypes) {
				var gameConstructors = games.GetConstructor(Type.EmptyTypes);
				var gameBoard = gameConstructors.Invoke(new object[0]);
				this.Resources.Add("GameTypes", gameBoard);

			}*/


		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			Button b = sender as Button;
			// Retrieve the game type bound to the button
			IWpfGameFactory gameType = b.DataContext as IWpfGameFactory;
			// Construct a GameWindow to play the game.
			var gameWindow = new GameWindow(gameType) {
				Title = gameType.GameName
			};
			// When the GameWindow closes, we want to show this window again.
			gameWindow.Closed += GameWindow_Closed;

			// Show the GameWindow, hide the Choice window.
			gameWindow.Show();
			this.Hide();
		}

		private void GameWindow_Closed(object sender, EventArgs e) {
			this.Show();
		}
	}
}
