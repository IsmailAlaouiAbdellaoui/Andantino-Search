namespace Andantino_Search
{
    partial class Home
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.box_p1_ai = new System.Windows.Forms.CheckBox();
            this.box_p1_human = new System.Windows.Forms.CheckBox();
            this.box_p2_ai = new System.Windows.Forms.CheckBox();
            this.box_p2_human = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(68, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(251, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player 2";
            // 
            // box_p1_ai
            // 
            this.box_p1_ai.AutoSize = true;
            this.box_p1_ai.Location = new System.Drawing.Point(71, 86);
            this.box_p1_ai.Name = "box_p1_ai";
            this.box_p1_ai.Size = new System.Drawing.Size(36, 17);
            this.box_p1_ai.TabIndex = 2;
            this.box_p1_ai.Text = "AI";
            this.box_p1_ai.UseVisualStyleBackColor = true;
            this.box_p1_ai.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // box_p1_human
            // 
            this.box_p1_human.AutoSize = true;
            this.box_p1_human.Location = new System.Drawing.Point(71, 132);
            this.box_p1_human.Name = "box_p1_human";
            this.box_p1_human.Size = new System.Drawing.Size(60, 17);
            this.box_p1_human.TabIndex = 3;
            this.box_p1_human.Text = "Human";
            this.box_p1_human.UseVisualStyleBackColor = true;
            this.box_p1_human.CheckedChanged += new System.EventHandler(this.box_p1_human_CheckedChanged);
            // 
            // box_p2_ai
            // 
            this.box_p2_ai.AutoSize = true;
            this.box_p2_ai.Location = new System.Drawing.Point(254, 86);
            this.box_p2_ai.Name = "box_p2_ai";
            this.box_p2_ai.Size = new System.Drawing.Size(36, 17);
            this.box_p2_ai.TabIndex = 4;
            this.box_p2_ai.Text = "AI";
            this.box_p2_ai.UseVisualStyleBackColor = true;
            this.box_p2_ai.CheckedChanged += new System.EventHandler(this.box_p2_ai_CheckedChanged);
            // 
            // box_p2_human
            // 
            this.box_p2_human.AutoSize = true;
            this.box_p2_human.Location = new System.Drawing.Point(254, 132);
            this.box_p2_human.Name = "box_p2_human";
            this.box_p2_human.Size = new System.Drawing.Size(60, 17);
            this.box_p2_human.TabIndex = 5;
            this.box_p2_human.Text = "Human";
            this.box_p2_human.UseVisualStyleBackColor = true;
            this.box_p2_human.CheckedChanged += new System.EventHandler(this.box_p2_human_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 37);
            this.button1.TabIndex = 6;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.box_p2_human);
            this.Controls.Add(this.box_p2_ai);
            this.Controls.Add(this.box_p1_human);
            this.Controls.Add(this.box_p1_ai);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Home";
            this.Text = "Home";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Home_FormClosing);
            this.Load += new System.EventHandler(this.Home_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Home_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox box_p1_ai;
        private System.Windows.Forms.CheckBox box_p1_human;
        private System.Windows.Forms.CheckBox box_p2_ai;
        private System.Windows.Forms.CheckBox box_p2_human;
        private System.Windows.Forms.Button button1;
    }
}