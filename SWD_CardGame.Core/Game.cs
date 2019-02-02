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

    /// <summary>
    /// A Normal game where the highest score wins
    /// </summary>
    public class NormalGame : Game
    {
        public NormalGame(int cardsPrPlayer) : base(cardsPrPlayer)
        {
        }

        public override void AnnounceWinner()
        {
            if (!GameStarted)
                return;
            var winner = _players.MaxObject(player => player.HandValue);
            Console.Write($"The winner is: ");
            winner.ShowHand();
            GameStarted = false;
        }
    }

    /// <summary>
    /// A Low Score game where the lowest score wins
    /// </summary>
    public class LowScoreGame : Game
    {
        public LowScoreGame(int cardsPrPlayer) : base(cardsPrPlayer)
        {
        }

        public override void AnnounceWinner()
        {
            if (!GameStarted)
                return;
            var winner = _players.MinObject(player => player.HandValue);
            Console.Write($"The winner with a hand value of {winner.HandValue} is: ");
            winner.ShowHand();
            GameStarted = false;
        }
    }

    /// <summary>
    /// Abstract base class for games to inherit from
    /// </summary>
    public abstract class Game : IGame
    {
        protected readonly List<IPlayer> _players = new List<IPlayer>();
        protected Deck _cardDeck;
        protected int _numCardsPrPlayer;

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
        public bool GameStarted { get; protected set; } = false;

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
