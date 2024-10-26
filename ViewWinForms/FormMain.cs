using BusinessLogic;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ViewWinForms
{
    public partial class FormMain : Form
    {
        public Logic logic;
        public FormMain()
        {
            InitializeComponent();
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            logic = ninjectKernel.Get<Logic>();
        }
        private void init_grid()
        {
            dataGridViewStudents.DataSource = null;
            dataGridViewStudents.Columns.Clear();

            dataGridViewStudents.Columns.Add("index", "Номер студента");
            dataGridViewStudents.Columns.Add("name", "Имя студента");
            dataGridViewStudents.Columns.Add("speciality", "Специальность");
            dataGridViewStudents.Columns.Add("group", "Группа");
        }
        public void reload_grid()
        {
            dataGridViewStudents.Rows.Clear();
            List<(int, string, string, string)> students = logic.GetStudents();
            for (int i = 0; i < students.Count(); i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewStudents);
                row.Cells[0].Value = students[i].Item1;
                row.Cells[1].Value = students[i].Item2;
                row.Cells[2].Value = students[i].Item3;
                row.Cells[3].Value = students[i].Item4;
                dataGridViewStudents.Rows.Add(row);
            }
        }
        public void reload_gistogram()
        {
            chartStudents.Series.Clear();
            chartStudents.ChartAreas.Clear();

            ChartArea area = new ChartArea("Default");
            chartStudents.ChartAreas.Add(area);

            Series series = new Series("Специальности");
            series.ChartType = SeriesChartType.Column;
            chartStudents.Series.Add(series);

            series.Points.Clear();
            foreach (var speciality in logic.GetGistogram())
            {
                series.Points.AddXY(speciality.Key, speciality.Value);
            }
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            init_grid();
            reload_grid();
            reload_gistogram();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAddStudent formAddStudent = new FormAddStudent();
            formAddStudent.init(this);
            formAddStudent.Show();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (logic.GetStudents().Count() < 1)
            {
                MessageBox.Show("Список студентов пуст!", "Ошибка");
                return;
            }
            var rows = dataGridViewStudents.SelectedRows;
            List<int> indexes = new List<int>();
            for (int i = 0; i < rows.Count; i++)
            {
                indexes.Add(Convert.ToInt32(rows[i].Cells[0].Value));
            }
            if (indexes.Count < 1)
            {
                MessageBox.Show("Необходимо выбрать строку(и)!", "Ошибка");
                return;
            }
            List<bool> deleted = indexes.Select(i => logic.DeleteStudent(i)).ToList();
            reload_grid();
            reload_gistogram();
            if (!deleted.All(i => i))
            {
                MessageBox.Show("Некоторых из выбранных студентов не существует!", "Ошибка");
                return;
            }
            MessageBox.Show("Студент(ы) успешно удален!", "Успех");
        }
    }
}
