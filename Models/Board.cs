using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SudokuSolver.Models
{
    public class Board
    {

        public string Name { set; get; }
        public Cell[,] ListCells = new Cell[9, 9];
        public Dictionary<string, string> GroupsListDict = new Dictionary<string, string>();


        public Board()
        {
            GenerateBoard();
        }

        private int GetRandomNumber()
        {//TODO: Implement a dicitionary of tries (for each number 1 to 9 )
            //save it in Cell class
            //after each trial of number, mark as true.
            //save iteration time, and also will let me know when to use backtrack algorithm to change the previous  numbers 
            //which were generated.
            //other wise it will keep iterating infinite loop because of particial state of numbers on board can't be with each other , 
            //so there is a need to change the previous number to find the right state and keeping sudoku rules.


            Random rand = new Random();
            int number = rand.Next(1, 10);

            return number;
        }


        private bool IsExistOnColumn(int randNumber, int row)
        {
            if (ListCells[8, 8] != null)
            {
                Debug.WriteLine("stay here, watch if there is overflow of the array.");
            }
            for (int col = 0; ListCells[row, col] != null; col++)
            {
                if (randNumber == ListCells[row, col].Value)
                {
                    return true;
                }

            }

            return false;
        }


        private bool IsExistOnRow(int randNumber, int col)
        {
            for (int row = 0; ListCells[row, col] != null; row++)
            {
                if (randNumber == ListCells[row, col].Value)
                {
                    return true;
                }

            }

            return false;

        }


        public void GenerateBoard()
        {


            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    this.ListCells[i, j] = new Cell();
                    this.ListCells[i, j].Location = i + "," + j;
                   

                    if (i < 3 && j < 3)
                    {
                        this.ListCells[i, j].Group = "grp_a";
                        this.GroupsListDict["grp_a"] = this.ListCells[i, j].Location;
                    }

                    else if (i < 3 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_b";
                        this.GroupsListDict["grp_b"] = this.ListCells[i, j].Location;
                    }

                    else if (i < 3 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_c";
                        this.GroupsListDict["grp_c"] = this.ListCells[i, j].Location;
                    }

                    else if (i < 6 && j < 3)
                    {
                        this.ListCells[i, j].Group = "grp_d";
                        this.GroupsListDict["grp_d"] = this.ListCells[i, j].Location;
                    }


                    else if (i < 6 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_e";
                        this.GroupsListDict["grp_e"] = this.ListCells[i, j].Location;
                    }


                    else if (i < 6 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_f";
                        this.GroupsListDict["grp_f"] = this.ListCells[i, j].Location;
                    }


                    else if (i < 9 && j < 3)
                    {
                        this.ListCells[i, j].Group = "grp_g";
                        this.GroupsListDict["grp_g"] = this.ListCells[i, j].Location;
                    }


                    else if (i < 9 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_h";
                        this.GroupsListDict["grp_h"] = this.ListCells[i, j].Location;
                    }



                    else if (i < 9 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_i";
                        this.GroupsListDict["grp_i"] = this.ListCells[i, j].Location;
                    }






                }
            }



            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int RandNumber = GetRandomNumber();
                    bool IsExistonColumn = IsExistOnColumn(RandNumber, i);
                    bool IsExistonRow = IsExistOnRow(RandNumber, j);
                    bool IsExistonBox = false;//TOOD: add implemention of function to check if already exist in the close numbers box. need to use dictionary

                    while (IsExistonColumn || IsExistonRow || IsExistonBox)
                    {
                        RandNumber = GetRandomNumber();
                        IsExistonColumn = IsExistOnColumn(RandNumber, i);
                        IsExistonRow = IsExistOnRow(RandNumber, j);
                        IsExistonBox = false;
                    }

                    this.ListCells[i, j].Value = RandNumber;




                }
            }



        }


    }
}
