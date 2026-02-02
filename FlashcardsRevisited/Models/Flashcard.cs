namespace FlashcardsRevisited.Models;
internal class Flashcard
{
    public int FlashcardsId { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
    public StackDeck Stack { get; set; }
}
