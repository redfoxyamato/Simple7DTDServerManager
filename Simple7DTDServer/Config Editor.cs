using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Simple7DTDServer
{
    public partial class Config_Editor : Form
    {
        string lang,lang_path,conf_path;
        List<Information> infos = new List<Information>();
        List<Setting> settings = new List<Setting>();

        public Config_Editor(string language, string language_path, string config_path)
        {
            InitializeComponent();
            lang = language;
            lang_path = language_path;
            conf_path = config_path;
            parseSettingFromFile();
        }

        #region Form_Events

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < 0)
            {
                return;
            }
            parseFromFile();
            removeDuplicate();

            for (int i = 0; i < settings.Count; i++)
            {
                if (settings[i].id.Trim() == "")
                {
                    settings.RemoveAt(i);
                }
            }
            for (int i = 0; i < infos.Count; i++)
            {
                if (infos[i].id.Trim() == "")
                {
                    infos.RemoveAt(i);
                }
            }
            if(settings.Count != infos.Count)
            {
                listBox1.Items.Clear();
                settings = new List<Setting>();
                infos = new List<Information>();
                MessageBox.Show(Form1.translateTo("invalid_cfg"));
                listBox1.SelectedIndex = -1;
            }
        }

        private void Config_Editor_Load(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(lang_path, string.Format("*_{0}.xml",lang));
            foreach(string file in files)
            {
                comboBox1.Items.Add(Path.GetFileName(file).Split(new char[] { '_' })[0]);
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeDuplicate();
            comboBox2.Items.Clear();
            int index = listBox1.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            description.Text = infos[index].desc.Replace("\\n", "\r\n");
            Type type = infos[index].type;
            string value = settings[index].value;
            Reset();
            applyType(listBox1.Items[index].ToString(), type, value);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Text = checkBox1.Checked.ToString().ToLower();
            settings[listBox1.SelectedIndex].value = checkBox1.Text;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void apply_Click(object sender, EventArgs e)
        {
            string str = string.Format("<?xml version={0}1.0{0}?>\r\n\t<ServerSettings>\n", "\"");
            for(int i = 0;i < settings.Count - 1;i++)
            {
                Setting set = settings[i];
                Information info = infos[i];
                str += string.Format("\t\t<property name={0}{1}{0} value={0}{2}{0} /> <!-- {3} -->\n","\"",set.id,set.value,getOriginalDesc(set.id).Replace("\\n"," "));
            }
            {
                int index = settings.Count - 1;
                if(settings[index].value != "")
                {
                    str += string.Format("\t\t<property name={0}SaveGameFolder{0} value={0}{1}{0} /> <!-- {2} -->\n", "\"", settings[index].value, getOriginalDesc("SaveGameFolder").Replace("\\n", " "));
                }
                else
                {
                    str += string.Format("\t\t<!--property name={0}SaveGameFolder{0}      value={0}absolute path{0} /-->	<!-- {1} -->\n","\"",getOriginalDesc("SaveGameFolder").Replace("\\n", " "));
                }
            }
            str += "</ServerSettings>";
            File.WriteAllText(conf_path,str);
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (infos[listBox1.SelectedIndex].type == Type.STRING)
            {
                settings[listBox1.SelectedIndex].value = textBox1.Text;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (infos[listBox1.SelectedIndex].type == Type.SELECT)
            {
                settings[listBox1.SelectedIndex].value = comboBox2.SelectedItem.ToString();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (infos[listBox1.SelectedIndex].type == Type.INT)
            {
                settings[listBox1.SelectedIndex].value = numericUpDown1.Value.ToString();
            }
        }
        #endregion

        #region config

        private string getOriginalDesc(string name)
        {
            XmlTextReader reader = null;
            try
            {
                string p = lang_path + string.Format("{0}_{1}.xml", comboBox1.SelectedItem, "English");
                reader = new XmlTextReader(new StreamReader(p, Encoding.UTF8));
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.MoveToFirstAttribute())
                        {
                            string id = reader.Value;
                            if (name != id)
                            {
                                continue;
                            }
                            if (reader.MoveToNextAttribute())
                            {
                                Type type = getType(reader.Value);
                                if (type == Type.UNKNOWN)
                                {
                                    continue;
                                }
                                if (reader.MoveToNextAttribute())
                                {
                                    return reader.Value;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return "";
        }
        private void parseFromFile()
        {
            XmlTextReader reader = null;
            try
            {
                string p = lang_path + string.Format("{0}_{1}.xml", comboBox1.SelectedItem, lang);
                reader = new XmlTextReader(new StreamReader(p, Encoding.UTF8));
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.MoveToFirstAttribute())
                        {
                            string id = reader.Value;
                            if (reader.MoveToNextAttribute())
                            {
                                Type type = getType(reader.Value);
                                if (type == Type.UNKNOWN)
                                {
                                    continue;
                                }
                                if (reader.MoveToNextAttribute())
                                {
                                    infos.Add(new Information(id, type, reader.Value));
                                }
                            }
                        }
                    }
                }
                if (!isThereSavePath())
                {
                    infos.Add(new Information("SaveGameFolder", Type.STRING, ""));
                }
                listBox1.Items.Clear();
                for (int i = 0; i < infos.Count; i++)
                {
                    listBox1.Items.Add(infos[i].id);
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        private Limit getLimit(string id)
        {
            XmlTextReader reader = null;
            try
            {
                string p = lang_path + "limits.xml";
                reader = new XmlTextReader(new StreamReader(p, Encoding.UTF8));
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.MoveToFirstAttribute())
                        {
                            if (reader.Value == id && reader.MoveToNextAttribute())
                            {
                                string low = reader.Value;
                                if (reader.MoveToNextAttribute())
                                {
                                    return new Limit(int.Parse(low), int.Parse(reader.Value));
                                }
                            }
                        }
                    }
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return null;
        }
        private void parseSettingFromFile()
        {
            string p = conf_path;
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(new StreamReader(p, Encoding.UTF8));
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.MoveToFirstAttribute())
                        {
                            string id = reader.Value;
                            if (reader.MoveToNextAttribute())
                            {
                                settings.Add(new Setting(id, reader.Value));
                            }
                        }
                    }
                }
                if (!isThereSavePath())
                {
                    settings.Add(new Setting("SaveGameFolder", ""));
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        private string[] parseSelectiveFromFile(string name)
        {
            XmlTextReader reader = null;
            try
            {
                string p = lang_path + "selection.xml";
                reader = new XmlTextReader(new StreamReader(p, Encoding.UTF8));
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.MoveToFirstAttribute())
                        {
                            string id = reader.Value;
                            if (id == name && reader.MoveToNextAttribute())
                            {
                                return reader.Value.Split(",".ToCharArray());
                            }
                        }
                    }
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return new string[] { };
        }
        #endregion

        #region util

        private bool isBoolean(string str)
        {
            return str == "true" || str == "false";
        }
        private void selectFromName(ComboBox box, string name)
        {
            if (!box.Items.Contains(name))
            {
                return;
            }
            for (int i = 0; i < box.Items.Count; i++)
            {
                if (box.Items[i].ToString() == name)
                {
                    box.SelectedIndex = i;
                }
            }
        }
        private void applyType(string id, Type type, string value)
        {
            checkBox1.Enabled = false;
            textBox1.Enabled = false;
            numericUpDown1.Enabled = false;
            comboBox2.Enabled = false;
            switch (type)
            {
                case Type.INT:
                    {
                        numericUpDown1.Enabled = true;
                        Limit l = getLimit(id);
                        if (l != null)
                        {
                            numericUpDown1.Minimum = l.lower;
                            numericUpDown1.Maximum = l.upper;
                        }
                        int i = int.Parse(value);
                        if (i > numericUpDown1.Maximum)
                        {
                            i = (int)numericUpDown1.Maximum;
                        }
                        else if (i < numericUpDown1.Minimum)
                        {
                            i = (int)numericUpDown1.Minimum;
                        }
                        numericUpDown1.Value = i;
                        return;
                    }
                case Type.BOOL:
                    {
                        checkBox1.Enabled = true;
                        if (isBoolean(value))
                        {
                            checkBox1.Checked = bool.Parse(value);
                        }
                        return;
                    }
                case Type.STRING:
                    {
                        textBox1.Enabled = true;
                        textBox1.Text = value;
                        return;
                    }
                case Type.SELECT:
                    {
                        comboBox2.Enabled = true;
                        comboBox2.Items.AddRange(parseSelectiveFromFile(id));
                        selectFromName(comboBox2, value);
                        return;
                    }
                default: return;
            }
        }
        private Type getType(string str)
        {
            switch (str)
            {
                case "int": return Type.INT;
                case "bool": return Type.BOOL;
                case "string": return Type.STRING;
                case "select": return Type.SELECT;
                default: return Type.UNKNOWN;
            }
        }
        private void removeDuplicate()
        {
            for (int i = settings.Count; i < listBox1.Items.Count; i++)
            {
                listBox1.Items.RemoveAt(i);
            }
        }

        private bool isThereSavePath()
        {
            Setting set = getSettingFromID("SaveGameFolder");
            return set != null && set.value != "";
        }
        private Setting getSettingFromID(string id)
        {
            foreach (Setting set in settings)
            {
                if (set.id == id)
                {
                    return set;
                }
            }
            return null;
        }
        private void Reset()
        {
            textBox1.Text = "";
            checkBox1.Checked = false;
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 1000;
            numericUpDown1.Value = 0;
        }
        #endregion

    }
    enum Type
    {
        INT,
        BOOL,
        STRING,
        SELECT,
        UNKNOWN
    };
    class Information
    {
        public string id, desc;
        public Type type;

        public Information(string id, Type type, string desc)
        {
            this.type = type;
            this.id = id;
            this.desc = desc;
        }
    }
    class Setting
    {
        public string id, value;
        public Setting(string id, string value)
        {
            this.id = id;
            this.value = value;
        }
    }
    class Limit
    {
        public int lower, upper;
        public Limit(int low,int up)
        {
            lower = low;
            upper = up;
        }
    }
}
