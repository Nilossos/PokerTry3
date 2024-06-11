using System.Drawing;
using System.Windows.Forms;
using System;

namespace Shaski_Bakhmut
{
    partial class SetupForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label player1Label;
        private TextBox player1TextBox;
        private Label player2Label;
        private TextBox player2TextBox;
        private Label colorLabel;
        private ComboBox colorComboBox1;
        private ComboBox colorComboBox2;
        private CheckBox randomColorsCheckBox;
        private Button startButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.player1Label = new System.Windows.Forms.Label();
            this.player1TextBox = new System.Windows.Forms.TextBox();
            this.player2Label = new System.Windows.Forms.Label();
            this.player2TextBox = new System.Windows.Forms.TextBox();
            this.colorLabel = new System.Windows.Forms.Label();
            this.colorComboBox1 = new System.Windows.Forms.ComboBox();
            this.colorComboBox2 = new System.Windows.Forms.ComboBox();
            this.randomColorsCheckBox = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // player1Label
            // 
            this.player1Label.AutoSize = true;
            this.player1Label.Location = new System.Drawing.Point(12, 11);
            this.player1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(141, 16);
            this.player1Label.TabIndex = 0;
            this.player1Label.Text = "Имя первого игрока:";
            // 
            // player1TextBox
            // 
            this.player1TextBox.Location = new System.Drawing.Point(187, 11);
            this.player1TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.player1TextBox.Name = "player1TextBox";
            this.player1TextBox.Size = new System.Drawing.Size(171, 22);
            this.player1TextBox.TabIndex = 1;
            // 
            // player2Label
            // 
            this.player2Label.AutoSize = true;
            this.player2Label.Location = new System.Drawing.Point(12, 43);
            this.player2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(140, 16);
            this.player2Label.TabIndex = 2;
            this.player2Label.Text = "Имя второго игрока:";
            // 
            // player2TextBox
            // 
            this.player2TextBox.Location = new System.Drawing.Point(187, 43);
            this.player2TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.player2TextBox.Name = "player2TextBox";
            this.player2TextBox.Size = new System.Drawing.Size(171, 22);
            this.player2TextBox.TabIndex = 3;
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(12, 75);
            this.colorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(117, 16);
            this.colorLabel.TabIndex = 4;
            this.colorLabel.Text = "Выберите цвета:";
            this.colorLabel.Visible = false;
            // 
            // colorComboBox1
            // 
            this.colorComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorComboBox1.Items.AddRange(new object[] {
            "Белые",
            "Черные"});
            this.colorComboBox1.Location = new System.Drawing.Point(187, 75);
            this.colorComboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.colorComboBox1.Name = "colorComboBox1";
            this.colorComboBox1.Size = new System.Drawing.Size(171, 24);
            this.colorComboBox1.TabIndex = 5;
            this.colorComboBox1.Visible = false;
            // 
            // colorComboBox2
            // 
            this.colorComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorComboBox2.Enabled = false;
            this.colorComboBox2.Items.AddRange(new object[] {
            "Черные",
            "Белые"});
            this.colorComboBox2.Location = new System.Drawing.Point(187, 107);
            this.colorComboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.colorComboBox2.Name = "colorComboBox2";
            this.colorComboBox2.Size = new System.Drawing.Size(171, 24);
            this.colorComboBox2.TabIndex = 6;
            this.colorComboBox2.Visible = false;
            // 
            // randomColorsCheckBox
            // 
            this.randomColorsCheckBox.AutoSize = true;
            this.randomColorsCheckBox.Location = new System.Drawing.Point(12, 139);
            this.randomColorsCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.randomColorsCheckBox.Name = "randomColorsCheckBox";
            this.randomColorsCheckBox.Size = new System.Drawing.Size(145, 20);
            this.randomColorsCheckBox.TabIndex = 7;
            this.randomColorsCheckBox.Text = "Случайные цвета";
            this.randomColorsCheckBox.Visible = false;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(133, 191);
            this.startButton.Margin = new System.Windows.Forms.Padding(4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(115, 32);
            this.startButton.TabIndex = 8;
            this.startButton.Text = "Начать игру";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 238);
            this.Controls.Add(this.player1Label);
            this.Controls.Add(this.player1TextBox);
            this.Controls.Add(this.player2Label);
            this.Controls.Add(this.player2TextBox);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.colorComboBox1);
            this.Controls.Add(this.colorComboBox2);
            this.Controls.Add(this.randomColorsCheckBox);
            this.Controls.Add(this.startButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SetupForm";
            this.Text = "Настройка игры";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
