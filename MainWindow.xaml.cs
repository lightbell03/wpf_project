using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace WPProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        SoundPlayer storeinSound = new SoundPlayer(@"sound\storeinSound.wav");  //상점 들어갈때 소리
        SoundPlayer stoneSound = new SoundPlayer(@"sound\stone.wav");           //채광버튼 누를때 소리
        SoundPlayer dungeoninSound = new SoundPlayer(@"sound\dungeonin.wav");   //던전에 들어갈때 소리
        private int point = 0;                                                  //MainWindow 내에 표시될 포인트 값
        private int digLevel = 1;                                               //MainWindow 내에 표시될 곡괭이 레벨 값
        private int skillLevel = 1;                                             //MainWindow 내에 표시될 스킬 레벨 값
        private int characterLevel = 1;                                         //현재 캐릭터 레벨 
        private int characterExp = 100;                                         //현재 캐릭터가 레벨업하기 위해 채워야할 총 경험치량
        private int curcharacterExp;                                            //현재 캐릭터의 경험치


        public MainWindow()
        {
             InitializeComponent();
            pointText.Text = "포인트 : " + point;   //MainWindow 에 현재 보유 포인트 표시
            skillLevelText.Text = "현재 무기 Lv. " + skillLevel;
            digLevelText.Text = "현재 곡괭이 Lv. " + digLevel; //MainWindow 에 현재 곡괭이 레벨 표시
            characterLevelText.Text = "캐릭터 Lv. " + characterLevel;
            myMediaElement.Play();
        }

        public MainWindow(int point, int digLevel, int skillLevel, int characterLevel, int characterExp, int curcharacterExp)
        {
            InitializeComponent();
            this.point = point;
            this.digLevel = digLevel;
            this.skillLevel = skillLevel;
            this.characterLevel = characterLevel;
            this.characterExp = characterExp;
            this.curcharacterExp = curcharacterExp;
            TextWrite();
            myMediaElement.Play();
        }

        private void dunBtn_Click(object sender, RoutedEventArgs e) //던전으로 가는 버튼 이벤트
        {
            dungeoninSound.Play();
            DungeonWindow dw = new DungeonWindow(point, digLevel, skillLevel, characterLevel, curcharacterExp, characterExp);
            dw.Show();
            myMediaElement.Stop();
            this.Close();
        }

        private void storeBtn_Click(object sender, RoutedEventArgs e) //상점으로 가는 버튼 이벤트
        {
            storeinSound.Play();
            StoreWindow sw = new StoreWindow(point, digLevel, skillLevel, characterLevel, curcharacterExp, characterExp);
            sw.Show();
            this.Close();

        }

        private void dig_Click(object sender, RoutedEventArgs e) //채광하기 버튼 이벤트
        {
            var controller = ImageBehavior.GetAnimationController(this.digAction);
            controller.Play();
            stoneSound.Play();
            point += digLevel; //포인트를 늘려줌
            pointText.Text = "포인트 : " + point; //MainWindow에 현재 보유 포인트 표시를 최신화

            timer.Interval = TimeSpan.FromTicks(12000000);
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var controller = ImageBehavior.GetAnimationController(this.digAction);
            controller.Pause();
            timer.Stop();
        }

        public void TextWrite()
        {
            pointText.Text = "포인트 : " + point; //MainWindow 에 현재 보유 포인트 표시
            skillLevelText.Text = "현재 무기 Lv. " + skillLevel;
            digLevelText.Text = "현재 곡괭이 Lv. " + digLevel; //MainWindow 에 현재 곡괭이 레벨 표시
            characterLevelText.Text = "캐릭터 Lv. " + characterLevel;
        }

        private void myMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            myMediaElement.Position = TimeSpan.FromSeconds(0);
            myMediaElement.Play();
        }
    }
}
