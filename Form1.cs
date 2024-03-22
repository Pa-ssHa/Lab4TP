using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private konus_class [] Cone_List = new konus_class[16];
        public Form1()
        {
            InitializeComponent();
            InitializeCone();

            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }

        konus_class Not_name;

        private void InitializeCone()
        {
            for (int i = 0; i < 16; i++)
            {
                Cone_List[i] = new konus_class(0, 0);
            }
        }

        private bool flag = false;
        private int activeIndex = -1;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// A class for calculating the diameter and volume of a straight cone.
        /// </summary>
        public class konus_class
        {
            private double radius;
            private double height;

            public konus_class(double radius, double height)
            {
                this.radius = radius; this.height = height;
            }
            public konus_class(double radius)
            {
                this.radius = radius;
            }
            public konus_class()
            {
                radius = 0;
                height = 0;
            }
            public double Calcul_Diameter()
            {
                return 2 * radius;
            }

            public double Calcul_Volume()
            {
                return (Math.PI * Math.Pow(radius, 2) * height) / 3;
            }
            public void SetRadius(double rad)
            {
                if (rad < 0)
                {
                    MessageBox.Show("Введите не отрицательное число");
                }
                else 
                {
                    radius = rad;
                }
            }
            public void SetHeight( double len_height) 
            {
                if (len_height < 0)
                {
                    MessageBox.Show("Введите не отрицательное число");
                }
                if (len_height < 9 || len_height > 30)
                {
                    MessageBox.Show("Введите число больше 9 и меньше 30 (для высоты) ");
                }
                else
                {
                    height = len_height;
                }
            }
            public double GetRadius()
            {
                return radius;
            }
            public double GetHeight()
            {
                return height;
            }
        }


        private void buttonDiam_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                textBox3.Text = Not_name.Calcul_Diameter().ToString();
                return;
            }

            double radius;
            if (double.TryParse(textBox1.Text, out radius))
            {
                if (radius > 0)
                {
                    konus_class cone = new konus_class(radius);
                    textBox3.Text = cone.Calcul_Diameter().ToString();
                }
                else
                {
                    MessageBox.Show("Введено некорректное значение радиуса");
                }
            }
            else
            {
                MessageBox.Show("Не введено значение радиуса");
            }
        }

        private void buttonVolume_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                textBox4.Text = Not_name.Calcul_Volume().ToString();
                return;
            }

            double radius, height;
            if (double.TryParse(textBox1.Text, out radius) && double.TryParse(textBox2.Text, out height))
            {
                if (radius >= 0)
                {
                    if (height > 9 && height < 30)
                    {
                        konus_class cone = new konus_class(radius, height);
                        textBox4.Text = cone.Calcul_Volume().ToString();
                    }
                    else
                    {
                        MessageBox.Show("Значение высоты должно быть от 9 до 30");
                    }
                }
                else
                {
                    MessageBox.Show("Введите неотрицательное значение для радиуса");
                }
            }
            else
            {
                MessageBox.Show("Не все значения введены");
            }
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            
        }

        private void button_enter_Click(object sender, EventArgs e)
        {
            flag = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (activeIndex != -1)
            {
                double radius, height;
                if (double.TryParse(textBox1.Text, out radius) && double.TryParse(textBox2.Text, out height))
                {
                    Cone_List[activeIndex].SetRadius(radius);
                    Cone_List[activeIndex].SetHeight(height);
                    listBox1.Items[activeIndex] = $"Конус {activeIndex + 1}: Радиус - {radius}, Высота - {height}, Диаметр - {Cone_List[activeIndex].Calcul_Diameter()}, Объем - {Cone_List[activeIndex].Calcul_Volume()}";
                }
                else
                {
                    MessageBox.Show("Некорректные значения радиуса или высоты");
                }
            }
            else
            {
                MessageBox.Show("Выберите объект из списка для сохранения изменений");
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
 
            int index = 0;
            foreach (konus_class cone in Cone_List)
            {
                listBox1.Items.Add($"Конус {index + 1}: Радиус - {cone.GetRadius()}, Высота - {cone.GetHeight()}, Диаметр - {cone.Calcul_Diameter()}, Объем - {cone.Calcul_Volume()}");
                index++;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeIndex = listBox1.SelectedIndex;
            if (activeIndex >= 0 && activeIndex < 16)
            {
                textBox1.Text = Cone_List[activeIndex].GetRadius().ToString();
                textBox2.Text = Cone_List[activeIndex].GetHeight().ToString();
            }
        }

        private void buttonClearList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            
            for (int i = 0; i < 16; i++)
            {
                Cone_List[i] = new konus_class(0, 0);
            }
        }
    }
}
