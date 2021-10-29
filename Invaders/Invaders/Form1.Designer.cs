namespace Invaders
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
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ScoreText = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.GameOverLabel = new System.Windows.Forms.Label();
            this.RestartLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // animationTimer
            // 
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // gameTimer
            // 
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // ScoreText
            // 
            this.ScoreText.AutoSize = true;
            this.ScoreText.Font = new System.Drawing.Font("Gulim", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ScoreText.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.ScoreText.Location = new System.Drawing.Point(28, 22);
            this.ScoreText.Name = "ScoreText";
            this.ScoreText.Size = new System.Drawing.Size(81, 20);
            this.ScoreText.TabIndex = 1;
            this.ScoreText.Text = "Score :";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Gulim", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ScoreLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.ScoreLabel.Location = new System.Drawing.Point(115, 22);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(45, 20);
            this.ScoreLabel.TabIndex = 2;
            this.ScoreLabel.Text = "100";
            // 
            // GameOverLabel
            // 
            this.GameOverLabel.AutoSize = true;
            this.GameOverLabel.BackColor = System.Drawing.Color.Transparent;
            this.GameOverLabel.Font = new System.Drawing.Font("Gulim", 75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.GameOverLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.GameOverLabel.Location = new System.Drawing.Point(102, 81);
            this.GameOverLabel.Name = "GameOverLabel";
            this.GameOverLabel.Size = new System.Drawing.Size(572, 100);
            this.GameOverLabel.TabIndex = 3;
            this.GameOverLabel.Text = "Game Over";
            this.GameOverLabel.Visible = false;
            // 
            // RestartLabel
            // 
            this.RestartLabel.AutoSize = true;
            this.RestartLabel.BackColor = System.Drawing.Color.Transparent;
            this.RestartLabel.Font = new System.Drawing.Font("Gulim", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.RestartLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.RestartLabel.Location = new System.Drawing.Point(259, 196);
            this.RestartLabel.Name = "RestartLabel";
            this.RestartLabel.Size = new System.Drawing.Size(254, 27);
            this.RestartLabel.TabIndex = 4;
            this.RestartLabel.Text = "Press S to Restart";
            this.RestartLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.RestartLabel);
            this.Controls.Add(this.GameOverLabel);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.ScoreText);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Invaders";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Invaders_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Invaders_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Invaders_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer animationTimer;
        private System.Windows.Forms.Timer gameTimer;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label ScoreText;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label GameOverLabel;
        private System.Windows.Forms.Label RestartLabel;
    }
}

