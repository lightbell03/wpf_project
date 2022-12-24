
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;
using System.Windows.Media.Animation;

namespace WPProject
{
    /// <summary>
    /// DungeonWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DungeonWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        SoundPlayer dungeonoutSound = new SoundPlayer(@"sound\dungeonin.wav");
        private int dPoint;
        private int digLevel;
        public int skillLevel;
        public int characterLevel;
        public int curcharacterExp;
        public int characterExp;
        public DungeonWindow(int dpoint, int digLevel, int skillLevel, int characterLevel, int curcharacterExp, int characterExp)
        {
            InitializeComponent();
            this.dPoint = dpoint;
            this.digLevel = digLevel;
            this.skillLevel = skillLevel;
            this.characterLevel = characterLevel;
            this.curcharacterExp = curcharacterExp;
            this.characterExp = characterExp;
            dungeonsound.Play();
            TextWrite();
        }

        private void dungeonToMain_Click(object sender, RoutedEventArgs e)
        {
            dungeonoutSound.Play();
            MainWindow mw = new MainWindow(dPoint, digLevel, skillLevel, characterLevel, characterExp, curcharacterExp);
            mw.Show();
            this.Close();

        }

        private void dungeonFight_Click(object sender, RoutedEventArgs e)
        {
            dungeonFight.IsEnabled = false;
            progressBarAni();
            curcharacterExp += skillLevel;
            progressfight.Visibility = Visibility.Visible;
            TextWrite();
        }
        public void TextWrite()
        { 
            skillLevelText.Text = "현재 무기 Lv. " + skillLevel;
            if (curcharacterExp >= characterExp)
            {
                while(curcharacterExp >= characterExp) //무기 레벨이 현재 레벨의 경험치량 보다 많아 레벨업을 하고도 경험치량이 초과하는 것을 방지 
                {
                    curcharacterExp -= characterExp;
                    characterExp += 50;
                    characterLevel += 1;
                }
                
                characterLevelText.Text = "캐릭터 Lv. " + characterLevel;
                characterExpText.Text = "Exp: " + curcharacterExp + "/" + characterExp;
            }
            else
            {
                characterLevelText.Text = "캐릭터 Lv. " + characterLevel;
                characterExpText.Text = "Exp: " + curcharacterExp + "/" + characterExp;
            }
        }

        private void progressBarAni()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        } 

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var controller = ImageBehavior.GetAnimationController(this.fight);
            controller.Play();

            fightProgressBar.Value = e.ProgressPercentage;
            if (fightProgressBar.Value == 100)
            {
                dungeonFight.IsEnabled = true;
                progressfight.Visibility = Visibility.Hidden;
                controller.Pause();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(10+characterLevel*3);
            }
        }

        private void dungeonsound_MediaEnded(object sender, RoutedEventArgs e)
        {
            dungeonsound.Position = TimeSpan.FromSeconds(0);
            dungeonsound.Play();
        }
    }
}
