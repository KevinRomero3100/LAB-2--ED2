using LAB_1___API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_1___API
{
    public class Movie
    {
        public string Director { get; set; }
        public double ImdbRating { get; set; }
        public string Genre { get; set; }
        public string ReleaseDate { get; set; }
        public int RottenTomatoesRating { get; set; }
        public string Title { get; set; }

        public static void IniciateTree(int grade)
        {
            if (Storage.Instance.MoviesTree.Count == 0 || Storage.Instance.MoviesTree.Count != 0) Storage.Instance.MoviesTree = new LAB_1___DataStructures.NoLinealStructures.Tree.MultipathTree<Movie>();
            Storage.Instance.MoviesTree.Grade = grade;
            Storage.Instance.MoviesTree.Comparer = IdComparison;
        }

        public static Comparison<Movie> IdComparison = delegate (Movie movie1, Movie movie2)
        {
            return movie1.Title.CompareTo(movie2.Title);
        };
    }
}
