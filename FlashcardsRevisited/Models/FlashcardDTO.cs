namespace FlashcardsRevisited.Models;
internal class FlashcardDTO
{
    //private static int LastId { get; set; } = 1;
    public int FlashcardId { get; set; }
    public int DTOId { get; set; } = 1;
    public string Front { get; set; }
    public string Back { get; set; }

    public FlashcardDTO()
    {
        DTOId = DTOId++;
    }

    public FlashcardDTO(int flashcardId, string front, string back)
    {
        FlashcardId = flashcardId;
        DTOId = DTOId++;
        Front = front;
        Back = back;
    }
}
