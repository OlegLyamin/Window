namespace GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Build_window = new System.Windows.Forms.Button();
            this.Section = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Close = new System.Windows.Forms.Button();
            this.Right = new System.Windows.Forms.RadioButton();
            this.Left = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Weight = new System.Windows.Forms.NumericUpDown();
            this.Width = new System.Windows.Forms.NumericUpDown();
            this.Height = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.OpenSection = new System.Windows.Forms.ComboBox();
            this.FastVariable = new System.Windows.Forms.Button();
            this.test100 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Weight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Height)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Build_window
            // 
            this.Build_window.Location = new System.Drawing.Point(181, 143);
            this.Build_window.Name = "Build_window";
            this.Build_window.Size = new System.Drawing.Size(70, 28);
            this.Build_window.TabIndex = 0;
            this.Build_window.Text = "Построить";
            this.Build_window.UseVisualStyleBackColor = true;
            this.Build_window.Click += new System.EventHandler(this.Build_window_Click);
            // 
            // Section
            // 
            this.Section.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Section.FormattingEnabled = true;
            this.Section.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.Section.Location = new System.Drawing.Point(123, 6);
            this.Section.Name = "Section";
            this.Section.Size = new System.Drawing.Size(35, 21);
            this.Section.TabIndex = 1;
            this.Section.SelectedIndexChanged += new System.EventHandler(this.Section_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Количество секций";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Высота";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ширина";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Толщина";
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(265, 143);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(70, 28);
            this.Close.TabIndex = 12;
            this.Close.Text = "Закрыть";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Right
            // 
            this.Right.AutoSize = true;
            this.Right.Checked = true;
            this.Right.Location = new System.Drawing.Point(6, 32);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(65, 17);
            this.Right.TabIndex = 19;
            this.Right.TabStop = true;
            this.Right.Text = "Справа ";
            this.Right.UseVisualStyleBackColor = true;
            // 
            // Left
            // 
            this.Left.AutoSize = true;
            this.Left.Location = new System.Drawing.Point(84, 33);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(56, 17);
            this.Left.TabIndex = 20;
            this.Left.Text = "Слева";
            this.Left.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Weight);
            this.groupBox1.Controls.Add(this.Width);
            this.groupBox1.Controls.Add(this.Height);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(15, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 129);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Размеры окна";
            // 
            // Weight
            // 
            this.Weight.Location = new System.Drawing.Point(57, 100);
            this.Weight.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.Weight.Minimum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.Weight.Name = "Weight";
            this.Weight.Size = new System.Drawing.Size(76, 20);
            this.Weight.TabIndex = 10;
            this.Weight.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // Width
            // 
            this.Width.Location = new System.Drawing.Point(57, 66);
            this.Width.Maximum = new decimal(new int[] {
            2670,
            0,
            0,
            0});
            this.Width.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(76, 20);
            this.Width.TabIndex = 9;
            this.Width.Value = new decimal(new int[] {
            570,
            0,
            0,
            0});
            // 
            // Height
            // 
            this.Height.Location = new System.Drawing.Point(57, 30);
            this.Height.Maximum = new decimal(new int[] {
            2760,
            0,
            0,
            0});
            this.Height.Minimum = new decimal(new int[] {
            580,
            0,
            0,
            0});
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(76, 20);
            this.Height.TabIndex = 8;
            this.Height.Value = new decimal(new int[] {
            580,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Right);
            this.groupBox2.Controls.Add(this.Left);
            this.groupBox2.Location = new System.Drawing.Point(189, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 70);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Расположение ручки";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Открывающаяся секция";
            // 
            // OpenSection
            // 
            this.OpenSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OpenSection.FormattingEnabled = true;
            this.OpenSection.Location = new System.Drawing.Point(301, 6);
            this.OpenSection.Name = "OpenSection";
            this.OpenSection.Size = new System.Drawing.Size(34, 21);
            this.OpenSection.TabIndex = 25;
            this.OpenSection.SelectedIndexChanged += new System.EventHandler(this.OpenSection_SelectedIndexChanged);
            // 
            // FastVariable
            // 
            this.FastVariable.Location = new System.Drawing.Point(215, 118);
            this.FastVariable.Name = "FastVariable";
            this.FastVariable.Size = new System.Drawing.Size(36, 19);
            this.FastVariable.TabIndex = 26;
            this.FastVariable.Text = "Fast";
            this.FastVariable.UseVisualStyleBackColor = true;
            this.FastVariable.Click += new System.EventHandler(this.FsatVariable_Click);
            // 
            // test100
            // 
            this.test100.Location = new System.Drawing.Point(265, 118);
            this.test100.Name = "test100";
            this.test100.Size = new System.Drawing.Size(39, 19);
            this.test100.TabIndex = 27;
            this.test100.Text = "Test100";
            this.test100.UseVisualStyleBackColor = true;
            this.test100.Click += new System.EventHandler(this.test100_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 179);
            this.Controls.Add(this.test100);
            this.Controls.Add(this.FastVariable);
            this.Controls.Add(this.OpenSection);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Section);
            this.Controls.Add(this.Build_window);
            this.Name = "MainForm";
            this.Text = "Пластиковое окно";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Weight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Height)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Build_window;
        private System.Windows.Forms.ComboBox Section;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.RadioButton Right;
        private System.Windows.Forms.RadioButton Left;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown Weight;
        private System.Windows.Forms.NumericUpDown Width;
        private System.Windows.Forms.NumericUpDown Height;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox OpenSection;
        private System.Windows.Forms.Button FastVariable;
        private System.Windows.Forms.Button test100;
    }
}

