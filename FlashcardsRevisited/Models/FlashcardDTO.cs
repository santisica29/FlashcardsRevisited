namespace FlashcardsRevisited.Models;
internal class FlashcardDTO
{
    //private static int LastId { get; set; } = 1;
    public int Id { get; set; } = 1;
    public string Front { get; set; }
    public string Back { get; set; }

    public FlashcardDTO()
    {
        Id = Id++;
    }

    public FlashcardDTO(string front, string back)
    {
        Id = Id++;
        Front = front;
        Back = back;
    }
}
