namespace AuthorsWebAPI.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishedYear { get; set; }
        public int AuthorId {get; set;}
        public Authors Author { get; set;}  
    }
}
