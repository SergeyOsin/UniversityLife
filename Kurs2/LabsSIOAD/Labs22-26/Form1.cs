using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs22_26
{
    public partial class Form1 : Form
    {
        private int compare;
        private int count_per;
        private int FinishedTime;
        private int SizeArr;
        private int percsort;
        private int[] unsortarr;
        private int number_str;
        private Random rand = new Random();
        public Form1() => InitializeComponent();
        private void button1_Click(object sender, EventArgs e) => Application.Exit();
        private void InitialElem()
        {
            SizeArr = Convert.ToInt32(numericUpDown1.Value);
            percsort = Convert.ToInt32(numericUpDown2.Value);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            BackColor = Color.AliceBlue;
            dataGridView1.BackgroundColor = Color.LightSteelBlue;
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnCount = 6;
            dataGridView1.RowCount = 6;
            dataGridView1.AllowUserToAddRows = false;
            numericUpDown1.Minimum = 10;
            numericUpDown1.Maximum = (int)Math.Pow(10, 7);
            numericUpDown1.Value = 5000;
            numericUpDown2.Value = 50;
            string[] names_sorts = { "Простое 2ф", "Простое 1ф", "Естественное 2ф", "Естественное 1ф", "Поглощение" };
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1.Rows[i].Cells[1].Value = names_sorts[i];
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.BlueViolet;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.BlueViolet;
        }
        private bool isSortedArr() => unsortarr.Zip(unsortarr.Skip(1), (a, unsortarrB) => a <= unsortarrB).All(i => i);
        private void WriteToTable()
        {
            dataGridView1.Rows[number_str].Cells[2].Value = compare;
            dataGridView1.Rows[number_str].Cells[3].Value = count_per;
            dataGridView1.Rows[number_str].Cells[4].Value = FinishedTime;
            dataGridView1.Rows[number_str].Cells[5].Value = (isSortedArr()) ? "Да" : "Нет";
        }
        private void ClearStr()
        {
            for (int i = 2; i < dataGridView1.ColumnCount; i++)
                dataGridView1.Rows[number_str].Cells[i].Value = " ";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            number_str = 0;
            InitialElem();
            unsortarr = new int[SizeArr];
            for (int j = 0; j < SizeArr; j++)
                unsortarr[j] = rand.Next(0, SizeArr);
            int[] default_unsortarr = (int[])unsortarr.Clone();
            if (Convert.ToBoolean(dataGridView1.Rows[0].Cells[0].Value)) SimpleTwoPhase();
            else ClearStr();
            number_str++;
            unsortarr = (int[])default_unsortarr.Clone();
            if (Convert.ToBoolean(dataGridView1[0, 1].Value)) SimpleOnePhase();
            else ClearStr();
            number_str++;
            unsortarr = (int[])default_unsortarr.Clone();
            if (Convert.ToBoolean(dataGridView1[0, 2].Value)) Nativetwophase();
            else ClearStr();
            number_str++;
            unsortarr = (int[])default_unsortarr.Clone();
            if (Convert.ToBoolean(dataGridView1[0, 3].Value)) Nativeonephase();
            else ClearStr();
            number_str++;
            unsortarr = (int[])default_unsortarr.Clone();
            if (Convert.ToBoolean(dataGridView1[0, 4].Value)) Uptake();
            else ClearStr();
        }
        private void SimpleTwoPhase()
        {
            compare = count_per = 0;
            int StartTime = Environment.TickCount;
            int[] unsortarrB = new int[SizeArr];
            int[] unsortarrC = new int[SizeArr];
            int size_seria = 1;
            while (size_seria < SizeArr)
            {
                int Bcount = 0, Ccount = 0;
                for (int current = 0; current < SizeArr;)
                {
                    for (int i = 0; i < size_seria && current < SizeArr; i++, current++)
                    {
                        unsortarrB[Bcount++] = unsortarr[current];
                        count_per++;
                    }
                    for (int i = 0; i < size_seria && current < SizeArr; i++, current++)
                    {
                        unsortarrC[Ccount++] = unsortarr[current];
                        count_per++;
                    }
                }
                int unsortarrIndex = 0, BRun = 0, CRun = 0;
                while (BRun < Bcount || CRun < Ccount)
                {
                    int BEnd = Math.Min(BRun + size_seria, Bcount); 
                    int CEnd = Math.Min(CRun + size_seria, Ccount);
                    while (BRun < BEnd && CRun < CEnd)
                    {
                        compare++;
                        if (unsortarrB[BRun] <= unsortarrC[CRun])
                            unsortarr[unsortarrIndex++] = unsortarrB[BRun++];
                        else
                            unsortarr[unsortarrIndex++] = unsortarrC[CRun++];
                        count_per++;
                    }
                    while (BRun < BEnd)
                    {
                        unsortarr[unsortarrIndex++] = unsortarrB[BRun++];
                        count_per++;
                    }
                    while (CRun < CEnd)
                    {
                        unsortarr[unsortarrIndex++] = unsortarrC[CRun++];
                        count_per++;
                    }
                }
                size_seria *= 2;
            }
            FinishedTime = Environment.TickCount - StartTime;
            WriteToTable();
        }

        private void SimpleOnePhase()
        {
            compare = count_per = 0;
            int StartTime = Environment.TickCount;
            int[] B = new int[SizeArr];
            int[] C = new int[SizeArr];
            int[] D = new int[SizeArr];
            int[] E = new int[SizeArr];
            int size_seria = 1;
            int Bcount = 0, Ccount = 0;
            bool toB = true;
            for (int i = 0; i < SizeArr; i += size_seria)
            {
                int end = Math.Min(i + size_seria, SizeArr);
                for (int j = i; j < end; j++)
                {
                    count_per++;
                    if (j%2==0)
                        B[Bcount++] = unsortarr[j];
                    else
                        C[Ccount++] = unsortarr[j];
                }
            }
            while (size_seria < SizeArr)
            {
                int readB = 0, readC = 0;
                int writeD = 0, writeE = 0;
                toB = true;
                while (readB < Bcount || readC < Ccount)
                {
                    int endB = Math.Min(readB + size_seria, Bcount);
                    int endC = Math.Min(readC + size_seria, Ccount);
                    int i = readB, j = readC;
                    while (i < endB && j < endC)
                    {
                        compare++;
                        if (B[i] <= C[j])
                        {
                            if (toB) D[writeD++] = B[i++];
                            else E[writeE++] = B[i++];
                        }
                        else
                        {
                            if (toB) D[writeD++] = C[j++];
                            else E[writeE++] = C[j++];
                        }
                        count_per++;
                    }
                    while (i < endB)
                    {
                        if (toB) D[writeD++] = B[i++];
                        else E[writeE++] = B[i++];
                        count_per++;
                    }
                    while (j < endC)
                    {
                        if (toB) D[writeD++] = C[j++];
                        else E[writeE++] = C[j++];
                        count_per++;
                    }
                    readB = endB;
                    readC = endC;
                    toB = !toB;
                }
                Bcount = writeD;
                Ccount = writeE;
                Array.Copy(D, B, Bcount);
                count_per += Bcount;
               Array.Copy(E, C, Ccount);
                count_per += Ccount;
                size_seria *= 2;
            }
            for (int i = 0; i < SizeArr; i++)
            {
                unsortarr[i] = B[i];
                count_per++;
            }
            FinishedTime = Environment.TickCount - StartTime;
            WriteToTable();
        }

        private void Nativetwophase()
        {
            count_per = compare = 0;
            int[] unsortarrB = new int[SizeArr];
            int[] unsortarrC = new int[SizeArr];
            int StartTime = Environment.TickCount;
            bool isSorted = false;
            while (!isSorted)
            {
                int bCount = 0;
                int cCount = 0;
                bool writeToB = true;
                unsortarrB[bCount++] = unsortarr[0];
                for (int i = 1; i < SizeArr; i++)
                {
                    compare++;
                    if (unsortarr[i] < unsortarr[i - 1])
                        writeToB = !writeToB;
                    if (writeToB)
                        unsortarrB[bCount++] = unsortarr[i];
                    else
                        unsortarrC[cCount++] = unsortarr[i];
                    count_per++;
                }
                if (cCount == 0)
                {
                    isSorted = true;
                    continue;
                }
                int aIndex = 0, bPos = 0, cPos = 0;
                while (bPos < bCount && cPos < cCount)
                {
                    int bEnd = bPos;
                    while (bEnd + 1 < bCount && unsortarrB[bEnd] <= unsortarrB[bEnd + 1])
                    {
                        compare++;
                        bEnd++;
                    }
                    int cEnd = cPos;
                    while (cEnd + 1 < cCount && unsortarrC[cEnd] <= unsortarrC[cEnd + 1])
                    {
                        compare++;
                        cEnd++;
                    }
                    while (bPos <= bEnd && cPos <= cEnd)
                    {
                        compare++;
                        if (unsortarrB[bPos] <= unsortarrC[cPos])
                        {
                            unsortarr[aIndex++] = unsortarrB[bPos++];
                        }
                        else
                        {
                            unsortarr[aIndex++] = unsortarrC[cPos++];
                        }
                        count_per++;
                    }
                    while (bPos <= bEnd)
                    {
                        unsortarr[aIndex++] = unsortarrB[bPos++];
                        count_per++;
                    }

                    while (cPos <= cEnd)
                    {
                        unsortarr[aIndex++] = unsortarrC[cPos++];
                        count_per++;
                    }
                }
                while (bPos < bCount)
                {
                    unsortarr[aIndex++] = unsortarrB[bPos++];
                    count_per++;
                }

                while (cPos < cCount)
                {
                    unsortarr[aIndex++] = unsortarrC[cPos++];
                    count_per++;
                }
            }
            FinishedTime = Environment.TickCount - StartTime;
            WriteToTable();
        }

        private void Nativeonephase()
        {
            count_per = compare = 0;
            int StartTime = Environment.TickCount;
            int[] B = new int[SizeArr];
            int[] C = new int[SizeArr];
            int[] D = new int[SizeArr];
            int[] E = new int[SizeArr];
            int bCount = 0, cCount = 0;
            bool writeToB = true;
            B[bCount++] = unsortarr[0];
            count_per++;
            for (int i = 1; i < SizeArr; i++)
            {
                compare++;
                if (unsortarr[i] < unsortarr[i - 1])
                {
                    writeToB = !writeToB;
                }
                if (writeToB)
                {
                    B[bCount++] = unsortarr[i];
                    count_per++;
                }
                else
                {
                    C[cCount++] = unsortarr[i];
                    count_per++;
                }
            }
            bool toD = true;
            while (cCount > 0)
            {
                int readB = 0, readC = 0;
                int writeD = 0, writeE = 0;
                bool currentTarget = toD;
                while (readB < bCount && readC < cCount)
                {
                    int endB = readB;
                    while (endB + 1 < bCount && B[endB] <= B[endB + 1])
                    {
                        compare++;
                        endB++;
                    }
                    endB++;
                    int endC = readC;
                    while (endC + 1 < cCount && C[endC] <= C[endC + 1])
                    {
                        compare++;
                        endC++;
                    }
                    endC++;
                    int i = readB, j = readC;
                    while (i < endB && j < endC)
                    {
                        compare++;
                        if (B[i] <= C[j])
                        {
                            if (currentTarget)
                                D[writeD++] = B[i++];
                            else
                                E[writeE++] = B[i++];
                        }
                        else
                        {
                            if (currentTarget)
                                D[writeD++] = C[j++];
                            else
                                E[writeE++] = C[j++];
                        }
                        count_per++;
                    }
                    while (i < endB)
                    {
                        if (currentTarget)
                            D[writeD++] = B[i++];
                        else
                            E[writeE++] = B[i++];
                        count_per++;
                    }
                    while (j < endC)
                    {
                        if (currentTarget)
                            D[writeD++] = C[j++];
                        else
                            E[writeE++] = C[j++];
                        count_per++;
                    }
                    readB = endB;
                    readC = endC;
                    currentTarget = !currentTarget;
                }
                while (readB < bCount)
                {
                    int endB = readB;
                    while (endB + 1 < bCount && B[endB] <= B[endB + 1])
                    {
                        compare++;
                        endB++;
                    }
                    endB++;
                    while (readB < endB)
                    {
                        if (currentTarget)
                        {
                            D[writeD++] = B[readB++];
                        }
                        else
                        {
                            E[writeE++] = B[readB++];
                        }
                        count_per++;
                    }
                    currentTarget = !currentTarget;
                }
                while (readC < cCount)
                {
                    int endC = readC;
                    while (endC + 1 < cCount && C[endC] <= C[endC + 1])
                    {
                        compare++;
                        endC++;
                    }
                    endC++;
                    while (readC < endC)
                    {
                        if (currentTarget)
                        {
                            D[writeD++] = C[readC++];
                        }
                        else
                        {
                            E[writeE++] = C[readC++];
                        }
                        count_per++;
                    }
                    currentTarget = !currentTarget;
                }
                bCount = toD ? writeD : writeE;
                cCount = toD ? writeE : writeD;
                Array.Copy(toD ? D : E, B, bCount);
                Array.Copy(toD ? E : D, C, cCount);
                toD = !toD;
            }
            Array.Copy(B, unsortarr, SizeArr);
            FinishedTime = Environment.TickCount - StartTime;
            WriteToTable();
        }

        private void QuickSort(int[] arr3, int left, int right)
        {
            if (left < right)
            {
                int oporelem = arr3[left];
                int i = left;
                for (int j = i + 1; j <= right; j++)
                {
                    if (arr3[j] < oporelem)
                    {
                        i++;
                        (arr3[i], arr3[j]) = (arr3[j], arr3[i]);
                    }
                }
                (arr3[i], arr3[left]) = (arr3[left], arr3[i]);
                QuickSort(arr3, left, i - 1);
                QuickSort(arr3, i + 1, right);
            }
        }
        private void Uptake()
        {
            compare = count_per = 0;
            int prozForSort = Convert.ToInt32(numericUpDown2.Value);
            int sizeForArr = SizeArr * prozForSort / 100;
            int[] arrayToSort = new int[sizeForArr];
            int startTime = Environment.TickCount;
            for (int i = SizeArr - sizeForArr; i >= 0; i -= sizeForArr)
                ProcessSegment(i, sizeForArr, arrayToSort);
            int remainingElements = SizeArr % sizeForArr;
            if (remainingElements > 0)
                ProcessSegment(0, remainingElements, arrayToSort);
            FinishedTime = Environment.TickCount - startTime;
            WriteToTable();
        }

        private void ProcessSegment(int startIndex, int segmentSize, int[] tempArray)
        {
            int currentSize = Math.Min(segmentSize, SizeArr - startIndex);
            Array.Copy(unsortarr, startIndex, tempArray, 0, currentSize);
            QuickSort(tempArray, 0, currentSize - 1);
            int leftIndex = 0;        
            int rightIndex = startIndex + currentSize; 
            int mergeIndex = startIndex; 
            while (leftIndex < currentSize && rightIndex < SizeArr)
            {
                compare++;
                if (tempArray[leftIndex] <= unsortarr[rightIndex])
                    unsortarr[mergeIndex++] = tempArray[leftIndex++];
                else
                    unsortarr[mergeIndex++] = unsortarr[rightIndex++];
                count_per++;
            }
            while (leftIndex < currentSize)
            {
                unsortarr[mergeIndex++] = tempArray[leftIndex++];
                count_per++;
            }
        }
    }
}
