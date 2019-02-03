using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD_CardGame.Core
{
    /// <summary>
    /// Interface for a Game
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// The amount of cards to be given each player
        /// </summary>
        int NumCardsPrPlayer { get; set; }
        /// <summary>
        /// Flag to see indicate if the game is started
        /// </summary>
        bool GameStarted { get; }
        /// <summary>
        /// The strategy to choose the winner from
        /// </summary>
        IWinnerBehaviour WinnerBehaviour { get; set; }
        /// <summary>
        /// Start a new game if the game isn't started and there is atleast to players
        /// </summary>
        void StartNewGame();
        /// <summary>
        /// Adds a player to the game
        /// </summary>
        /// <param name="pl">The player to be added</param>
        void AddPlayer(IPlayer pl);
        /// <summary>
        /// Prints the hand of each player in the game
        /// </summary>
        void PrintPlayerCards();
        /// <summary>
        /// Announce who the winner of the game is if the game isn't started. The game ends after the winner is announced.
        /// </summary>
        void AnnounceWinner();
    }

    public interface IWinnerBehaviour
    {
        /// <summary>
        /// Strategy for announcing a winner in a game
        /// </summary>
        /// <param name="players">The players playing the game</param>
        /// <param name="isGameStarted">Flag to see if the game has been started</param>
        void AnnounceWinner(IReadOnlyList<IPlayer> players,ref bool isGameStarted);
    }

    /// <summary>
    /// Normal Winner Behavior where the highest score wins
    /// </summary>
    public class NormalWinnerBehavior : IWinnerBehaviour
    {
        public void AnnounceWinner(IReadOnlyList<IPlayer> players,ref bool isGameStarted)
        {
            if (!isGameStarted)
                return;
            var winner = players.MaxObject(player => player.HandValue);
            Console.Write($"The winner is: ");
            winner.ShowHand();
            isGameStarted = false;
        }
        
    }

    /// <summary>
    /// Low Cost Winner Behavior where the lowest score of the game wins
    /// </summary>
    public class LowCostWinnerBehavior : IWinnerBehaviour
    {
        public void AnnounceWinner(IReadOnlyList<IPlayer> players, ref bool isGameStarted)
        {
            if (!isGameStarted)
                return;
            var winner = players.MinObject(player => player.HandValue);
            Console.Write($"The winner with a hand value of {winner.HandValue} is: ");
            winner.ShowHand();
            isGameStarted = false;
        }
    }

    /// <summary>
    /// A Normal game where the highest score wins
    /// </summary>
    public class NormalGame : Game
    {
        public NormalGame(int cardsPrPlayer) : base(cardsPrPlayer)
        {
            WinnerBehaviour = new NormalWinnerBehavior();
        }

        public override void AnnounceWinner()
        {
            WinnerBehaviour.AnnounceWinner(_players, ref _gameStarted);
        }
    }

    /// <summary>
    /// A Low Score game where the lowest score wins
    /// </summary>
    public class LowScoreGame : Game
    {
        public LowScoreGame(int cardsPrPlayer) : base(cardsPrPlayer)
        {
            WinnerBehaviour = new LowCostWinnerBehavior();
        }

        public override void AnnounceWinner()
        {
            WinnerBehaviour.AnnounceWinner(_players, ref _gameStarted);
        }
    }

    /// <summary>
    /// Abstract base class for games to inherit from
    /// </summary>
    public abstract class Game : IGame
    {
        protected List<IPlayer> _players = new List<IPlayer>();
        protected Deck _cardDeck;
        protected int _numCardsPrPlayer;
        protected bool _gameStarted;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="cardsPrPlayer">The amount of cards to give each player</param>
        protected Game(int cardsPrPlayer)
        {
            _numCardsPrPlayer = cardsPrPlayer;
        }

        /// <summary>
        /// The amount of cards to be given each player
        /// </summary>
        public int NumCardsPrPlayer
        {
            get => _numCardsPrPlayer;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                _numCardsPrPlayer = value > 0 ? value : 0;
            }
        }

        /// <summary>
        /// Flag to indicate if a game is started or not
        /// </summary>
        public bool GameStarted { get=>_gameStarted; protected set=> _gameStarted = value; }

        /// <summary>
        /// The strategy to choose the winner from
        /// </summary>
        public IWinnerBehaviour WinnerBehaviour { get; set; }

        /// <summary>
        /// Start a new game if the game isn't started and there is atleast to players
        /// </summary>
        public void StartNewGame()
        {
            if (GameStarted || _players.Count < 2)
                return;
            _cardDeck = new Deck(NumCardsPrPlayer * _players.Count);
            _players.ForEach(player => _cardDeck.DealCards(player,_numCardsPrPlayer));
            GameStarted = true;
        }

        /// <summary>
        /// Add a player to the game
        /// </summary>
        /// <param name="pl"></param>
        public void AddPlayer(IPlayer pl)
        {
            _players.Add(pl);
        }

        /// <summary>
        /// Function to print the Cards of each player
        /// </summary>
        public virtual void PrintPlayerCards()
        {
            _players.ForEach(player => player.ShowHand());
        }

        /// <summary>
        /// Calculates and announces who the winner is. The game ends after the winner is announced
        /// </summary>
        public abstract void AnnounceWinner();
    }
}
