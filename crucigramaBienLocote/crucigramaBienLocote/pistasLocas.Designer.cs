namespace crucigramaBienLocote
{
    partial class pistasLocas
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
            this.pistas_board = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pistas_board)).BeginInit();
            this.SuspendLayout();
            // 
            // pistas_board
            // 
            this.pistas_board.AllowDrop = true;
            this.pistas_board.AllowUserToAddRows = false;
            this.pistas_board.AllowUserToDeleteRows = false;
            this.pistas_board.AllowUserToOrderColumns = true;
            this.pistas_board.AllowUserToResizeColumns = false;
            this.pistas_board.AllowUserToResizeRows = false;
            this.pistas_board.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pistas_board.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.pistas_board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pistas_board.Location = new System.Drawing.Point(0, 0);
            this.pistas_board.Name = "pistas_board";
            this.pistas_board.RowHeadersVisible = false;
            this.pistas_board.RowHeadersWidth = 51;
            this.pistas_board.RowTemplate.Height = 24;
            this.pistas_board.Size = new System.Drawing.Size(532, 620);
            this.pistas_board.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No.";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Dirección";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Pista";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // pistasLocas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 620);
            this.Controls.Add(this.pistas_board);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pistasLocas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "pistasLocas";
            ((System.ComponentModel.ISupportInitialize)(this.pistas_board)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        public System.Windows.Forms.DataGridView pistas_board;
    }
}