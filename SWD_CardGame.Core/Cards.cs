using System;

namespace SWD_CardGame.Core
{
    /// <summary>
    /// The different suits available to the cards
    /// </summary>
    public enum CardSuits
    {
        Red,
        Blue,
        Green,
        Yellow,
        Gold
    }

    /// <summary>
    /// Interface to define action on a Card
    /// </summary>
    public interface ICard
    {
        int Multiplier { get; }
        int CardNumber { get; }
        int Value { get; }
        CardSuits CardSuit { get; }
    }
    /// <summary>
    /// Base class for the different cards to inherit from
    /// </summary>
    public abstract class Card : ICard
    {
        /// <summary>
        /// The multiplier of the card
        /// </summary>
        public int Multiplier
        {
            get;
            protected set;
        }

        /// <summary>
        /// The Number of the card to be multiplied with the multiplier for the value
        /// </summary>
        public int CardNumber
        {
            get;
            protected set;
        }

        /// <summary>
        /// Default protected constructor. Generates a random <see cref="CardNumber"/> between 1 and 8
        /// </summary>
        protected Card()
        {
            CardNumber = RandomNumberGenerator.GetInstance.GenerateRandomNumber(1, 9);
        }

        /// <summary>
        /// The value of card given by the product of the <see cref="Multiplier"/> & <see cref="CardNumber"/>
        /// </summary>
        public int Value => Multiplier * CardNumber;

        /// <summary>
        /// The <see cref="CardSuits"/> of the card
        /// </summary>
        public CardSuits CardSuit { get; protected set; }

        /// <summary>
        /// Factory function to generate a random <see cref="ICard"/>
        /// </summary>
        /// <returns></returns>
        public static ICard CreateRandomCard()
        {
            int suit = RandomNumberGenerator.GetInstance.GenerateRandomNumber(0,Enum.GetNames(typeof(CardSuits)).Length);
            switch (suit)
            {
                case 0:
                    return new RedCard();
                case 1:
                    return new BlueCard();
                case 2:
                    return new GreenCard();
                case 3:
                    return new YellowCard();
                case 4:
                    return new GoldCard();
                default:
                    throw new Exception("Random Card Generation failed");
            }
        }
    }
    /// <summary>
    /// A Gold card with a multiplier of 5x
    /// </summary>
    public class GoldCard : Card
    {
        public GoldCard()
        {
            CardSuit = CardSuits.Gold;
            Multiplier = 5;
        }
    }

    /// <summary>
    /// A Red card with a multiplier of 1x
    /// </summary>
    public class RedCard : Card
    {
        public RedCard()
        {
            CardSuit = CardSuits.Red;
            Multiplier = 1;
        }
    }

    /// <summary>
    /// A Blue card with a multiplier of 2
    /// </summary>
    public class BlueCard : Card
    {
        public BlueCard()
        {
            CardSuit = CardSuits.Blue;
            Multiplier = 2;
        }
    }

    /// <summary>
    /// A Green guard with a multiplier of 3
    /// </summary>
    public class GreenCard : Card
    {
        public GreenCard()
        {
            CardSuit = CardSuits.Green;
            Multiplier = 3;
        }
    }

    /// <summary>
    /// A Yellow card with a multiplier of 4
    /// </summary>
    public class YellowCard : Card
    {
        public YellowCard()
        {
            CardSuit = CardSuits.Yellow;
            Multiplier = 4;
        }
    }
}
