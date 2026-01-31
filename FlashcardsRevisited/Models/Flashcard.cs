namespace FlashcardsRevisited.Models;
internal class Flashcard
{
    public int Id { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
    public Stack Stack { get; set; }
}
