using Cecs475.BoardGames.WpfView;
using System;
using System.Linq;
using System.Reflection;

namespace tets
{
    class Program
    {
        static void Main(string[] args)
        {
			Assembly current = Assembly.GetExecutingAssembly();

			Type iWpfGameFactory = typeof(IWpfGameFactory);
			Assembly tttAssembly = Assembly.LoadFrom("../../../../src/Cecs475.BoardGames.WpfApp/bin/Debug/games/Cecs475.BoardGames.Chess.Model.dll");

			var gameTypes = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(t => iWpfGameFactory.IsAssignableFrom(t));

			foreach (var games in gameTypes)
			{
				Console.WriteLine(games.FullName);
			}
		}
    }
}
