using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Объект компаса
        /// </summary>
        private Kompas _kompas = new Kompas();

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Действия при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            test100.Enabled = false;
            Height.Enabled = false;
            Weight.Enabled = false;
            Width.Enabled = false;
            Left.Enabled = false;
            Right.Enabled = false;
            Build_window.Enabled = false;
        }

        /// <summary>
        /// Нажатие кнопки построения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Build_window_Click(object sender, EventArgs e)
        {
            var window = new WindowParametrs
            {
                SectionNumber = Convert.ToInt32(Section.SelectedItem),
                OpenSection = Convert.ToInt32(OpenSection.SelectedItem),
                LengthWidth = Convert.ToInt32(Width.Value),
                LengthHeight = Convert.ToInt32(Height.Value),
                LengthWeight = Convert.ToInt32(Weight.Value)
            };


            if (Left.Checked)
            {
                window.HandlePosition = HandlePosition.Left;
            }
            if (Right.Checked)
            {
                window.HandlePosition = HandlePosition.Right;
            }

            _kompas.BuildWindow(window);
        }

        /// <summary>
        /// Закрытие
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Выбор секции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Section_SelectedIndexChanged(object sender, EventArgs e)
        {
            Build_window.Enabled = true;
            OpenSection.Items.Clear();
            if (Section.SelectedIndex == 0)
            {
                OpenSection.Items.Add("1");
                Height.Minimum = 580;
                Height.Maximum = 1160;
                Width.Minimum = 570;
                Width.Maximum = 870;
                Weight.Minimum = 24;
                Weight.Maximum = 48;
                Height.Enabled = true;
                Weight.Enabled = true;
                Width.Enabled = true;
                Left.Enabled = true;
                Right.Enabled = true;
            }

            if (Section.SelectedIndex == 1)
            { 
                string[] mas2 = { "1", "2" };
                OpenSection.Items.AddRange(mas2);
                Height.Minimum = 580;
                Height.Maximum = 1470;
                Width.Minimum = 570;
                Width.Maximum = 1360;
                Weight.Minimum = 24;
                Weight.Maximum = 48;
                Height.Enabled = true;
                Weight.Enabled = true;
                Width.Enabled = true;
                Left.Enabled = true;
                Right.Enabled = true;
            }

            if (Section.SelectedIndex == 2)
            {
                string[] mas2 = { "1", "2","3" };
                OpenSection.Items.AddRange(mas2);
               
                Height.Minimum = 580;
                Height.Maximum = 2060;
                Width.Minimum = 570;
                Width.Maximum = 1770;
                Weight.Minimum = 24;
                Weight.Maximum = 48;
                Height.Enabled = true;
                Weight.Enabled = true;
                Width.Enabled = true;
                Left.Enabled = true;
                Right.Enabled = true;
            }

            if (Section.SelectedIndex == 3)
            {
                string[] mas2 = { "1", "2","3","4" };
                OpenSection.Items.AddRange(mas2);
                
                Height.Minimum = 580;
                Height.Maximum = 2755;
                Width.Minimum = 570;
                Width.Maximum = 2670;
                Weight.Minimum = 24;
                Weight.Maximum = 48;
                Height.Enabled = true;
                Weight.Enabled = true;
                Width.Enabled = true;
                Left.Enabled = true;
                Right.Enabled = true;
            }
            Left.Checked = false;
            Right.Checked = true;
        }

        /// <summary>
        /// Быстрое заполнение формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FsatVariable_Click(object sender, EventArgs e)
        {
            Section.SelectedIndex = 1;
            OpenSection.SelectedIndex = 1;
        }

        /// <summary>
        /// Нагрузочный тест
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void test100_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();

            string fileName = DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss-") + "test.txt";
            FsatVariable_Click(sender, e);
            for (int i = 0; i < 101; i++)
            {
                stopWatch.Start();

                Build_window_Click(sender, e); 

                stopWatch.Stop();
                FileStream file = new FileStream(fileName, FileMode.Append);
                StreamWriter writer = new StreamWriter(file); 

                string elapsedTime =
                    $"{stopWatch.Elapsed.Milliseconds + stopWatch.Elapsed.Seconds * 1000 + stopWatch.Elapsed.Minutes * 60 * 1000}";
                writer.Write("({0};{1})", i, elapsedTime);
                writer.Close();
                stopWatch.Reset();
            }
        }

        private void OpenSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Left.Checked = false;
            Right.Checked = true;
        }
    }
}