using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sodoku
{
    internal class Project
    {
        public static Int16 SIZE = 9;                 //? size of sudoku row & col  سایز را نشون میده
        public static Int16 UNASSIGNED = 0;           //جای خالی را میگخ

        public static void Main()
        {
            int[,] sudoku = new int[SIZE, SIZE];
            SudokuSolver S = new SudokuSolver(UNASSIGNED, SIZE, sudoku);

            Console.WriteLine("fill the sudoku");
            S.sudokuFiller();

            Console.WriteLine("The sudoku :");
            // بدون جواب سودوکو
            S.printCurrentSudoku();

            S.solver();
            //? جواب اخریس
            Console.WriteLine("Final Sudoku");
            S.printCurrentSudoku();
        }
    }
    internal class SudokuSolver
    {
        private Int16 unassigned;
        private Int16 size;
        private int[,] sudoku;

        public Int16 Unassigned
        {
            get { return unassigned; }
            set { unassigned = value; }
        }
        public Int16 Size
        {
            get { return size; }
            set { size = value; }
        }

        public int[,] Sudoku
        {
            get { return sudoku; }
            set { sudoku = value; }
        }

        public SudokuSolver(Int16 unassigned, Int16 size, int[,] sudoku)
        {
            Unassigned = unassigned;
            Size = size;
            Sudoku = sudoku;
        }
        public SudokuSolver() { }


        public int[,] sudokuFiller()
        {
            int userInpInt = unassigned;
            string userInpStr;
            bool isExists = false;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.WriteLine("The current sudoku:");
                    printCurrentSudoku();
                    do
                    {
                        Console.WriteLine("Enter el [" + i + "][" + j + "]:");
                        userInpStr = Console.ReadLine();
                        if (
                            userInpStr == "" ||
                            userInpStr == " " ||
                            userInpStr == null)
                        {
                            userInpInt = unassigned;     // هیچی توش نیس
                        }
                        else
                        {
                            userInpInt = Convert.ToInt16(userInpStr);
                        }
                        if (userInpInt > 9 || userInpInt < 0)
                        {
                            Console.WriteLine("ERR:enter number between 0-9(space or enter for skipping) !!!");
                        }
                        isExists = checkDuplicateNum(userInpInt, i, j);

                        if (isExists && userInpInt != unassigned)
                        {
                            Console.WriteLine("dont repeat two num in row or col در سطر یا ستون عدد مشابه هست پاک کن درست کن ");
                        }
                        else
                        {
                            isExists = false;
                        }
                    } while ((userInpInt > 9 || userInpInt < 0) || isExists);

                    sudoku[i, j] = userInpInt;
                }
            }
            return sudoku;
        }

        public void printCurrentSudoku()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(sudoku[i, j]);
                    if (j != 0 || j != size)
                    {
                        Console.Write("|");
                    }

                }
                Console.Write("\n------------------\n");
            }
        }

        private bool checkDuplicateNum(int num, int x, int y)
        {
            bool isExists = false;
            for (int i = 0; i < size; i++)
            {

                if (sudoku[i, y] == num)
                {     // ستون را چک کن
                    isExists = true;
                }

                if (sudoku[x, i] == num)
                {     //سطر چک کن
                    isExists = true;
                }
            }


            int startRow = x - x % 3;
            int startCol = y - y % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sudoku[i + startRow, j + startCol] == num)
                    {
                        isExists = true;
                    }
                }
            }

            return isExists;
        }

        public bool solver()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (sudoku[i, j] == unassigned)
                    {
                        for (int n = 1; n <= size; n++)
                        {

                            if (!checkDuplicateNum(n, i, j))
                            {
                                sudoku[i, j] = n;
                                //اگه درست حل کرده بیا بیرون
                                if (solver())
                                {
                                    return true;
                                }
                                else
                                {

                                    sudoku[i, j] = unassigned;
                                }
                            }
                        }

                    }
                }
            }


            return true;

        }

    }
}
