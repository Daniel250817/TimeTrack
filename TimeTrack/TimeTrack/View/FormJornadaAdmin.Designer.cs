namespace TimeTrack.View
{
    partial class FormJornadaAdmin
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvJornada = new System.Windows.Forms.DataGridView();
            this.panelTop1 = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.PanelTop2 = new System.Windows.Forms.Panel();
            this.lblDetails = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnActu = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJornada)).BeginInit();
            this.panelTop1.SuspendLayout();
            this.PanelTop2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvJornada
            // 
            this.dgvJornada.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJornada.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJornada.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvJornada.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJornada.GridColor = System.Drawing.SystemColors.Control;
            this.dgvJornada.Location = new System.Drawing.Point(13, 252);
            this.dgvJornada.Margin = new System.Windows.Forms.Padding(4);
            this.dgvJornada.Name = "dgvJornada";
            this.dgvJornada.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvJornada.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvJornada.RowHeadersWidth = 51;
            this.dgvJornada.Size = new System.Drawing.Size(774, 185);
            this.dgvJornada.TabIndex = 7;
            // 
            // panelTop1
            // 
            this.panelTop1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(255)))));
            this.panelTop1.Controls.Add(this.button1);
            this.panelTop1.Controls.Add(this.btnActu);
            this.panelTop1.Controls.Add(this.button3);
            this.panelTop1.Controls.Add(this.btnBuscar);
            this.panelTop1.Controls.Add(this.label1);
            this.panelTop1.Controls.Add(this.txtName);
            this.panelTop1.Controls.Add(this.PanelTop2);
            this.panelTop1.Location = new System.Drawing.Point(13, 13);
            this.panelTop1.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop1.Name = "panelTop1";
            this.panelTop1.Size = new System.Drawing.Size(774, 231);
            this.panelTop1.TabIndex = 6;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(567, 95);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 34);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(29, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Buscar nombre del empleado";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(290, 98);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(270, 28);
            this.txtName.TabIndex = 2;
            // 
            // PanelTop2
            // 
            this.PanelTop2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelTop2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PanelTop2.Controls.Add(this.lblDetails);
            this.PanelTop2.Location = new System.Drawing.Point(13, 13);
            this.PanelTop2.Margin = new System.Windows.Forms.Padding(4);
            this.PanelTop2.Name = "PanelTop2";
            this.PanelTop2.Size = new System.Drawing.Size(749, 71);
            this.PanelTop2.TabIndex = 1;
            // 
            // lblDetails
            // 
            this.lblDetails.AutoSize = true;
            this.lblDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetails.Location = new System.Drawing.Point(16, 25);
            this.lblDetails.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(445, 22);
            this.lblDetails.TabIndex = 0;
            this.lblDetails.Text = "El siguiente recuadro pertenece a sus jornadas laboral\r\n";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(447, 148);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnActu
            // 
            this.btnActu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActu.Location = new System.Drawing.Point(555, 148);
            this.btnActu.Margin = new System.Windows.Forms.Padding(4);
            this.btnActu.Name = "btnActu";
            this.btnActu.Size = new System.Drawing.Size(100, 34);
            this.btnActu.TabIndex = 6;
            this.btnActu.Text = "Modificar";
            this.btnActu.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(663, 148);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 34);
            this.button3.TabIndex = 7;
            this.button3.Text = "Eliminar";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // FormJornadaAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvJornada);
            this.Controls.Add(this.panelTop1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormJornadaAdmin";
            this.Text = "FormEmpleado";
            this.Load += new System.EventHandler(this.FormEmpleado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJornada)).EndInit();
            this.panelTop1.ResumeLayout(false);
            this.panelTop1.PerformLayout();
            this.PanelTop2.ResumeLayout(false);
            this.PanelTop2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJornada;
        private System.Windows.Forms.Panel panelTop1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Panel PanelTop2;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnActu;
        private System.Windows.Forms.Button button3;
    }
}