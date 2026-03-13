namespace LB.Encrypt
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            lblResult = new Label();
            lblKey = new Label();
            lblIV = new Label();
            lblText = new Label();
            txtKey = new TextBox();
            btnEncrypt = new Button();
            txtIV = new TextBox();
            txtText = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnHash = new Button();
            btnDecrypt = new Button();
            txtResult = new TextBox();
            btnCopyResult = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(15, 155);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(63, 20);
            lblResult.TabIndex = 0;
            lblResult.Text = "Result";
            // 
            // lblKey
            // 
            lblKey.AutoSize = true;
            lblKey.Location = new Point(12, 14);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(36, 20);
            lblKey.TabIndex = 1;
            lblKey.Text = "Key";
            // 
            // lblIV
            // 
            lblIV.AutoSize = true;
            lblIV.Location = new Point(12, 45);
            lblIV.Name = "lblIV";
            lblIV.Size = new Size(27, 20);
            lblIV.TabIndex = 2;
            lblIV.Text = "IV";
            // 
            // lblText
            // 
            lblText.AutoSize = true;
            lblText.Location = new Point(12, 76);
            lblText.Name = "lblText";
            lblText.Size = new Size(45, 20);
            lblText.TabIndex = 3;
            lblText.Text = "Text";
            // 
            // txtKey
            // 
            txtKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtKey.BackColor = Color.Black;
            txtKey.BorderStyle = BorderStyle.FixedSingle;
            txtKey.ForeColor = Color.Orange;
            txtKey.Location = new Point(93, 12);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(329, 25);
            txtKey.TabIndex = 5;
            // 
            // btnEncrypt
            // 
            btnEncrypt.Dock = DockStyle.Fill;
            btnEncrypt.FlatStyle = FlatStyle.Flat;
            btnEncrypt.Location = new Point(3, 3);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(130, 33);
            btnEncrypt.TabIndex = 1;
            btnEncrypt.Text = "Encrypt";
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // txtIV
            // 
            txtIV.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtIV.BackColor = Color.Black;
            txtIV.BorderStyle = BorderStyle.FixedSingle;
            txtIV.ForeColor = Color.Orange;
            txtIV.Location = new Point(93, 43);
            txtIV.Name = "txtIV";
            txtIV.Size = new Size(329, 25);
            txtIV.TabIndex = 9;
            // 
            // txtText
            // 
            txtText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtText.BackColor = Color.Black;
            txtText.BorderStyle = BorderStyle.FixedSingle;
            txtText.ForeColor = Color.Orange;
            txtText.Location = new Point(93, 74);
            txtText.Name = "txtText";
            txtText.Size = new Size(329, 25);
            txtText.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnHash, 2, 0);
            tableLayoutPanel1.Controls.Add(btnDecrypt, 1, 0);
            tableLayoutPanel1.Controls.Add(btnEncrypt, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 105);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(410, 39);
            tableLayoutPanel1.TabIndex = 11;
            // 
            // btnHash
            // 
            btnHash.Dock = DockStyle.Fill;
            btnHash.FlatStyle = FlatStyle.Flat;
            btnHash.Location = new Point(275, 3);
            btnHash.Name = "btnHash";
            btnHash.Size = new Size(132, 33);
            btnHash.TabIndex = 3;
            btnHash.Text = "Hash";
            btnHash.UseVisualStyleBackColor = true;
            btnHash.Click += btnHash_Click;
            // 
            // btnDecrypt
            // 
            btnDecrypt.Dock = DockStyle.Fill;
            btnDecrypt.FlatStyle = FlatStyle.Flat;
            btnDecrypt.Location = new Point(139, 3);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(130, 33);
            btnDecrypt.TabIndex = 2;
            btnDecrypt.Text = "Decrypt";
            btnDecrypt.UseVisualStyleBackColor = true;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // txtResult
            // 
            txtResult.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtResult.BackColor = Color.Black;
            txtResult.BorderStyle = BorderStyle.FixedSingle;
            txtResult.ForeColor = Color.Orange;
            txtResult.Location = new Point(90, 150);
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(302, 25);
            txtResult.TabIndex = 12;
            // 
            // btnCopyResult
            // 
            btnCopyResult.FlatStyle = FlatStyle.Flat;
            btnCopyResult.Image = (Image)resources.GetObject("btnCopyResult.Image");
            btnCopyResult.Location = new Point(398, 151);
            btnCopyResult.Name = "btnCopyResult";
            btnCopyResult.Size = new Size(24, 24);
            btnCopyResult.TabIndex = 13;
            btnCopyResult.UseVisualStyleBackColor = true;
            btnCopyResult.Click += btnCopyResult_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(434, 188);
            Controls.Add(btnCopyResult);
            Controls.Add(txtResult);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(txtText);
            Controls.Add(txtIV);
            Controls.Add(txtKey);
            Controls.Add(lblText);
            Controls.Add(lblIV);
            Controls.Add(lblKey);
            Controls.Add(lblResult);
            Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.Orange;
            Margin = new Padding(4);
            MaximizeBox = false;
            MaximumSize = new Size(1918, 227);
            MinimumSize = new Size(0, 227);
            Name = "MainForm";
            Text = "Encrypt/Decrypt";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblResult;
        private Label lblKey;
        private Label lblIV;
        private Label lblText;
        private TextBox txtKey;
        private Button btnEncrypt;
        private TextBox txtIV;
        private TextBox txtText;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnDecrypt;
        private TextBox txtResult;
        private Button btnHash;
        private Button btnCopyResult;
    }
}