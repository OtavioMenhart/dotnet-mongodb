namespace MongoDb.Domain.Dto
{
    public class CreateOrUpdateBookDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string AuthorName { get; set; }
    }
}
