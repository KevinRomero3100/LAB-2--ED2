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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LAB_1___API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            string text = "\t\t\t- LAB 1 -\n\nKevin Romero 1047519\nJosé De León 1072619\n\nPOST- /api/movies\n\t{\"order\" : 5}\n\nPOST- /api/movies/populate\n\tAdd test1.json in form-data with postman\n\nGET-    /api/movies/inorden\n\t/api/movies/preorden\n\t/api/movies/postorden";
            return text;
        }

        [HttpGet("{traversal}")]
        public IEnumerable<Movie> Get(string traversal)
        {
            try
            {
                if (Storage.Instance.MoviesTree.Count == 0) return null;
                if (traversal == "inorden")
                {
                    return Storage.Instance.MoviesTree.ToInOrden();
                }
                if (traversal == "preorden")
                {
                    return Storage.Instance.MoviesTree.ToPreOrden();
                }
                if (traversal == "postorden")
                {
                    return Storage.Instance.MoviesTree.ToPostOrden();
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
                if (grade < 2) return StatusCode(500);
                Movie.IniciateTree(grade);
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

                if (movies_list != null && Storage.Instance.MoviesTree.Grade != 0)
                {
                    for (int i = 0; i < movies_list.Count; i++)
                    {
                        Storage.Instance.MoviesTree.Insert(movies_list[i]);
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

    }
}
