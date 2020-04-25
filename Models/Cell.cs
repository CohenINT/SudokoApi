using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuSolver.Models
{
    public class Cell
    {
        public int Value { set; get; }
        public string Group { set; get; }
        //This is the location using X,Y axis for example: "4,9"
        public string Location { set; get; }
        public Dictionary<int, bool> Tries = new Dictionary<int, bool>()
        {
            [0]=  true,
            [1] = false,
            [2] = false,
            [3] = false,
            [4] = false,
            [5] = false,
            [6] = false,
            [7] = false,
            [8] = false,
            


        };

        public Cell()
        {

        }




    }
}
