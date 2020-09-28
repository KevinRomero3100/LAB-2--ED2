using LAB_1___API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB_1___DataStructures;
using LAB_1___DataStructures.NoLinealStructures.Tree;
using System.Text.Json;

namespace LAB_1___API
{
    public class Movie
    {
        delegate List<Movie> ConvertValues(List<string> values);
        static ConvertValues ConvertNodetoT = new ConvertValues(ConvertToMovie);
        delegate string ConvertString(List<Movie> values, int lend);
        static ConvertString ConvertTtoNode = new ConvertString(ConvertToString);
        public string Id { get; set; }
        public string Director { get; set; }
        public double ImdbRating { get; set; }
        public string Genre { get; set; }
        public string ReleaseDate { get; set; }
        public int RottenTomatoesRating { get; set; }
        public string Title { get; set; }

        public static void IniciateTree(string path, int fieldlength, int grade)
        {           
            Storage.Instance.Fm = new FileManage<Movie>();
            Storage.Instance.Fm.Path = path;

            Storage.Instance.Fm.DeleteFile();
            Storage.Instance.Fm.UpdateGrade(grade);

            Storage.Instance.Fm.FieldLength = fieldlength;
            Storage.Instance.Fm.LineLength = 75 + ((grade) * fieldlength);
            Storage.Instance.Fm.ValueConverter = ConvertNodetoT;
            Storage.Instance.Fm.ValueDeconverter = ConvertTtoNode;

            Storage.Instance.BTree = new BTree<Movie>();
            Storage.Instance.BTree.Comparer = IdComparison;
            Storage.Instance.BTree.Fm = Storage.Instance.Fm;

            Storage.Instance.BTree.Grade = grade;
            Storage.Instance.BTree.Comparer = IdComparison;
            Storage.Instance.BTree.InitiateTree();
        }

        public static Comparison<Movie> IdComparison = delegate (Movie movie1, Movie movie2)
        {
            return movie1.Id.CompareTo(movie2.Id);
        };

        static List<Movie> ConvertToMovie(List<string> values)
        {
            List<Movie> values_list = new List<Movie>();
            JsonSerializerOptions name_rule = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true };
            for (int i = 0; i < values.Count; i++)
            {
                values_list.Add(JsonSerializer.Deserialize<Movie>(values[i], name_rule));
            }
            return values_list;
        }

        static string ConvertToString(List<Movie> values, int length)
        {
            string values_string = "";
            string length_s = "{0," + length.ToString() + "}";
            JsonSerializerOptions name_rule = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true };
            for (int i = 0; i < values.Count; i++)
            {
                values_string += $"{string.Format(length_s, JsonSerializer.Serialize<Movie>(values[i], name_rule))}";
            }
            return values_string;
        }
    }
}
