using System;


List<Card> deck = new List<Card>();
List<Card> playerHand = new List<Card>();
List<Card> dealerHand = new List<Card>();

int playerPoints = 0;
int dealerPoints = 0;
bool standing = false;
bool playAgain = true;


Console.OutputEncoding = System.Text.Encoding.UTF8;
while (playAgain)
{
    if (deck.Count < 18)
    {
        Console.Clear();
        deck.Clear();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 2; j <= 14; j++)
            {
                deck.Add(new Card(i, j));

            }
        }
        Console.Write("Shuffling the deck");
        Thread.Sleep(1000);
        Console.Write(".");
        Thread.Sleep(1000);
        Console.Write(".");
        Thread.Sleep(1000);
        Console.Write(".");
        Thread.Sleep(1000);

        Card.Shuffle(deck);
    }


    for (int i = 0; i < 2; i++)
    {
        Card cardPlayer = deck[Card.random.Next(deck.Count)];
        playerHand.Add(cardPlayer);
        deck.Remove(cardPlayer);

        Card cardDealer = deck[Card.random.Next(deck.Count)];
        dealerHand.Add(cardDealer);
        deck.Remove(cardDealer);
    }

    while (playerPoints < 21 && !standing)
    {
        Console.Clear();

        List<Card> dealerCardCovered = new List<Card>();

        dealerCardCovered.Add(dealerHand[0]);

        playerPoints = Card.GetPoints(playerHand);
        dealerPoints = Card.GetPoints(dealerCardCovered);

        Console.WriteLine("Dealer's hand:");

        Console.WriteLine(dealerHand[0].ToText() + ", ?");

        Console.WriteLine($"Dealer Points: {dealerPoints}");

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Player's hand:");

        foreach (Card card in playerHand)
        {
            Console.Write(card.ToText() + ", ");
        }

        Console.WriteLine();

        Console.WriteLine($"Player Points: {playerPoints}");
        Console.WriteLine();

        if (playerPoints <= 21)
        {
            Thread.Sleep(1500);
            Console.WriteLine("[H]it or [S]tand?");

            switch (Console.ReadLine())
            {
                case "Hit" or "hit" or "H" or "h":
                    Card cardPlayer = deck[Card.random.Next(deck.Count)];
                    playerHand.Add(cardPlayer);
                    deck.Remove(cardPlayer);

                    break;

                case "Stand" or "stand" or "S" or "s":
                    standing = true;
                    break;

                case "deck":                                                // tool to see the rest of the deck
                    for (int i = 0; i < deck.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {deck[i].ToText()}");
                    }
                    Console.ReadKey();
                    break;

                case "hit35":                                               // tool to control if the shuffling works
                    for (int i = 0; i < 35; i++)
                    {
                        cardPlayer = deck[Card.random.Next(deck.Count)];
                        playerHand.Add(cardPlayer);
                        deck.Remove(cardPlayer);
                    }
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("That was not an option...");
                    Thread.Sleep(1000);
                    Console.WriteLine("Please wait a moment...");
                    Thread.Sleep(3000);
                    break;
            }
        }
    }

    Console.Clear();

    dealerPoints = Card.GetPoints(dealerHand);

    while (dealerPoints < 17 && playerPoints < 22)
    {
        Card cardDealer = deck[Card.random.Next(deck.Count)];
        dealerHand.Add(cardDealer);
        deck.Remove(cardDealer);
        dealerPoints = Card.GetPoints(dealerHand);
    }

    if (playerPoints > 21)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Player BUSTED");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
    }
    else if (playerPoints == 21 && playerHand.Count == 2)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Player BLACKJACK");
        Console.ForegroundColor = ConsoleColor.White;
    }
    else if (dealerPoints > 21 && playerPoints < 22)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Player wins");
        Console.WriteLine();
        Console.WriteLine("Dealer BUSTED");
        Console.ForegroundColor = ConsoleColor.White;
    }
     else if (dealerPoints > playerPoints && dealerPoints < 22)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Dealer wins");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }
    else if (playerPoints > dealerPoints && playerPoints < 22)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Player wins");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }
    else if (dealerPoints == playerPoints)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Push");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

    }
    Console.WriteLine($"Dealer: {dealerPoints}");
    Console.WriteLine($"Player: {playerPoints}");
    Console.WriteLine();

    Console.Write("Dealer's hand: ");
    foreach (Card card in dealerHand)
    {
        Console.Write(card.ToText() + ", ");
    }
    Console.WriteLine();

    Console.Write("Player's hand: ");
    foreach (Card card in playerHand)
    {
        Console.Write(card.ToText() + ", ");
    }
    Console.WriteLine();
    Console.WriteLine();

    Console.WriteLine("Do you want to play again? ([Y]es or [N]o)");

    if (Console.ReadLine().ToUpper() == "Y")
    {
        playAgain = true;
        dealerHand.Clear();
        playerHand.Clear();
        playerPoints = 0;
        dealerPoints = 0;
        standing = false;
    }
    else
    {
        playAgain = false;
    }
}

Console.ReadKey();

public class Card
{
    public int Suit;
    public int Value;

    public static Random random = new Random();

    public Card (int suit, int value)
    {
        Suit = suit;
        Value = value;
    }

    public string GetSuitAsText()
    {
        switch (Suit)
        {
            case 0: return "♣";
            case 1: return "♠";
            case 2: return "♥";
            case 3: return "♦";
            default: return "Unknown";
        }
    }
    public string GetValueAsText()
    {
        if (Value >= 2 && Value <= 10)
            return Value.ToString();
        else if (Value == 11)
            return "J";
        else if (Value == 12)
            return "Q";
        else if (Value == 13)
            return "K";
        else if (Value == 14)
            return "A";
        else
            return "Unknown";
    }

    public string ToText()
    {
        return $"{GetSuitAsText()} {GetValueAsText()}";
    }
    public static void Shuffle(List<Card> deck)
    {
        
        for (int i = deck.Count - 1; i >= 0; i--)
        {
            int j = random.Next(0, i + 1);
            Card tempCard = deck[i];
            deck[i] = deck[j];
            deck[j] = tempCard;
        }
    }
    public static int GetPoints(List<Card> cards)
    {
        int points = 0;
        int aces = 0;

        foreach (Card card in cards)
        {
            if (card.Value >= 2 && card.Value <= 10)
            {
                points += card.Value;
            }
            else if (card.Value >= 11  && card.Value <= 13)
            {
                points += 10;
            }
            else if (card.Value == 14)
            {
                points += 11;
                aces++;
            }
        }

        while (points > 21 && aces > 0)
        {
            points -= 10;
            aces--;
        }

        return points;
    }

}
