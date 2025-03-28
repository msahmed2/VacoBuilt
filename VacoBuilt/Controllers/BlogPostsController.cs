using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacoBuilt.Data;
using VacoBuilt.Dtos.Read;
using VacoBuilt.Dtos.Write;
using VacoBuilt.Entities;

namespace VacoBuilt.Controllers
{
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogPostsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("posts")]
        public async Task<ActionResult<IEnumerable<BlogPostResponseDto>>> GetBlogPosts()
        {
            var blogPosts = await _context.BlogPosts
            .Select(bp => new BlogPostResponseDto
            {
                Id = bp.Id,
                Title = bp.Title,
                Contents = bp.Contents,
                Timestamp = bp.Timestamp,
                CategoryId = bp.CategoryId
            })
            .ToListAsync();

            if (!blogPosts.Any())
            {
                return NotFound("No blog posts found.");
            }

            return Ok(blogPosts);
        }

        [HttpGet("posts/{id}")]
        public async Task<ActionResult<BlogPostResponseDto>> GetBlogPostById(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound($"No blog post found with ID {id}.");
            }

            var blogPostDto = new BlogPostResponseDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Contents = blogPost.Contents,
                Timestamp = blogPost.Timestamp,
                CategoryId = blogPost.CategoryId
            };

            return Ok(blogPostDto);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _context.Categories
             .Select(c => new CategoryResponseDto
             {
                 Id = c.Id,
                 Name = c.Name
             })
             .ToListAsync();

            if (!categories.Any())
            {
                return NotFound("No categories found.");
            }

            return Ok(categories);
        }


        [HttpPut("posts/{id}")]
        public async Task<ActionResult<IEnumerable<BlogPostResponseDto>>> PutBlogPost(int id, BlogPostRequestDTO blogPostDto)
        {
            if (blogPostDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var existingPost = await _context.BlogPosts.FindAsync(id);
            if (existingPost == null)
            {
                return NotFound($"No blog post found with ID {id}.");
            }

            existingPost.Title = blogPostDto.Title;
            existingPost.Contents = blogPostDto.Contents;
            existingPost.CategoryId = blogPostDto.CategoryId;
            existingPost.Timestamp = DateTime.Now; 

            await _context.SaveChangesAsync();

            var updatedPostDto = new BlogPostResponseDto
            {
                Id = existingPost.Id,
                Title = existingPost.Title,
                Contents = existingPost.Contents,
                Timestamp = existingPost.Timestamp,
                CategoryId = existingPost.CategoryId
            };

            return Ok(updatedPostDto);
        }

        [HttpPost("posts")]
        public async Task<ActionResult<BlogPostResponseDto>> PostBlogPost(BlogPostRequestDTO blogPostDto)
        {

            if (blogPostDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == blogPostDto.CategoryId);
            if (!categoryExists)
            {
                return BadRequest("Category is invalid");
            }

            var blogPost = new BlogPost
            {
                Title = blogPostDto.Title,
                Contents = blogPostDto.Contents,
                Timestamp = DateTime.Now,
                CategoryId = blogPostDto.CategoryId
            };

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            return new BlogPostResponseDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Contents = blogPost.Contents,
                Timestamp = blogPost.Timestamp,
                CategoryId = blogPost.CategoryId
            };
        }

        [HttpDelete("posts")]
        public async Task<IActionResult> DeleteAllBlogPost()
        {
            var allPosts = await _context.BlogPosts.ToListAsync();

            if (!allPosts.Any())
            {
                return NotFound("No blog posts found to delete.");
            }

            _context.BlogPosts.RemoveRange(allPosts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("posts/{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound($"No blog post found with ID {id}.");
            }

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}
