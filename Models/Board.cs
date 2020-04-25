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

        public string GeneratedTime { set; get; }

        public Board()
        {

            GenerateBoard();
        }

        private int GetRandomNumber(int row, int col)
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
            /*
            int ErrorFlag = -1;

            foreach (var trial in this.ListCells[row,col].Tries)
            {
                if (trial.Value == false)
                {
                    this.ListCells[row, col].Tries.Remove(trial.Key);  //To mark as used.
                    this.ListCells[row, col].Tries.Add(trial.Key, true);
                    return trial.Key;
                }
            }

            return ErrorFlag;
            */
        }


        private bool IsExistOnColumn(int randNumber, int row)
        {

            for (int col = 0; col < 9; col++)
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
            for (int row = 0; row > 9; row++)
            {
                if (randNumber == ListCells[row, col].Value)
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
                        this.GroupsListDict["grp_a"].Add(this.ListCells[i, j].Value);
                    }

                    else if (i < 3 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_b";
                        this.GroupsListDict["grp_b"].Add(this.ListCells[i, j].Value);
                    }

                    else if (i < 3 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_c";
                        this.GroupsListDict["grp_c"].Add(this.ListCells[i, j].Value);
                    }

                    else if (i < 6 && j < 3)
                    {
                        this.ListCells[i, j].Group = "grp_d";
                        this.GroupsListDict["grp_d"].Add(this.ListCells[i, j].Value);
                    }


                    else if (i < 6 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_e";
                        this.GroupsListDict["grp_e"].Add(this.ListCells[i, j].Value);
                    }


                    else if (i < 6 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_f";
                        this.GroupsListDict["grp_f"].Add(this.ListCells[i, j].Value);
                    }


                    else if (i < 9 && j < 3)
                    {
                        this.ListCells[i, j].Group = "grp_g";
                        this.GroupsListDict["grp_g"].Add(this.ListCells[i, j].Value);
                    }


                    else if (i < 9 && j < 6)
                    {
                        this.ListCells[i, j].Group = "grp_h";
                        this.GroupsListDict["grp_h"].Add(this.ListCells[i, j].Value);
                    }



                    else if (i < 9 && j < 9)
                    {
                        this.ListCells[i, j].Group = "grp_i";
                        this.GroupsListDict["grp_i"].Add(this.ListCells[i, j].Value);
                    }






                }
            }



            for (int i = 0; i < 9; i++)
            {
                for (int j = 0, RecursiveColIndex = 1; j < 9; j++)
                {

                    int RandNumber = GetRandomNumber(i, j);
                    this.ListCells[i, j].Tries[RandNumber] = true;
                    bool IsExistonColumn = IsExistOnColumn(RandNumber, i);
                    bool IsExistonRow = IsExistOnRow(RandNumber, j);
                    bool IsExistonBox = IsExistOnBox(RandNumber, this.ListCells[i, j].Group);//TOOD: add implemention of function to check if already exist in the close numbers box. need to use dictionary

                    while (IsExistonColumn || IsExistonRow || IsExistonBox)
                    {

                        //CHECK IF ALL TRIES FOR THIS CELL ARE MARKED AS TRUE (USED ALREADY)
                        var AllUsedNumbersList = ListCells[i, j].Tries.Where(x => x.Value == false);
                        if (AllUsedNumbersList.Count() == 9)
                            while (RecursiveColIndex <= j && (IsExistonColumn || IsExistonRow || IsExistonBox))
                            {//all already used, start recursive number changing

                                this.ListCells[i, j].Tries[0] = true;
                                this.ListCells[i, j].Tries[1] = false;
                                this.ListCells[i, j].Tries[2] = false;
                                this.ListCells[i, j].Tries[3] = false;
                                this.ListCells[i, j].Tries[4] = false;
                                this.ListCells[i, j].Tries[5] = false;
                                this.ListCells[i, j].Tries[6] = false;
                                this.ListCells[i, j].Tries[7] = false;
                                this.ListCells[i, j].Tries[8] = false;
                                this.ListCells[i, j].Tries[9] = false;


                                this.ListCells[i, j- RecursiveColIndex].Tries[0] = true;
                                this.ListCells[i, j- RecursiveColIndex].Tries[1] = false;
                                this.ListCells[i, j- RecursiveColIndex].Tries[2] = false;
                                this.ListCells[i, j- RecursiveColIndex].Tries[3] = false;
                                this.ListCells[i, j- RecursiveColIndex].Tries[4] = false;
                                this.ListCells[i, j- RecursiveColIndex].Tries[5] = false;
                                this.ListCells[i, j- RecursiveColIndex].Tries[6] = false;
                                this.ListCells[i, j- RecursiveColIndex].Tries[7] = false;
                                this.ListCells[i, j- RecursiveColIndex].Tries[8] = false;
                                this.ListCells[i, j- RecursiveColIndex].Tries[9] = false;


                                RandNumber = GetRandomNumber(i, j);
                                this.ListCells[i, j].Tries[RandNumber] = true;
                                IsExistonColumn = IsExistOnColumn(RandNumber, i);
                                IsExistonRow = IsExistOnRow(RandNumber, j);
                                IsExistonBox = IsExistOnBox(RandNumber, this.ListCells[i, j].Group);


                                if(!IsExistonColumn && !IsExistonRow && !IsExistonBox)
                                this.ListCells[i, j - RecursiveColIndex].Value = RandNumber;
                                
                                RecursiveColIndex++;



                            }


                        // if so then reset tries variable all back to false,  change the previous number cell value using random function, and repeat.
                        //??how to change previous number which would be increased (or decresed) up to 0. (first)??
                        RandNumber = GetRandomNumber(i, j);
                        this.ListCells[i, j].Tries[RandNumber] = true;
                        IsExistonColumn = IsExistOnColumn(RandNumber, i);
                        IsExistonRow = IsExistOnRow(RandNumber, j);
                        IsExistonBox = IsExistOnBox(RandNumber, this.ListCells[i, j].Group);




                    }

                    this.ListCells[i, j].Value = RandNumber;




                }
            }



        }


    }
}
