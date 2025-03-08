using Microsoft.AspNetCore.Mvc;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class GameController : Controller
    {
        private static GameModel _game = new GameModel();

        public IActionResult Index()
        {
            return View(_game);
        }

        public IActionResult MakeMove(int row, int col)
        {
            _game.MakeMove(row, col);
            return RedirectToAction("Index");
        }

        public IActionResult ResetBoard()
        {
            _game.ResetBoard();
            return RedirectToAction("Index");
        }

        public IActionResult RestartGame()
        {
            _game.RestartGame();
            return RedirectToAction("Index");
        }

        public IActionResult ToggleMode()
        {
            _game.IsAgainstAI = !_game.IsAgainstAI;
            _game.ResetBoard();
            return RedirectToAction("Index");
        }
    }
}
