using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB_1___DataStructures;
using LAB_1___DataStructures.NoLinealStructures.Tree;

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
        public BTree<Movie> BTree = new BTree<Movie>();
        public FileManage<Movie> Fm = new FileManage<Movie>();
     
    }  
}
