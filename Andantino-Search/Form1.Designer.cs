namespace Andantino_Search
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.picGrid = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_player_turn = new System.Windows.Forms.Label();
            this.txtbox_number_possible_hexes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbox_p1_number_coins = new System.Windows.Forms.TextBox();
            this.txtbox_p2_number_coins = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.button_ai_move = new System.Windows.Forms.Button();
            this.label_ai_move_result = new System.Windows.Forms.Label();
            this.button_undo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_ai_move_stats = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_time_limit = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // picGrid
            // 
            this.picGrid.BackColor = System.Drawing.Color.White;
            this.picGrid.Location = new System.Drawing.Point(12, 12);
            this.picGrid.Name = "picGrid";
            this.picGrid.Size = new System.Drawing.Size(711, 629);
            this.picGrid.TabIndex = 0;
            this.picGrid.TabStop = false;
            this.picGrid.Click += new System.EventHandler(this.picGrid_Click);
            this.picGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.picGrid_Paint_1);
            this.picGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picGrid_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(729, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "Player\'s turn :";
            // 
            // label_player_turn
            // 
            this.label_player_turn.AutoSize = true;
            this.label_player_turn.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.label_player_turn.Location = new System.Drawing.Point(736, 99);
            this.label_player_turn.Name = "label_player_turn";
            this.label_player_turn.Size = new System.Drawing.Size(79, 29);
            this.label_player_turn.TabIndex = 7;
            this.label_player_turn.Text = "label2";
            // 
            // txtbox_number_possible_hexes
            // 
            this.txtbox_number_possible_hexes.Enabled = false;
            this.txtbox_number_possible_hexes.Location = new System.Drawing.Point(860, 166);
            this.txtbox_number_possible_hexes.Name = "txtbox_number_possible_hexes";
            this.txtbox_number_possible_hexes.Size = new System.Drawing.Size(40, 20);
            this.txtbox_number_possible_hexes.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(738, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Number possible hexes";
            // 
            // txtbox_p1_number_coins
            // 
            this.txtbox_p1_number_coins.Enabled = false;
            this.txtbox_p1_number_coins.Location = new System.Drawing.Point(860, 217);
            this.txtbox_p1_number_coins.Name = "txtbox_p1_number_coins";
            this.txtbox_p1_number_coins.Size = new System.Drawing.Size(40, 20);
            this.txtbox_p1_number_coins.TabIndex = 12;
            // 
            // txtbox_p2_number_coins
            // 
            this.txtbox_p2_number_coins.Enabled = false;
            this.txtbox_p2_number_coins.Location = new System.Drawing.Point(860, 257);
            this.txtbox_p2_number_coins.Name = "txtbox_p2_number_coins";
            this.txtbox_p2_number_coins.Size = new System.Drawing.Size(40, 20);
            this.txtbox_p2_number_coins.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(765, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "P1 # hexes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(768, 264);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "P2 # hexes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(768, 305);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Player 1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(771, 334);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Player 2";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.RoyalBlue;
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(860, 304);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(40, 20);
            this.textBox6.TabIndex = 18;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.IndianRed;
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(860, 333);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(40, 20);
            this.textBox7.TabIndex = 19;
            // 
            // button_ai_move
            // 
            this.button_ai_move.Location = new System.Drawing.Point(741, 12);
            this.button_ai_move.Name = "button_ai_move";
            this.button_ai_move.Size = new System.Drawing.Size(75, 23);
            this.button_ai_move.TabIndex = 21;
            this.button_ai_move.Text = "AI Move";
            this.button_ai_move.UseVisualStyleBackColor = true;
            this.button_ai_move.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_ai_move_result
            // 
            this.label_ai_move_result.AutoSize = true;
            this.label_ai_move_result.Location = new System.Drawing.Point(946, 19);
            this.label_ai_move_result.Name = "label_ai_move_result";
            this.label_ai_move_result.Size = new System.Drawing.Size(68, 13);
            this.label_ai_move_result.TabIndex = 23;
            this.label_ai_move_result.Text = "AI Move Info";
            // 
            // button_undo
            // 
            this.button_undo.Location = new System.Drawing.Point(833, 12);
            this.button_undo.Name = "button_undo";
            this.button_undo.Size = new System.Drawing.Size(75, 23);
            this.button_undo.TabIndex = 24;
            this.button_undo.Text = "Undo";
            this.button_undo.UseVisualStyleBackColor = true;
            this.button_undo.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(826, 466);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "label2";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // label_ai_move_stats
            // 
            this.label_ai_move_stats.AutoSize = true;
            this.label_ai_move_stats.Location = new System.Drawing.Point(865, 372);
            this.label_ai_move_stats.Name = "label_ai_move_stats";
            this.label_ai_move_stats.Size = new System.Drawing.Size(0, 13);
            this.label_ai_move_stats.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(771, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Stats of AI move:";
            // 
            // label_time_limit
            // 
            this.label_time_limit.AutoSize = true;
            this.label_time_limit.Location = new System.Drawing.Point(754, 529);
            this.label_time_limit.Name = "label_time_limit";
            this.label_time_limit.Size = new System.Drawing.Size(0, 13);
            this.label_time_limit.TabIndex = 28;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(949, 139);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(64, 17);
            this.checkBox1.TabIndex = 29;
            this.checkBox1.Text = "Minimax";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(949, 180);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(78, 17);
            this.checkBox2.TabIndex = 30;
            this.checkBox2.Text = "Alpha Beta";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(949, 224);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(75, 17);
            this.checkBox3.TabIndex = 31;
            this.checkBox3.Text = "Nega Max";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Enabled = false;
            this.checkBox4.Location = new System.Drawing.Point(949, 264);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(47, 17);
            this.checkBox4.TabIndex = 32;
            this.checkBox4.Text = "PVS";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 653);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label_time_limit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_ai_move_stats);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_undo);
            this.Controls.Add(this.label_ai_move_result);
            this.Controls.Add(this.button_ai_move);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtbox_p2_number_coins);
            this.Controls.Add(this.txtbox_p1_number_coins);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtbox_number_possible_hexes);
            this.Controls.Add(this.label_player_turn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_player_turn;
        private System.Windows.Forms.TextBox txtbox_number_possible_hexes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbox_p1_number_coins;
        private System.Windows.Forms.TextBox txtbox_p2_number_coins;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button button_ai_move;
        private System.Windows.Forms.Label label_ai_move_result;
        private System.Windows.Forms.Button button_undo;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_ai_move_stats;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_time_limit;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}

