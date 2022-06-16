using System;
using System.Windows.Forms;

namespace ScreenSum10
{
    public partial class Form1 : Form
    {
        //инициализация компонента
        public Form1() => InitializeComponent();
        //событие изменения размеров компонента
        private void myClassSum101_SizeChanged(object sender, EventArgs e) => myClassSum101.ChangeSize(e);
        //событие на нажатие
        private void myClassSum101_Click(object sender, EventArgs e)
        {
            myClassSum101.onClickListener(MousePosition);
            label1.Text = $"Sum: {myClassSum101.TotalSum}";
        }
        //нажатие на кнопку
        private void button1_Click(object sender, EventArgs e)
        {
            myClassSum101.updateArray();
            Refresh();
            label1.Text = $"Sum: {myClassSum101.TotalSum}";
        }
    }
}
