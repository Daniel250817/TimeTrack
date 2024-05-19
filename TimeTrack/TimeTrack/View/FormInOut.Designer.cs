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
            this.label1 = new System.Windows.Forms.Label();
            this.panelHora = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelInOut = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelHora.SuspendLayout();
            this.panelInOut.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Horario del empleado Lunes a viernes:";
            // 
            // panelHora
            // 
            this.panelHora.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelHora.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelHora.Controls.Add(this.label1);
            this.panelHora.Controls.Add(this.panel1);
            this.panelHora.Controls.Add(this.label4);
            this.panelHora.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelHora.Location = new System.Drawing.Point(436, 71);
            this.panelHora.Name = "panelHora";
            this.panelHora.Size = new System.Drawing.Size(273, 339);
            this.panelHora.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Horario del empleado sábado:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Hora Actual:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fechas:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Marcar entrada";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panelInOut
            // 
            this.panelInOut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelInOut.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelInOut.Controls.Add(this.button2);
            this.panelInOut.Controls.Add(this.label5);
            this.panelInOut.Controls.Add(this.button1);
            this.panelInOut.Location = new System.Drawing.Point(64, 157);
            this.panelInOut.Name = "panelInOut";
            this.panelInOut.Size = new System.Drawing.Size(315, 212);
            this.panelInOut.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(105, 135);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Marcar salida";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Bienvenido a su jornada";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(17, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 100);
            this.panel1.TabIndex = 2;
            // 
            // FormInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelInOut);
            this.Controls.Add(this.panelHora);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "FormInOut";
            this.Text = "FormInOut";
            this.panelHora.ResumeLayout(false);
            this.panelHora.PerformLayout();
            this.panelInOut.ResumeLayout(false);
            this.panelInOut.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelHora;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelInOut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
    }
}