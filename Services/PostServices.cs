using WebApiApp.Repositories;


namespace WebApiApp.Services
{
    public class PostServices : Repository<Post>
    {

        private readonly DataContext _context;

        public PostServices(DataContext context) : base(context)
        {
            _context = context;
        }
            //public object Check()
            //{
            //    var list = _context.GetAll().Select(d => new Object
            //    {@
            //    });
            //    return true;
            //}
            public IEnumerable<Post> GetAll()
        {
            return _context.Post.ToList();
        }   

        public Post GetById(int id)
        {
            return _context.Post.Find(id);
        }

        public void Create(Post post)
        {
            _context.Post.Add(post);
            _context.SaveChanges();
        }

        public void Update(Post updatedPost, int id)
        {
            var post = _context.Post.Find(id);
            if (post != null)
                {
                post.Title = updatedPost.Title;
                post.Description = updatedPost.Description;
                post.Body = updatedPost.Body;
                post.Thumbnail = updatedPost.Thumbnail;
                post.Categoryld = updatedPost.Categoryld;
                post.Appld = updatedPost.Appld;
                post.Author = updatedPost.Author;
                post.Status = updatedPost.Status;
                post.PublishedDate = updatedPost.PublishedDate;
                post.UpdatedDate = DateTime.Now;

            }
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = _context.Post.Find(id);
            if (post != null)
            {
                _context.Post.Remove(post);
                _context.SaveChanges();
            }
        }
    }
}
