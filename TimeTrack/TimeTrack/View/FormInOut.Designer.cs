namespace TimeTrack.View
{
    partial class FormInOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInOut));
            this.lblLunesViernes = new System.Windows.Forms.Label();
            this.panelHora = new System.Windows.Forms.Panel();
            this.panelDateHour = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblHour = new System.Windows.Forms.Label();
            this.lblSabado = new System.Windows.Forms.Label();
            this.btnIn = new System.Windows.Forms.Button();
            this.panelInOut = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOut = new System.Windows.Forms.Button();
            this.panelHour = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panelHora.SuspendLayout();
            this.panelDateHour.SuspendLayout();
            this.panelInOut.SuspendLayout();
            this.panelHour.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLunesViernes
            // 
            this.lblLunesViernes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLunesViernes.AutoSize = true;
            this.lblLunesViernes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLunesViernes.Location = new System.Drawing.Point(13, 150);
            this.lblLunesViernes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLunesViernes.Name = "lblLunesViernes";
            this.lblLunesViernes.Size = new System.Drawing.Size(318, 110);
            this.lblLunesViernes.TabIndex = 0;
            this.lblLunesViernes.Text = "Horario del empleado Lunes a viernes:\r\n\r\nEntrada: 7:00 am\r\n\r\nSalida: 5:00 pm";
            // 
            // panelHora
            // 
            this.panelHora.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelHora.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelHora.Controls.Add(this.lblLunesViernes);
            this.panelHora.Controls.Add(this.panelDateHour);
            this.panelHora.Controls.Add(this.lblSabado);
            this.panelHora.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelHora.Location = new System.Drawing.Point(29, 67);
            this.panelHora.Margin = new System.Windows.Forms.Padding(4);
            this.panelHora.Name = "panelHora";
            this.panelHora.Size = new System.Drawing.Size(364, 435);
            this.panelHora.TabIndex = 1;
            // 
            // panelDateHour
            // 
            this.panelDateHour.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelDateHour.Controls.Add(this.lblDate);
            this.panelDateHour.Controls.Add(this.lblHour);
            this.panelDateHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelDateHour.Location = new System.Drawing.Point(16, 17);
            this.panelDateHour.Margin = new System.Windows.Forms.Padding(4);
            this.panelDateHour.Name = "panelDateHour";
            this.panelDateHour.Size = new System.Drawing.Size(332, 123);
            this.panelDateHour.TabIndex = 2;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(19, 23);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(74, 22);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Fechas:";
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Location = new System.Drawing.Point(19, 77);
            this.lblHour.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(109, 22);
            this.lblHour.TabIndex = 0;
            this.lblHour.Text = "Hora Actual:";
            // 
            // lblSabado
            // 
            this.lblSabado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSabado.AutoSize = true;
            this.lblSabado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSabado.Location = new System.Drawing.Point(13, 320);
            this.lblSabado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSabado.Name = "lblSabado";
            this.lblSabado.Size = new System.Drawing.Size(250, 110);
            this.lblSabado.TabIndex = 1;
            this.lblSabado.Text = "Horario del empleado sábado:\r\n\r\nEntrada: 8:00 am\r\n\r\nSalida: 12:00 pm\r\n";
            // 
            // btnIn
            // 
            this.btnIn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(60)))), ((int)(((byte)(190)))));
            this.btnIn.Location = new System.Drawing.Point(50, 89);
            this.btnIn.Margin = new System.Windows.Forms.Padding(4);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(131, 108);
            this.btnIn.TabIndex = 2;
            this.btnIn.Text = "📥\r\n";
            this.btnIn.UseVisualStyleBackColor = true;
            // 
            // panelInOut
            // 
            this.panelInOut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelInOut.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelInOut.Controls.Add(this.label7);
            this.panelInOut.Controls.Add(this.label6);
            this.panelInOut.Controls.Add(this.btnOut);
            this.panelInOut.Controls.Add(this.panelHour);
            this.panelInOut.Controls.Add(this.btnIn);
            this.panelInOut.Location = new System.Drawing.Point(470, 147);
            this.panelInOut.Margin = new System.Windows.Forms.Padding(4);
            this.panelInOut.Name = "panelInOut";
            this.panelInOut.Size = new System.Drawing.Size(420, 261);
            this.panelInOut.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(230, 212);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 22);
            this.label7.TabIndex = 5;
            this.label7.Text = "Marcar Salida";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(51, 212);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 22);
            this.label6.TabIndex = 1;
            this.label6.Text = "Marcar entrada";
            // 
            // btnOut
            // 
            this.btnOut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(60)))), ((int)(((byte)(190)))));
            this.btnOut.Location = new System.Drawing.Point(223, 89);
            this.btnOut.Margin = new System.Windows.Forms.Padding(4);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(131, 108);
            this.btnOut.TabIndex = 4;
            this.btnOut.Text = "📤";
            this.btnOut.UseVisualStyleBackColor = true;
            // 
            // panelHour
            // 
            this.panelHour.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelHour.Controls.Add(this.label5);
            this.panelHour.Location = new System.Drawing.Point(15, 15);
            this.panelHour.Margin = new System.Windows.Forms.Padding(4);
            this.panelHour.Name = "panelHour";
            this.panelHour.Size = new System.Drawing.Size(391, 63);
            this.panelHour.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(45, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(294, 29);
            this.label5.TabIndex = 2;
            this.label5.Text = "Bienvenido a su jornada";
            // 
            // FormInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(954, 554);
            this.Controls.Add(this.panelInOut);
            this.Controls.Add(this.panelHora);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormInOut";
            this.Text = "FormInOut";
            this.Load += new System.EventHandler(this.FormInOut_Load);
            this.panelHora.ResumeLayout(false);
            this.panelHora.PerformLayout();
            this.panelDateHour.ResumeLayout(false);
            this.panelDateHour.PerformLayout();
            this.panelInOut.ResumeLayout(false);
            this.panelInOut.PerformLayout();
            this.panelHour.ResumeLayout(false);
            this.panelHour.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLunesViernes;
        private System.Windows.Forms.Panel panelHora;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Label lblSabado;
        private System.Windows.Forms.Panel panelInOut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelDateHour;
        private System.Windows.Forms.Panel panelHour;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOut;
    }
}