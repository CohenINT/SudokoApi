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
        Dictionary<string, List<int>> GroupsListDict = new Dictionary<string, List<int>>();

        //Temp array, just to make it eaiaser to do backtrack algorithm in case of failed validations after trying all numbers 1-9.
        List<Cell> grid = new List<Cell>();//81

        public string GeneratedTime { set; get; }

        public Board()
        {

            GenerateBoard();
        }

        private int GetRandomNumber()
        {
            Random rand = new Random();
            int number = rand.Next(1, 10);
            return number;
        }

        private int GetRandomNumber(int row, int col)
        {//TODO: Implement a dicitionary of tries (for each number 1 to 9 )
         //save it in Cell class
         //after each trial of number, mark as true.
         //save iteration time, and also will let me know when to use backtrack algorithm to change the previous  numbers 
         //which were generated.
         //other wise it will keep iterating infinite loop because of particial state of numbers on board can't be with each other , 
         //so there is a need to change the previous number to find the right state and keeping sudoku rules.


         

            foreach (var trial in this.ListCells[row, col].Tries)
            {
                if (trial.Value == false)
                {
                    this.ListCells[row, col].Tries.Remove(trial.Key);  //To mark as used.
                    this.ListCells[row, col].Tries.Add(trial.Key, true);
                    return trial.Key;
                }
            }
            //if it returns -1, then all tries are being used. now need to change previous number.
            return -1;

        }

        //Checks if tries all numbers from 1-9, if so it would be need to to change the previous number on the gridlist cells.
        private bool IsAllTriesUsed(int row, int col)
        {
            foreach (var trial in this.ListCells[row, col].Tries)
            {
                if (trial.Value == false)
                {
                    return false;
                }
            }

            return true;
        }


        //Validations
        private bool IsExistOnColumn(int randNumber, int col)
        {
            for (int row = 0; row < 9; row++)
            {
                if (ListCells[row, col].Value == randNumber)
                {
                    return true;
                }
            }

            return false;

        }
        private bool IsExistOnRow(int randNumber, int row)
        {
            for (int col = 0; col < 9; col++)
            {
                if (ListCells[row, col].Value == randNumber)
                {
                    return true;
                }
            }


            return false;
        }
        private bool IsExistOnBox(int randNumber, string group)
        {

            var itemCell = GroupsListDict[$"{group}"].Find(x => x == randNumber);

            if (itemCell == 0)
            {//not found the same value
                return false;
            }

            return true;

        }



        private int TryGenereateNumber(int row, int col, string group, int colReducer)
        {
            bool IsExistonColumn = true;
            bool IsExistonRow = true;
            bool IsExistonBox = true;
            int  RandNumber = 0;

            RandNumber = GetRandomNumber(row, col);
            if (RandNumber == -1)
            {
                //TODO: FIND OTHER WAY TO INCREASE REDUCUER FOR COL, AND ALSO ROW IN CASE OF BACKTRACK.
                return TryGenereateNumber(row, col - colReducer, group, colReducer++);
            }
            //Validations
            IsExistonColumn = IsExistOnColumn(RandNumber, col);
            IsExistonRow = IsExistOnRow(RandNumber, row);
            IsExistonBox = IsExistOnBox(RandNumber, group);

            if (IsAllTriesUsed(row, col) == true && (IsExistonColumn || IsExistonRow || IsExistonBox))
            {
                return TryGenereateNumber(row, col - colReducer, group, colReducer++);
            }
            else if (IsExistonColumn || IsExistonRow || IsExistonBox)
            {
                return TryGenereateNumber(row, col, group, 1);
            }

            return RandNumber;


        }

        private void InitilizedBoardNumbers()
        {
            int newNumber = 0;
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    newNumber = TryGenereateNumber(row, col, ListCells[row, col].Group,1);
                    ListCells[row, col].Value = newNumber;
                    ListCells[row, col].Location = $"{row},{col}";
                    
                }
            }

        }




        public void GenerateBoard()
        {

            //Initlize dictionary for sorting numbers locations to groups (boxes)
            GroupsListDict.Add("grp_a", new List<int>());
            GroupsListDict.Add("grp_b", new List<int>());
            GroupsListDict.Add("grp_c", new List<int>());
            GroupsListDict.Add("grp_d", new List<int>());
            GroupsListDict.Add("grp_e", new List<int>());
            GroupsListDict.Add("grp_f", new List<int>());
            GroupsListDict.Add("grp_g", new List<int>());
            GroupsListDict.Add("grp_h", new List<int>());
            GroupsListDict.Add("grp_i", new List<int>());

            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    this.ListCells[i, j] = new Cell();
                    this.ListCells[i, j].Location = i + "," + j;


                    if (i < 3 && j < 3)
                    {
                        this.ListCells[i, j].Group = "grp_a";
                        // this.GroupsListDict["grp_a"].Add(this.ListCells[i, j].Value);
                    }

                    else if (i < 3 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_b";
                        //this.GroupsListDict["grp_b"].Add(this.ListCells[i, j].Value);
                    }

                    else if (i < 3 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_c";
                        // this.GroupsListDict["grp_c"].Add(this.ListCells[i, j].Value);
                    }

                    else if (i < 6 && j < 3)
                    {
                        this.ListCells[i, j].Group = "grp_d";
                        //  this.GroupsListDict["grp_d"].Add(this.ListCells[i, j].Value);
                    }


                    else if (i < 6 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_e";
                        //   this.GroupsListDict["grp_e"].Add(this.ListCells[i, j].Value);
                    }


                    else if (i < 6 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_f";
                        //  this.GroupsListDict["grp_f"].Add(this.ListCells[i, j].Value);
                    }


                    else if (i < 9 && j < 3)
                    {
                        this.ListCells[i, j].Group = "grp_g";
                        //  this.GroupsListDict["grp_g"].Add(this.ListCells[i, j].Value);
                    }


                    else if (i < 9 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_h";
                        // this.GroupsListDict["grp_h"].Add(this.ListCells[i, j].Value);
                    }



                    else if (i < 9 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_i";
                        //  this.GroupsListDict["grp_i"].Add(this.ListCells[i, j].Value);
                    }


                }
            }


            InitilizedBoardNumbers();


        }


    }
}
