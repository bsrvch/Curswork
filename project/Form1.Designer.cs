
namespace project
{
    partial class Sign
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxPas = new System.Windows.Forms.TextBox();
            this.sin = new System.Windows.Forms.Button();
            this.reg = new System.Windows.Forms.Button();
            this.Exep = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя пользователя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пароль";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(359, 121);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 22);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxPas
            // 
            this.textBoxPas.Location = new System.Drawing.Point(359, 162);
            this.textBoxPas.Name = "textBoxPas";
            this.textBoxPas.PasswordChar = '*';
            this.textBoxPas.Size = new System.Drawing.Size(100, 22);
            this.textBoxPas.TabIndex = 4;
            this.textBoxPas.TextChanged += new System.EventHandler(this.textBoxPas_TextChanged);
            // 
            // sin
            // 
            this.sin.Location = new System.Drawing.Point(241, 216);
            this.sin.Name = "sin";
            this.sin.Size = new System.Drawing.Size(75, 23);
            this.sin.TabIndex = 5;
            this.sin.Text = "Вход";
            this.sin.UseVisualStyleBackColor = true;
            this.sin.Click += new System.EventHandler(this.button1_Click);
            // 
            // reg
            // 
            this.reg.Location = new System.Drawing.Point(359, 216);
            this.reg.Name = "reg";
            this.reg.Size = new System.Drawing.Size(100, 23);
            this.reg.TabIndex = 6;
            this.reg.Text = "Регистрация";
            this.reg.UseVisualStyleBackColor = true;
            this.reg.Click += new System.EventHandler(this.reg_Click);
            // 
            // Exep
            // 
            this.Exep.AutoSize = true;
            this.Exep.Location = new System.Drawing.Point(337, 252);
            this.Exep.Name = "Exep";
            this.Exep.Size = new System.Drawing.Size(0, 17);
            this.Exep.TabIndex = 7;
            this.Exep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(480, 124);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(0, 17);
            this.labelName.TabIndex = 8;
            // 
            // labelPas
            // 
            this.labelPas.AutoSize = true;
            this.labelPas.Location = new System.Drawing.Point(480, 165);
            this.labelPas.Name = "labelPas";
            this.labelPas.Size = new System.Drawing.Size(0, 17);
            this.labelPas.TabIndex = 9;
            // 
            // Sign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelPas);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.Exep);
            this.Controls.Add(this.reg);
            this.Controls.Add(this.sin);
            this.Controls.Add(this.textBoxPas);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Sign";
            this.Text = "Регистрация/Вход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxPas;
        private System.Windows.Forms.Button sin;
        private System.Windows.Forms.Button reg;
        private System.Windows.Forms.Label Exep;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPas;
    }
}

