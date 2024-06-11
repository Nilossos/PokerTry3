using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shaski_Bakhmut
{
    public partial class SetupForm : Form
    {
        public string Player1Name { get; private set; }
        public string Player2Name { get; private set; }
        public SideType Player1Color { get; private set; }
        public SideType Player2Color { get; private set; }
        public bool RandomColors { get; private set; }

        public SetupForm()
        {
            InitializeComponent();
            colorComboBox1.SelectedItem = "Белые";
            colorComboBox2.SelectedItem = "Черные";
            colorComboBox1.SelectedIndexChanged += new EventHandler(ColorComboBox1_SelectedIndexChanged);
            randomColorsCheckBox.CheckedChanged += new EventHandler(RandomColorsCheckBox_CheckedChanged);
        }

        private void ColorComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (colorComboBox1.SelectedItem.ToString() == "Белые")
            {
                colorComboBox2.SelectedItem = "Черные";
            }
            else
            {
                colorComboBox2.SelectedItem = "Белые";
            }
        }

        private void RandomColorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = randomColorsCheckBox.Checked;
            colorComboBox1.Enabled = !isChecked;

            if (isChecked)
            {
                colorComboBox1.SelectedItem = "Белые";
                colorComboBox2.SelectedItem = "Черные";
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Player1Name = player1TextBox.Text;
            Player2Name = player2TextBox.Text;
            RandomColors = randomColorsCheckBox.Checked;

            if (!RandomColors)
            {
                Player1Color = colorComboBox1.SelectedItem.ToString() == "Белые" ? SideType.White : SideType.Black;
                Player2Color = colorComboBox2.SelectedItem.ToString() == "Белые" ? SideType.White : SideType.Black;
            }
            else
            {
                Random rnd = new Random();
                if (rnd.Next(2) == 0)
                {
                    Player1Color = SideType.White;
                    Player2Color = SideType.Black;
                }
                else
                {
                    Player1Color = SideType.Black;
                    Player2Color = SideType.White;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
