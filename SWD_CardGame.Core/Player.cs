using System;
using System.Collections.Generic;
using System.Linq;

namespace SWD_CardGame.Core
{
    /// <summary>
    /// Strategy for dealing cards
    /// </summary>
    public interface IDealBehavior
    {
        void DealCard(ICard card);
    }

    /// <summary>
    /// The interface for a player
    /// </summary>
    public interface IPlayer
    {
        string Name { get; set; }
        int HandValue { get; }
        void ShowHand();
        void DealCard(ICard card);

    }

    /// <summary>
    /// Normal Deal behavior with no limits to amount of cards
    /// </summary>
    public class NormalDeal : IDealBehavior
    {
        private readonly List<ICard> _cards;

        public NormalDeal(List<ICard> cards)
        {
            _cards = cards;
        }
        public void DealCard(ICard card)
        {
            _cards.Add(card);
        }
    }

    /// <summary>
    /// Weak Deal behavior where the limit on cards are 3.
    /// </summary>
    public class WeakDeal : IDealBehavior
    {
        private readonly List<ICard> _cards;

        public WeakDeal(List<ICard> cards)
        {
            _cards = cards;
        }
        public void DealCard(ICard card)
        {
            _cards.Add(card);
            if (_cards.Count <= 3)
                return;
            _cards.RemoveAt(0);
        }
    }

    /// <summary>
    /// Base class for a player
    /// </summary>
    public abstract class Player : IPlayer
    {
        /// <summary>
        /// The cards the player is currently holding
        /// </summary>
        protected List<ICard> _cardsInHand = new List<ICard>();

        /// <summary>
        /// The name of the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The combined <see cref="ICard.Value"/> of all cards the player is currently holding.
        /// </summary>
        public int HandValue => _cardsInHand.Sum(card => card.Value);
        
        /// <summary>
        /// Writes the hand of the player to the console
        /// </summary>
        public void ShowHand()
        {
            Console.WriteLine($"Player {Name} has following cards in hand");
            _cardsInHand.ForEach(item => Console.WriteLine($"{item.CardSuit.ToString()} : {item.Value} "));
        }
        /// <summary>
        /// The strategy of how to deal cards to the player
        /// </summary>
        public IDealBehavior DealBehavior { get; set; }

        /// <summary>
        /// Deal a card to the player
        /// </summary>
        /// <param name="card">The card to be dealt</param>
        public void DealCard(ICard card)
        {
            DealBehavior.DealCard(card);
        }
    }

    /// <summary>
    /// A player with normal deal behavior
    /// </summary>
    public class NormalPlayer : Player
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name of the player</param>
        public NormalPlayer(string name)
        {
            Name = name;
            DealBehavior = new NormalDeal(_cardsInHand);
        }
    }

    /// <summary>
    /// A player with weak deal behavior
    /// </summary>
    public class WeakPlayer: Player
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The name of the player</param>
        public WeakPlayer(string name)
        {
            Name = name;
            DealBehavior = new WeakDeal(_cardsInHand);
        }
    }

}
