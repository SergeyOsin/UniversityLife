using System.Runtime.InteropServices.Marshalling;

namespace Lab8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private const int Size = 1000; //размер таблицы хеш-адресов (от 0 до 999)
        private int[] keys = new int[Size];

        private const int SizeForMOAandChain = 10000; 
        private int[] m1 = new int[SizeForMOAandChain];
        private int[] m2 = new int[SizeForMOAandChain];

        LinkedList<int>[]AllList = new LinkedList<int>[Size];
        Random rnd = new Random();
        private bool isSimple(int elem)
        {
            if (elem <= 1) return false;
            if (elem == 2) return true;
            if (elem > 2 && elem % 2 == 0) return false;
            for (int i = 3; i < elem; i+=2)
                if (elem % i == 0)
                    return false;
            return true;
        }
        private int Method_del(int key,int Size)
        {
            int m = Size;
            while (!isSimple(m)) m--;
            return key % m;
        }
        private int Method_mid_square(int key, int Size)
        {
            long squared = (long)key * key;
            if (squared < Size) return (int)squared;
            long squaredLength = (int)Math.Log10(squared) + 1;
            int lengthHash = (int)Math.Log10(Size) + 1;
            long middleStart = (squaredLength - lengthHash)/2; 
            long divisor = (int)Math.Pow(10, middleStart);
            int middleDigits = (int)((squared / divisor) % Size);
            return middleDigits;
        }
        private int Method_svert(int key, int size)
        {
            int hash = 0;
            while (key > 0)
            {
                hash += key % size;
                key /= size;
            }
            return hash % size;
        }
        private int Method_multiply(int key, int size)
        {
            double a = (Math.Sqrt(5) - 1) / 2;
            return (int)(size * ((key * a) - (int)(key * a)));
        }

        private void CreateArray()
        {
            for (int i = 0; i < Size; i++) keys[i] = rnd.Next(0, 100000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Size; i++)
            {
                AllList[i] = new LinkedList<int>();
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            int countComparison = Convert.ToInt32(numericUpDown1.Value);//количество сравнений
            int t1 = 0, t2 = 0, t3 = 0, t4 = 0;
            for (int j = 0; j < countComparison; j++)
            {
                CreateArray();
                int k1 = 0, k2 = 0, k3 = 0, k4 = 0;
                for (int i = 0; i < Size; i++)
                {
                    int hash1 = Method_del(keys[i], Size);
                    int hash2 = Method_mid_square(keys[i], Size);
                    int hash3 = Method_multiply(keys[i], Size);
                    int hash4 = Method_svert(keys[i], Size);
                    if (AllList[hash1].Count > 0) k1++;
                    AllList[hash1].AddLast(keys[i]);
                    if (AllList[hash2].Count > 0) k2++;
                    AllList[hash2].AddLast(keys[i]);
                    if (AllList[hash3].Count > 0) k3++;
                    AllList[hash3].AddLast(keys[i]);
                    if (AllList[hash4].Count > 0) k4++;
                    AllList[hash4].AddLast(keys[i]);
                }

                for (int i = 0; i < Size; i++)
                {
                    AllList[i].Clear();
                }
                if (k1 <= k2 && k1 <= k3 && k1 <= k4) t1++;
                if (k2 <= k1 && k2 <= k3 && k2 <= k4) t2++;
                if (k3 <= k1 && k3 <= k2 && k3 <= k4) t3++;
                if (k4 <= k1 && k4 <= k2 && k4 <= k3) t4++;
            }
            textBox1.Text = t1.ToString();
            textBox2.Text = t2.ToString();
            textBox3.Text = t3.ToString();
            textBox4.Text = t4.ToString();
            
        }



        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SizeForMOAandChain; i++) //заполняем массивы данными
            {
                m1[i] = rnd.Next(1, SizeForMOAandChain); //числа для построения хэш-таблицы
                m2[i] = rnd.Next(0, 20000); //поиск в хэш-таблице
            }
            MOA(m1, m2);
            Chain(m1, m2);
        }
        private void MOA(int[] m1, int[] m2)
        {
            int[] moa = new int[SizeForMOAandChain]; //хэш-таблица 
            int countFind = 0, countCompare = 0;
            for (int i = 0; i < SizeForMOAandChain; i++) //с помощью МАО избегаем коллизии
            {
                int hash = Method_multiply(m1[i], SizeForMOAandChain);

                while (moa[hash] != 0)
                {
                    hash = (hash + 1) % SizeForMOAandChain;
                }
                moa[hash] = m1[i];

            }

            int startTime = Environment.TickCount;
            for (int i = 0; i < SizeForMOAandChain; i++)
            {
                int hash = Method_multiply(m2[i], SizeForMOAandChain);
                countCompare++;
                if (moa[hash] == m2[i]) { countFind++; continue; }

                int pos = (hash + 1) % SizeForMOAandChain;
                while (hash != pos)
                {
                    countCompare++;
                    if (moa[pos] == m2[i]) { countFind++; break; }
                    pos = (pos + 1) % SizeForMOAandChain;
                }
            }
            int resultTime = Environment.TickCount - startTime;
            textBox5.Text = resultTime.ToString();
            textBox6.Text = ((double)countCompare / SizeForMOAandChain).ToString();
            textBox7.Text = countFind.ToString();
        }
        private void Chain(int[] m1, int[] m2)
        {
            LinkedList<int>[] chain = new LinkedList<int>[SizeForMOAandChain]; //хэш-таблица
            int countFind = 0, countCompare = 0;
            for (int i = 0; i < SizeForMOAandChain; i++)
            {
                chain[i] = new LinkedList<int>();
            }

            for (int i = 0; i < SizeForMOAandChain; i++)//с помощью метода цепочек избавляемся от коллизий
            {
                int hash = Method_multiply(m1[i], SizeForMOAandChain);
                chain[hash].AddLast(m1[i]);
            }

            int startTime = Environment.TickCount;
            for (int i = 0; i < SizeForMOAandChain; i++)
            {
                int hash = Method_multiply(m2[i], SizeForMOAandChain);
                countCompare++;
                foreach (int search in chain[hash])
                {
                    countCompare++;
                    if (search == m2[i])
                    {
                        countFind++;
                        break;
                    }
                }
            }
            int resultTime = Environment.TickCount - startTime;

            textBox8.Text = resultTime.ToString();
            textBox9.Text = ((double)countCompare / SizeForMOAandChain).ToString();
            textBox10.Text = countFind.ToString();
        }


    }
}

        

