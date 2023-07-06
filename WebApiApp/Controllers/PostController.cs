using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WebApiApp.Data;
using WebApiApp.Middleware;
using WebApiApp.Models;
using WebApiApp.Services;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly PostServices _postServices;


        public PostController(PostServices postServices)
        {
            _postServices = postServices;

        }

        [HttpGet]
        //[MiddlewareFilter(typeof(Middleware.Middleware))]
        public ActionResult GetAll()
        {
            var posts = _postServices.GetAll();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        //[MiddlewareFilter(typeof(Middleware.Middleware))] 

        public ActionResult GetById(int id)
        {
            var post = _postServices.GetById(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }
        [HttpPost]
        [Route("Create")]
        //[MiddlewareFilter(typeof(Middleware.Middleware))]

        public ActionResult Create(Post post)
        {
            _postServices.Create(post);
            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }


        [HttpPut]
        [Route("Update")]
        //[MiddlewareFilter(typeof(Middleware.Middleware))]
        public ActionResult Update(int id, Post updatedPost)
        {
            _postServices.Update(updatedPost, id )  ;
            return Ok();
        }

        [HttpDelete("{id}")]
        //[MiddlewareFilter(typeof(Middleware.Middleware))]

        public ActionResult Delete(int id)
        {
            _postServices.Delete(id);

            return NoContent();
        }
    }
}
