using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple7DTDServer
{
    public partial class LanguageSelector : Form
    {
        Form1 form;
        public LanguageSelector(Form1 mainForm,string currentLang)
        {
            InitializeComponent();
            form = mainForm;
            for(int i = 0;i < comboBox1.Items.Count;i++)
            {
                if(comboBox1.Items[i].ToString() == currentLang)
                {
                    comboBox1.SelectedIndex = i;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.setLanguage(comboBox1.SelectedItem.ToString());
            Close();
        }
    }
}
