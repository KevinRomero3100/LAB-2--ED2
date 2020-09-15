using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB_1___API.Helpers
{
    public class Storage
    {
        private static Storage _instance = null;

        public static Storage Instance
        {
            get
            {
                if (_instance == null) _instance = new Storage();
                return _instance;
            }
        }
        public LAB_1___DataStructures.NoLinealStructures.Tree.MultipathTree<Movie> MoviesTree = new LAB_1___DataStructures.NoLinealStructures.Tree.MultipathTree<Movie>();
     
    }  
}
