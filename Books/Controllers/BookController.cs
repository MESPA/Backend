using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Context;
using Books.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext context;
        public BookController( BookContext context)
        {
            this.context = context;
        }
        // GET: api/<BookController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                
                return Ok (context.Book.ToList());
            }
            catch (Exception ex) 
            {

                return BadRequest(ex.Message);
            }
        }

        // GET api/<BookController>/5
        [HttpGet("{id}", Name ="GetBook") ]
        public ActionResult Get(int id)
        {
            try
            {
                var Bookid = context.Book.FirstOrDefault(b => b.IdBook == id);
                return Ok(Bookid);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }

        // POST api/<BookController>
        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            try
            {
                context.Book.Add(book);
                context.SaveChanges();
                return CreatedAtRoute("GetBook", new { id = book.IdBook }, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Book book)
        {
            try
            {
                if (book.IdBook == id)
                {
                    context.Entry(book).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetBook", new { id = book.IdBook }, book);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var bookdelete = context.Book.FirstOrDefault(d => d.IdBook == id);
                if (bookdelete != null)
                {
                    context.Book.Remove(bookdelete);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
