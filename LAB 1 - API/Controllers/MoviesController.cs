using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB_1___API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LAB_1___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IWebHostEnvironment environment;
        public MoviesController(IWebHostEnvironment env)
        {
            environment = env;
        }

        [HttpGet]
        public string Get()
        {
            string text = "\t\t\t- LAB 2 -\n\nKevin Romero 1047519\nJosé De León 1072619\n\nPOST- /api/movies\n\t{\"order\" : 5}\n\nPOST- /api/movies/populate\n\tAdd test1.json or test2.json in form-data with postman\n\nGET-    /api/movies/inorden\n\t/api/movies/preorden\n\t/api/movies/postorden\n\nDELETE-    /api/movies/{id}\n\tAdd the id in Title-RealeseDate format to delete in the B Tree\n\nDELETE-    /api/movies\n\tThis request delete all content of the B Tree in disk";
            return text;
        }

        [HttpGet("{traversal}")]
        public IEnumerable<Movie> Get(string traversal)
        {
            try
            {
                if (traversal == "inorden")
                {
                    return Storage.Instance.BTree.ToInOrden();
                }
                if (traversal == "preorden")
                {
                    return Storage.Instance.BTree.ToPreOrden();
                }
                if (traversal == "postorden")
                {
                    return Storage.Instance.BTree.ToPostOrden();
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }         
        }

        [HttpPost]
        public ActionResult Post([FromBody] JsonElement jsonobj)
        {
            try
            {
                JsonElement jsonprop = jsonobj.GetProperty("order");
                int grade = jsonprop.GetInt32();
                if (grade < 3) return StatusCode(500);
                string path = environment.ContentRootPath + $"\\data.txt";

                //VARIABLES////////////////////////////////////
                Movie.IniciateTree(path, 220, grade);
                ///////////////////////////////////////////////
                
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("populate")]
        public ActionResult ReadJson([FromForm] IFormFile file)
        {
            try
            {
                List<Movie> movies_list;
                using (var reserved_memory = new MemoryStream())
                {
                    file.CopyToAsync(reserved_memory);
                    string json_text = Encoding.ASCII.GetString(reserved_memory.ToArray());

                    JsonSerializerOptions name_rule = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true };
                    movies_list = JsonSerializer.Deserialize<List<Movie>>(json_text, name_rule);
                }

                if (movies_list != null && Storage.Instance.BTree.Grade != 0)
                {
                    for (int i = 0; i < movies_list.Count; i++)
                    {
                        Movie current_movie = movies_list[i];
                        current_movie.Id = current_movie.Title + "-" + Convert.ToDateTime(current_movie.ReleaseDate).Year;

                        Storage.Instance.BTree.Insert(current_movie);
                    }
                    return Ok();
                }
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpDelete]
        public ActionResult DeleteTreeOnDisk()
        {
            try
            {
                Storage.Instance.Fm.Path = environment.ContentRootPath + $"\\data.txt";
                Storage.Instance.Fm.DeleteFile();
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("populate/{id}")]
        public ActionResult DeleteElement(string id)
        {
            try
            {
                //if (Storage.Instance.BTree.Exist(id))
                //{
                //    Storage.Instance.BTree.Delete(id);
                //    return Ok();
                //}
                //else
                //{
                    return NotFound();
                //}
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }



     }
}
