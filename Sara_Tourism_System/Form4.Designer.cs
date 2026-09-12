namespace Sara_Tourism_System
{
    partial class Booking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPay = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdb12h30 = new System.Windows.Forms.RadioButton();
            this.rdb13 = new System.Windows.Forms.RadioButton();
            this.rdb13h30 = new System.Windows.Forms.RadioButton();
            this.rdb10h30 = new System.Windows.Forms.RadioButton();
            this.rdb11 = new System.Windows.Forms.RadioButton();
            this.rdb11h30 = new System.Windows.Forms.RadioButton();
            this.rdb12 = new System.Windows.Forms.RadioButton();
            this.rdb10 = new System.Windows.Forms.RadioButton();
            this.dateBooking = new System.Windows.Forms.DateTimePicker();
            this.cbxActivity = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnPay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdb12h30);
            this.groupBox1.Controls.Add(this.rdb13);
            this.groupBox1.Controls.Add(this.rdb13h30);
            this.groupBox1.Controls.Add(this.rdb10h30);
            this.groupBox1.Controls.Add(this.rdb11);
            this.groupBox1.Controls.Add(this.rdb11h30);
            this.groupBox1.Controls.Add(this.rdb12);
            this.groupBox1.Controls.Add(this.rdb10);
            this.groupBox1.Controls.Add(this.dateBooking);
            this.groupBox1.Controls.Add(this.cbxActivity);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(305, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 886);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create a Booking:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 701);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 28);
            this.label4.TabIndex = 16;
            this.label4.Text = "Enter Email Address:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(36, 742);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(314, 34);
            this.txtEmail.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 28);
            this.label3.TabIndex = 14;
            this.label3.Text = "Select a date for booking:";
            // 
            // btnPay
            // 
            this.btnPay.FlatAppearance.BorderSize = 2;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.Location = new System.Drawing.Point(86, 822);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(281, 48);
            this.btnPay.TabIndex = 13;
            this.btnPay.Text = "Pay for Booking";
            this.btnPay.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 633);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 28);
            this.label2.TabIndex = 12;
            this.label2.Text = "Number of People:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(222, 633);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(104, 34);
            this.txtNumber.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "Select from the available time slots:";
            // 
            // rdb12h30
            // 
            this.rdb12h30.AutoSize = true;
            this.rdb12h30.Location = new System.Drawing.Point(242, 412);
            this.rdb12h30.Name = "rdb12h30";
            this.rdb12h30.Size = new System.Drawing.Size(79, 32);
            this.rdb12h30.TabIndex = 9;
            this.rdb12h30.TabStop = true;
            this.rdb12h30.Text = "12:30";
            this.rdb12h30.UseVisualStyleBackColor = true;
            // 
            // rdb13
            // 
            this.rdb13.AutoSize = true;
            this.rdb13.Location = new System.Drawing.Point(242, 472);
            this.rdb13.Name = "rdb13";
            this.rdb13.Size = new System.Drawing.Size(79, 32);
            this.rdb13.TabIndex = 8;
            this.rdb13.TabStop = true;
            this.rdb13.Text = "13:00";
            this.rdb13.UseVisualStyleBackColor = true;
            // 
            // rdb13h30
            // 
            this.rdb13h30.AutoSize = true;
            this.rdb13h30.Location = new System.Drawing.Point(242, 526);
            this.rdb13h30.Name = "rdb13h30";
            this.rdb13h30.Size = new System.Drawing.Size(79, 32);
            this.rdb13h30.TabIndex = 7;
            this.rdb13h30.TabStop = true;
            this.rdb13h30.Text = "13:30";
            this.rdb13h30.UseVisualStyleBackColor = true;
            // 
            // rdb10h30
            // 
            this.rdb10h30.AutoSize = true;
            this.rdb10h30.Location = new System.Drawing.Point(35, 412);
            this.rdb10h30.Name = "rdb10h30";
            this.rdb10h30.Size = new System.Drawing.Size(79, 32);
            this.rdb10h30.TabIndex = 6;
            this.rdb10h30.TabStop = true;
            this.rdb10h30.Text = "10:30";
            this.rdb10h30.UseVisualStyleBackColor = true;
            // 
            // rdb11
            // 
            this.rdb11.AutoSize = true;
            this.rdb11.Location = new System.Drawing.Point(35, 472);
            this.rdb11.Name = "rdb11";
            this.rdb11.Size = new System.Drawing.Size(76, 32);
            this.rdb11.TabIndex = 5;
            this.rdb11.TabStop = true;
            this.rdb11.Text = "11:00";
            this.rdb11.UseVisualStyleBackColor = true;
            // 
            // rdb11h30
            // 
            this.rdb11h30.AutoSize = true;
            this.rdb11h30.Location = new System.Drawing.Point(36, 526);
            this.rdb11h30.Name = "rdb11h30";
            this.rdb11h30.Size = new System.Drawing.Size(76, 32);
            this.rdb11h30.TabIndex = 4;
            this.rdb11h30.TabStop = true;
            this.rdb11h30.Text = "11:30";
            this.rdb11h30.UseVisualStyleBackColor = true;
            // 
            // rdb12
            // 
            this.rdb12.AutoSize = true;
            this.rdb12.Location = new System.Drawing.Point(242, 354);
            this.rdb12.Name = "rdb12";
            this.rdb12.Size = new System.Drawing.Size(79, 32);
            this.rdb12.TabIndex = 3;
            this.rdb12.TabStop = true;
            this.rdb12.Text = "12:00";
            this.rdb12.UseVisualStyleBackColor = true;
            // 
            // rdb10
            // 
            this.rdb10.AutoSize = true;
            this.rdb10.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rdb10.FlatAppearance.BorderSize = 2;
            this.rdb10.Location = new System.Drawing.Point(35, 354);
            this.rdb10.Name = "rdb10";
            this.rdb10.Size = new System.Drawing.Size(79, 32);
            this.rdb10.TabIndex = 2;
            this.rdb10.TabStop = true;
            this.rdb10.Text = "10:00";
            this.rdb10.UseVisualStyleBackColor = true;
            // 
            // dateBooking
            // 
            this.dateBooking.CalendarMonthBackground = System.Drawing.Color.LightSlateGray;
            this.dateBooking.CalendarTitleBackColor = System.Drawing.Color.LightSlateGray;
            this.dateBooking.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateBooking.Location = new System.Drawing.Point(35, 220);
            this.dateBooking.Name = "dateBooking";
            this.dateBooking.Size = new System.Drawing.Size(299, 34);
            this.dateBooking.TabIndex = 1;
            // 
            // cbxActivity
            // 
            this.cbxActivity.BackColor = System.Drawing.Color.LightSlateGray;
            this.cbxActivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxActivity.ForeColor = System.Drawing.Color.Black;
            this.cbxActivity.FormattingEnabled = true;
            this.cbxActivity.Location = new System.Drawing.Point(35, 108);
            this.cbxActivity.Name = "cbxActivity";
            this.cbxActivity.Size = new System.Drawing.Size(299, 36);
            this.cbxActivity.TabIndex = 0;
            this.cbxActivity.Text = "Select an Activity:";
            // 
            // Booking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BackgroundImage = global::Sara_Tourism_System.Properties.Resources.Bookings_Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1127, 927);
            this.Controls.Add(this.groupBox1);
            this.Name = "Booking";
            this.Text = "Create a Booking";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxActivity;
        private System.Windows.Forms.RadioButton rdb10;
        private System.Windows.Forms.DateTimePicker dateBooking;
        private System.Windows.Forms.RadioButton rdb12h30;
        private System.Windows.Forms.RadioButton rdb13;
        private System.Windows.Forms.RadioButton rdb13h30;
        private System.Windows.Forms.RadioButton rdb10h30;
        private System.Windows.Forms.RadioButton rdb11;
        private System.Windows.Forms.RadioButton rdb11h30;
        private System.Windows.Forms.RadioButton rdb12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
    }
}