using AuthorsWebAPI.Models;

namespace AuthorsWebAPI.Data
{
    public class DataContainer
    {
        public List<Authors> authors {get;} = new List<Authors>();
        public List<Books> books {get;} = new List<Books>();
    }
}
