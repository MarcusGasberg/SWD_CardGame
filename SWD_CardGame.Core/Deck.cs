using System;
using System.Collections.Generic;
using System.Linq;

namespace SWD_CardGame.Core
{
    /// <summary>
    /// A Deck containing <see cref="ICard"/>
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// The container for the <see cref="ICard"/>s
        /// </summary>
        private readonly List<ICard> _cards;

        /// <summary>
        /// The number of cards in the deck
        /// </summary>
        public int NumCards { get; }

        /// <summary>
        /// Default constructor creates a random Deck
        /// </summary>
        /// <param name="numCards">The number of cards in the deck</param>
        public Deck(int numCards)
        {
            NumCards = numCards;
            _cards = new List<ICard>(numCards);
            ShuffleCards();
        }

        /// <summary>
        /// Deals an amount of cards to a player
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="amount"></param>
        public void DealCards(IPlayer pl, int amount)
        {
            if (amount > _cards.Count)
                return;
            for (int i = 0; i < amount; i++)
            {
                pl.DealCard(_cards.First());
                _cards.RemoveAt(0);
            }
        }

        /// <summary>
        /// Shuffles the card in the deck
        /// </summary>
        public void ShuffleCards()
        {
            _cards.Clear();
            for (int i = 0; i < NumCards; i++)
            {
                _cards.Add(Card.CreateRandomCard());
            }
        }

    }
}
