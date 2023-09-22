

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace alluberes.gamesoflife
{
    public class GameOfLifeGame
    {

        #region properties 

        // Grid of cells
        private Cell[][] _grid { get; set; }
        public int Width { get; set; } = 20;
        public int Height { get; set; } = 50;


        private int _iterations = 0;



        // Rules
        // 1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
        // 2. Any live cell with two or three live neighbours lives on to the next generation.
        // 3. Any live cell with more than three live neighbours dies, as if by overpopulation.
        // 4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.


        #endregion

        public GameOfLifeGame()
        {
            _grid = new Cell[Width][];
            for (int i = 0; i < Width; i++)
            {
                _grid[i] = new Cell[Height];
                for (int j = 0; j < Height; j++)
                {
                    _grid[i][j] = new Cell();
                }
            }


        }

        public void Init()
        {
            _iterations = 0;
            _grid = new Cell[Width][];
            for (int i = 0; i < Width; i++)
            {
                _grid[i] = new Cell[Height];
                for (int j = 0; j < Height; j++)
                {
                    var value = (new Random()).Next(0, 2) == 1;
                    _grid[i][j] = new Cell(value);
                }
            }

        }


        public void Run()
        {
            System.Console.WriteLine("Hello, World!");
            // DrawGrid();
            LoopAsk();
        }



        public void DrawGrid()
        {
            /// Draw the grid
            /// clean the console screen and draw the grid

            System.Console.Clear();
            System.Console.WriteLine("\n\nIterations: {0}", _iterations);
            for (int i = 0; i < Width; i++)
            {
                System.Console.WriteLine();
                for (int j = 0; j < Height; j++)
                {
                    //System.Console.Write(_grid[i][j].IsAlive ? "X" : "O");
                    _grid[i][j].Show();
                }
            }
            // sleep for 200 mili seconds
            System.Threading.Thread.Sleep(200);
        }



        public void LoopAsk()
        {
            var exit = false;
            var stop = true;
            while (!exit)
            {

                System.Console.WriteLine("\n\nIterations: {0}", _iterations);
                System.Console.WriteLine("\n\nPress any key to continue or 'q' to exit");
                Console.WriteLine("Press 'e' to reinitialize the grid");
                var key = 'n';
                if (stop) key = System.Console.ReadKey().KeyChar;
                switch (key)
                {
                    case 'q':
                        exit = true;
                        break;
                    case 'e':
                        Init();
                        DrawGrid();
                        break;

                    case 'n':
                        NextGeneration();
                        DrawGrid();
                        stop = false;
                        break;
                    default:
                        NextGeneration();
                        DrawGrid();
                        break;
                }
            }
        }

        /// <summary>
        /// Evaluate the rules and apply the changes to the grid
        /// </summary>
        /// <returns></returns>
        public void NextGeneration()
        {
            _iterations++;
            //Console.Clear();
            //Console.WriteLine("Next Generation");
            //Console.WriteLine("Width: {0}", Width);
            //Console.WriteLine("Height: {0}", Height);
            //Console.WriteLine("Grid: {0}", _grid.Length);
            //Console.WriteLine("Doing nothing....");

            /// sleep for 2 seconds
            // System.Threading.Thread.Sleep(200);

            DrawGrid();

            // Rules
            // 1. Any live cell with fewer than two live neighbours dies, as if by underpopulation.
            // 2. Any live cell with two or three live neighbours lives on to the next generation.
            // 3. Any live cell with more than three live neighbours dies, as if by overpopulation.
            // 4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var cell = _grid[i][j];
                    var aliveNeighbours = GetAliveNeighbours(i, j);
                    if (cell.IsAlive)
                    {
                        if (aliveNeighbours < 2 || aliveNeighbours > 3)
                        {
                            cell.IsAlive = false;
                        }
                    }
                    else
                    {
                        if (aliveNeighbours == 3)
                        {
                            cell.IsAlive = true;
                        }
                    }
                }
            }

        }

        public int GetAliveNeighbours(int x, int y)
        {
            var result = 0;

            for(int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= Width)
                {
                    continue;
                }
                for(int j = y - 1; j <= y + 1; j++)
                {
                    if (j < 0 || j >= Height)
                    {
                        continue;
                    }
                    if (i == x && j == y)
                    {
                        continue;
                    }
                    if (_grid[i][j].IsAlive)
                    {
                        result++;
                    }
                }
            }



            return result;
        }



    }



    public enum CellState
    {
        Alive,
        Dead,
        Used,
    }
    public class Cell
    {
        public bool IsAlive
        {
            get
            {
                return _state == CellState.Alive;
            }
            set
            {

                this._state = value ? CellState.Alive : CellState.Used;
            }
        }
        private CellState _state { get; set; }


        public Cell()
        {
            _state = CellState.Dead;
        }

        public Cell(bool isAlive)
        {
            _state = isAlive ? CellState.Alive : CellState.Dead;
        }


        public void Show()
        {
            switch (_state)
            {
                case CellState.Alive:
                    System.Console.Write("X");
                    break;
                case CellState.Dead:
                    System.Console.Write(" ");
                    break;
                case CellState.Used:
                    System.Console.Write(".");
                    break;
            }

            //System.Console.Write(IsAlive ? "X" : "O");
        }




    }

}