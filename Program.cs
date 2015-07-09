using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random
{
    class Random
    {
        int[] dauvao = { 1 ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,0,
                         0 ,0 ,0 ,0 ,0 ,0 ,0 ,-4 ,0,
                         0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,0,
                        -1 ,2 ,3 ,0 ,1 ,0 ,0 ,2 ,0,
                         2 ,3 ,1 ,1 ,1 ,0 ,1 ,2 ,1,
                         1 ,-1,2 ,3 ,1 ,0 ,1 ,1 ,2,
                         0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0,
                         0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0,
                         1 ,2 ,-1,0 ,0 ,0 ,0 ,0 ,0
                        };
        int[,] input = new int[9, 9];//Ma tran dau vao
        Node[,] matrix = new Node[9, 9]; //Ma tran cac node
        int start;
        int end;
        private bool havePath = false;

        public static void Main(String[] args)
        {

            Random random = new Random();
            for (int i = 0; i < 81; i++)
            {
                random.input[i / 9, i % 9] = random.dauvao[i];
            }
            random.start = 0;
            random.end = 75;
            random.makeMatrix();
            random.findWay();
            while (true) ;
        }
        public void setMatrix(int[,] inputMatrix)
        {
            input = inputMatrix;
        }
        private void makeMatrix()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Node node = new Node();
                    //Neu node o phia tren nho hon hoac bang 0 thi gan la true,nguoc lai la false
                    if (i - 1 >= 0)
                    {
                        if (input[i - 1, j] <= 0)
                        {
                            node.setAdjacency(0, 9 * (i - 1) + j);
                        }

                    }
                    else
                    {
                        node.setAdjacency(0, -1);
                    }


                    //Neu node o phia trai nho hon hoac bang 0 thi gan la true,nguoc lai la false
                    if (j - 1 >= 0)
                    {
                        if (input[i, j - 1] <= 0)
                        {
                            node.setAdjacency(1, 9 * i + j - 1);
                        }
                    }
                    else
                    {
                        node.setAdjacency(1, -1);
                    }


                    //Neu node o phia duoi nho hon hoac bang 0 thi gan la true,nguoc lai la false
                    if (i + 1 < 9)
                    {
                        if (input[i + 1, j] <= 0)
                        {
                            node.setAdjacency(2, 9 * (i + 1) + j);
                        }                       
                    }
                    else
                    {
                        node.setAdjacency(2, -1);
                    }
                    //Neu node o phia phai nho hon hoac bang 0 thi gan la true,nguoc lai la false
                    if (j + 1 < 9)
                    {
                        if (input[i, j + 1] <= 0)
                        {
                            node.setAdjacency(3, 9 * i + j + 1);
                        }                        
                    }
                    else
                    {
                        node.setAdjacency(3, -1);
                    }
                    matrix[i, j] = node;

                }

            }
        }


        private void findWay()
        {

            int[] open = new int[81];
            for (int i = 0; i < 81; i++)
            {
                open[i] = -1;
            }
            int openLength = 0;
            int[] close = new int[81];
            for (int i = 0; i < 81; i++)
            {
                close[i] = -1;
            }
            int closeLength = 0;
            int startRow = start / 9;
            int startCol = start % 9;
            int endRow = end / 9;
            int endCol = end % 9;

            //Dua start vao open
            open[openLength++] = start;
            //Dat g bang 0
            matrix[startRow, startCol].setG(0);
            //Khoang cach tu diem start den dich theo tinh theo khoang cach mahattan
            matrix[startRow, startCol].setH(Manhattan(startRow, startCol, endRow, endCol));
            //Dat f = g + h
            matrix[startRow, startCol].setF(matrix[startRow, startCol].getG() + matrix[startRow, startCol].getH());

            while (openLength > 0)
            {
                int fmin = 100000;
                int minPosition = 0;
                int minRow;
                int minCol;
                int minPosinOpenArray = 0; //Luu giu chi so cua node chua f nho nhat trong open
                //Tim vi tri node co f nho nhat
                for (int i = 0; i < openLength; i++)
                {
                    int position = open[i];
                    int row = position / 9;
                    int col = position % 9;
                    if (fmin > matrix[row, col].getF())
                    {
                        fmin = matrix[row, col].getF();
                        minPosition = position;
                        minPosinOpenArray = i;
                    }
                }

                minRow = minPosition / 9;
                minCol = minPosition % 9;

                //Tra ve duong di neu node co f nho nhat tim duoc trung voi end
                if (minPosition == end)
                {
                    //In ra duong di tu diem dau den diem cuoi
                    int consideringNode = minPosition;
                    while (consideringNode != start)
                    {
                        System.Console.Write(consideringNode.ToString() + "-->");
                        consideringNode = matrix[consideringNode / 9, consideringNode % 9].getPrev();
                    }
                    System.Console.Write(start.ToString());

                    break;
                }
                //Xoa node co f nho nhat vua tim duoc khoi open
                open[minPosinOpenArray] = open[openLength - 1];
                openLength--;
                //Them node nay vao trong close
                close[closeLength++] = minPosition;
                //Xet cac dinh ke voi node co f nho nhat 
                int[] Adj = new int[4];
                Adj = matrix[minRow, minCol].getAdjacency();
                for (int i = 0; i < 4; i++)
                {
                    if (Adj[i] != -1)
                    {
                        bool isExistInClose = false;
                        for (int k = 0; k < closeLength; k++)
                        {
                            if (close[k] == Adj[i])
                            {
                                isExistInClose = true;
                                break;
                            }
                        }
                        if (isExistInClose)
                        {
                            continue;
                        }
                        else
                        {
                            int temp = matrix[minRow, minCol].getG() + Manhattan(minRow, minCol, Adj[i] / 9, Adj[i] % 9);
                            bool isExistInOpen = false;
                            for (int j = 0; j < openLength; j++)
                            {
                                if (Adj[i] == open[j])
                                {
                                    isExistInOpen = true;
                                    break;
                                }
                            }
                            if (!isExistInOpen || temp < matrix[Adj[i] / 9, Adj[i] % 9].getG())
                            {
                                matrix[Adj[i] / 9, Adj[i] % 9].setPrev(minPosition);
                                matrix[Adj[i] / 9, Adj[i] % 9].setG(temp);
                                matrix[Adj[i] / 9, Adj[i] % 9].setH(Manhattan(Adj[i] / 9, Adj[i] % 9, endRow, endCol));
                                //Neu node ke dang xet khong co trong open thi ta cho vao open
                                if (!isExistInOpen)
                                {
                                    open[openLength++] = Adj[i];
                                }
                            }
                        }
                    }
                }


            }
        }

        private int Sum(int[,] matrix)
        {
            int S = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    S += matrix[i, j];
                }
            }
            return S;
        }
        private int Manhattan(int row1, int col1, int row2, int col2)
        {
            int result = Math.Abs(row1 - row2) + Math.Abs(col1 - col2);
            return result;
        }
    }
}

