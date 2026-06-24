using System.Diagnostics.Metrics;
using System.Globalization;

namespace LB12
{
    public partial class Form1 : Form
    {
        private int compare_pyramid;
        private int count_per_pyramid;
        private int count_per_quick;
        private int compare_quick;
        public Form1() => InitializeComponent();
        private void button1_Click(object sender, EventArgs e) => Application.Exit();
        private void writetoTable(int numb_row,int compare,int count_per,int time, int[]arr)
        {
            dataGridView1.Rows[numb_row].Cells[2].Value = compare;
            dataGridView1.Rows[numb_row].Cells[3].Value = count_per;
            dataGridView1.Rows[numb_row].Cells[4].Value = time;
            dataGridView1.Rows[numb_row].Cells[5].Value = (isSorted(arr)) ? "Да" : "Нет";
        }
        private void clearcells(int id)
        {
            for (int i = 2; i < dataGridView1.ColumnCount; i++)
                dataGridView1.Rows[id].Cells[i].Value = " ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Size_Array = Convert.ToInt32(numericUpDown1.Value);
            Random rd = new Random();
            int[] array = new int[Size_Array];
            for (int i = 0; i < Size_Array; i++) array[i] = rd.Next(0, Size_Array);
            int[] arr = (int[])array.Clone();
            if (Convert.ToBoolean(dataGridView1.Rows[0].Cells[0].Value))
                BubbleSort(Size_Array, arr);
            else clearcells(0);
            int[] arr_1 = (int[])array.Clone();
            int[] arrayCopy = (int[])array.Clone();
            if (Convert.ToBoolean(dataGridView1.Rows[1].Cells[0].Value))
            {
                ChooseSort(Size_Array, arrayCopy);
                dataGridView1.Rows[1].Cells[5].Value = (isSorted(arrayCopy)) ? "Да" : "Нет";
            }
            else clearcells(1);
            if (Convert.ToBoolean(dataGridView1.Rows[2].Cells[0].Value))
            {
                DirectConnection(Size_Array, arr_1);
                dataGridView1.Rows[2].Cells[5].Value = (isSorted(arr_1)) ? "Да" : "Нет";
            }
            else clearcells(2);
            int[] arr2 = (int[])array.Clone();
            if (Convert.ToBoolean(dataGridView1.Rows[3].Cells[0].Value))
            {
                Sort_Shell(Size_Array, arr2);
                dataGridView1.Rows[3].Cells[5].Value = (isSorted(arr2)) ? "Да" : "Нет";
            }
            else clearcells(3);
            int[] arr3 = (int[])array.Clone();
            if (Convert.ToBoolean(dataGridView1.Rows[4].Cells[0].Value))
            {
                count_per_quick = 0;
                compare_quick = 0;
                int StartTime = Environment.TickCount;
                QuickSort(arr3, 0, Size_Array - 1);
                int EndTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[4].Cells[2].Value = compare_quick;
                dataGridView1.Rows[4].Cells[3].Value = count_per_quick;
                dataGridView1.Rows[4].Cells[4].Value = EndTime;
                dataGridView1.Rows[4].Cells[5].Value = (isSorted(arr3)) ? "Да" : "Нет";
            }
            else clearcells(4);
            int[] arrayforLineSort = (int[])array.Clone();
            if (Convert.ToBoolean(dataGridView1.Rows[5].Cells[0].Value))
                LineSort(arrayforLineSort);
            else clearcells(5);
            int[] arrayBuiltInFunc = (int[])array.Clone();
            if (Convert.ToBoolean(dataGridView1.Rows[6].Cells[0].Value))
                BuiltInFunc(arrayBuiltInFunc);
            else clearcells(6);
            int[] arraypyramidal = (int[])array.Clone();
            if (Convert.ToBoolean(dataGridView1.Rows[7].Cells[0].Value))
                pyramidalsort(arraypyramidal);
            else clearcells(7);
        }
        private bool isSorted(int[]arr) => arr.Zip(arr.Skip(1), (a, b) => a <= b).All(i => i);
        private void BubbleSort(int Size_Array, int[]unarray)
        {
            long count_per = 0, compare = 0;
            bool flag = true;
            long StartTime = Environment.TickCount;
            for (int i = 0; i<Size_Array-1; i++)
            {
                for (int j = 0; j < Size_Array-1-i; j++)
                {
                    if (unarray[j] > unarray[j+1])
                    {
                        flag = false;
                        count_per++;
                        (unarray[j], unarray[j+1]) = (unarray[j+1], unarray[j]);
                    }
                    compare++;
                }
                if (flag)
                    break;
                flag = true;
            }
            long EndTime = Environment.TickCount - StartTime;
            dataGridView1.Rows[0].Cells[2].Value = compare;
            dataGridView1.Rows[0].Cells[3].Value = count_per;
            dataGridView1.Rows[0].Cells[4].Value = EndTime;
            dataGridView1.Rows[0].Cells[5].Value = (isSorted(unarray)) ? "Да" : "Нет";
        }
        private void ChooseSort(int Size_Array, int[] unarray)
        {
            long count_per = 0, compare = 0;
            long StartTime = Environment.TickCount;
            for (int i = Size_Array - 1; i > 0; i--)
            {
                int max_ind = i;
                for (int j = 0; j < i; j++)
                {
                    if (unarray[j] > unarray[max_ind])
                        max_ind = j;
                    compare++;
                }
                count_per++;
                (unarray[i], unarray[max_ind]) = (unarray[max_ind], unarray[i]);
            }
            long EndTime = Environment.TickCount - StartTime;
            dataGridView1.Rows[1].Cells[2].Value = compare;
            dataGridView1.Rows[1].Cells[3].Value = count_per;
            dataGridView1.Rows[1].Cells[4].Value = EndTime;
        }
        private void DirectConnection(int Size_Array, int[] unsortedarray1)
        {
            long count_per = 0, compare = 0;
            long StartTime = Environment.TickCount;
            long minIndex = 0;
            for (int k = 1; k < Size_Array; k++)
                if (unsortedarray1[k] < unsortedarray1[minIndex])
                    minIndex = k;
            (unsortedarray1[0], unsortedarray1[minIndex]) = (unsortedarray1[minIndex], unsortedarray1[0]);
            for(int i = 1; i < Size_Array; i++)
            {
                int value = unsortedarray1[i];
                int j;
                for (j = i - 1; unsortedarray1[j] > value; j--,compare++)
                {
                    unsortedarray1[j + 1] = unsortedarray1[j];
                    count_per++;
                }
                compare++;
                unsortedarray1[j + 1] = value;
            }
            long EndTime = Environment.TickCount - StartTime;
            dataGridView1.Rows[2].Cells[2].Value = compare;
            dataGridView1.Rows[2].Cells[3].Value = count_per;
            dataGridView1.Rows[2].Cells[4].Value = EndTime;
        }
        private void Sort_Shell(int Size_ar, int[] arr1)
        {
            int compare = 0, count_per = 0;
            int count_steps = (int)Math.Log2(Size_ar) - 1;
            int step = (int)Math.Pow(2, count_steps) - 1;
            int StartTime = Environment.TickCount;
            while (step > 0)
            {
                for(int i = step; i < Size_ar; i++)
                {
                    int value = arr1[i];
                    int j;
                    for (j = i-step;j>=0 && arr1[j]>value;j-=step, compare++)
                    {
                        arr1[j+step] = arr1[j];
                        count_per++;
                    }
                    compare++;
                    arr1[j+step] = value;
                }
                step /= 2;
            }
            int EndTime = Environment.TickCount - StartTime;
            writetoTable(3, compare, count_per, EndTime, arr1);
        }
        private void QuickSort(int[] arr3,int left,int right)
        {
            if (left < right)
            {
                int oporelem = arr3[left];
                int i = left;
                for(int j = i + 1; j <= right; j++)
                {
                    compare_quick++;
                    if (arr3[j] < oporelem)
                    {
                        i++;
                        (arr3[i], arr3[j]) = (arr3[j], arr3[i]);
                        count_per_quick++;
                    }
                }
                (arr3[i], arr3[left]) = (arr3[left], arr3[i]);
                count_per_quick++;
                QuickSort(arr3, left, i-1);
                QuickSort(arr3, i+1,right);
            }
        }
        private void LineSort(int[] array1)
        {
            int compare = 0, count_per = 0;
            int StartTime = Environment.TickCount;
            int[] arraycount = new int[array1.Length];
            for (int i = 0; i < array1.Length; i++) {
                arraycount[array1[i]]++;
                count_per++;
            }
            for(int elem = 0, count = 0; elem < array1.Length; elem++)
            {
                compare++;
                for (int i = count; i < arraycount[elem] + count; i++)
                {
                    count_per++;
                    array1[i] = elem;
                }
                count += arraycount[elem];
            }
            int EndTime = Environment.TickCount - StartTime;
            writetoTable(5, compare, count_per, EndTime, array1);
        }

        private void BuiltInFunc(int[] array2)
        {
            int StartTime = Environment.TickCount;
            Array.Sort(array2);
            int EndTime = Environment.TickCount - StartTime;
            dataGridView1.Rows[6].Cells[2].Value = "-";
            dataGridView1.Rows[6].Cells[3].Value = "-";
            dataGridView1.Rows[6].Cells[4].Value = EndTime;
            dataGridView1.Rows[6].Cells[5].Value = (isSorted(array2)) ? "Да" : "Нет";
        }

        private void toDown(int[] array3, int index, int size_array)
        {
            while (2 * index+1 < size_array)
            {
                int index_j = 2 * index;
                compare_pyramid++;
                if (array3[index_j] < array3[index_j + 1]) index_j++;
                compare_pyramid++;
                if (array3[index] >= array3[index_j]) break;
                count_per_pyramid++;
                (array3[index], array3[index_j]) = (array3[index_j], array3[index]);
                index = index_j;
            }
        }
        
        private void pyramidalsort(int[] array3)
        {
            compare_pyramid = count_per_pyramid = 0;
            int len = array3.Length;
            int StartTime = Environment.TickCount;
            for (int i =(len-1)/2; i >= 0; i--)
                toDown(array3, i, len);
            int last_ind = len - 1;
            while (last_ind > 0)
            {
                (array3[last_ind], array3[0]) = (array3[0], array3[last_ind]);
                count_per_pyramid++;
                toDown(array3, 0, last_ind);
                last_ind--;
            }
            int EndTime = Environment.TickCount - StartTime;
            writetoTable(7, compare_pyramid, count_per_pyramid, EndTime, array3);
        }
        
        private void tablewrite()
        {
            label1.Text = "Лабораторные работа 12-17";
            Text = "Лабораторные работы 12-17";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowCount = 8;
            dataGridView1.ColumnCount = 6;
            string[] name_sorts = { "Обмен", "Выбор", "Включение", "Шелла", "Быстрая", "Линейная", "Встроенная","Пирамидальная" };
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1.Rows[i].Cells[1].Value = name_sorts[i];
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.CornflowerBlue;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.CornflowerBlue;
        }
        private void Form1_Load(object sender, EventArgs e) => tablewrite();
    }
}
