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
     




    }
}
