using BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewWinForms
{
    public partial class FormAddStudent : Form
    {
        FormMain formMain;
        public FormAddStudent()
        {
            InitializeComponent();
        }
        public void init(FormMain formMain)
        {
            this.formMain = formMain;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string speciality = textBoxSpeciality.Text;
            string group = textBoxGroup.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(speciality) || string.IsNullOrEmpty(group))
            {
                MessageBox.Show("Поля не могут быть пустыми!", "Ошибка");
                return;
            }
            formMain.logic.AddStudent(name, speciality, group);
            formMain.reload_grid();
            formMain.reload_gistogram();
            textBoxName.Text = "";
            textBoxSpeciality.Text = "";
            textBoxGroup.Text = "";
            MessageBox.Show("Студент успешно добавлен!", "Успех");
        }
    }
}
