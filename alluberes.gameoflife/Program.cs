internal class Program
{
    private static void Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");
        var game = new alluberes.gamesoflife.GameOfLifeGame();
        game.Width = 20;
        game.Height = 50;
        game.Run();
    }
}