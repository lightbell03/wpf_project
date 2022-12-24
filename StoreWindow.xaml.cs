using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Media;

namespace WPProject
{
    /// <summary>
    /// StoreWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StoreWindow : Window
    {
        SoundPlayer coinSound = new SoundPlayer(@"sound\coinSound.wav");
        SoundPlayer storeoutSound = new SoundPlayer(@"sound\storeinSound.wav");
        public int sPoint; //StoreWindow 내에 표시될 포인트 값
        public int sDigLevel; //StoreWindow 내에 표시될 곡괭이 레벨 값
        public int sSkillLevel;//storeWindow 내에 표시될 무기 레벨 값
        public int characterLevel;
        public int curcharacterExp;
        public int characterExp;
        public StoreWindow(int p, int d, int sSkillLevel, int characterLevel, int curcharacterExp, int characterExp)
        {
            this.characterLevel = characterLevel;
            this.curcharacterExp = curcharacterExp;
            this.characterExp = characterExp;
            this.sPoint = p; //MainWindow 에서 point 변수를 p 매개변수로 넘겨주면 sPoint 에 저장
            this.sDigLevel = d; //MainWindow 에서 digLevel 변수를 d 매개변수로 넘겨주면 sDigLevel 에 저장
            this.sSkillLevel = sSkillLevel;
            InitializeComponent();
            storePointText.Content = "포인트 : " + sPoint; //StoreWindow 내에 현재 보유 포인트 표시
            sDigLevelText.Text = "현재 곡괭이 Lv. " + sDigLevel; //StoreWindow 내에 현재 곡괭이 레벨 표시 
            sSkillLevelText.Text = "현재 무기 Lv. " + sSkillLevel;
        }

        private void storeToMain_Click(object sender, RoutedEventArgs e) //상점에서 메인으로 넘어가는 버튼 이벤트
        {
            storeoutSound.Play();
            MainWindow mw = new MainWindow(sPoint, sDigLevel, sSkillLevel, characterLevel, characterExp, curcharacterExp);
            mw.Show();
            this.Close();
        }

        private void digUpgrade_Click(object sender, RoutedEventArgs e) //곡괭이 업그레이드
        {
            if (sPoint > sDigLevel)
            {
                coinSound.Play();
                sPoint -= sDigLevel+1;
                sDigLevel++;
                warningText.Text = "";
                sDigLevelText.Text = "현재 곡괭이 Lv. " + sDigLevel;
                storePointText.Content = "포인트 : " + sPoint;
            }
            else
            {
                warningText.Text = "포인트가 부족합니다!!!";
            }
        }

        private void skillUpgrade_Click(object sender, RoutedEventArgs e) //스킬 업그레이드
        {
            if (sPoint > sSkillLevel)
            {
                coinSound.Play();
                sPoint -= (sSkillLevel + 1);
                sSkillLevel++;
                sSkillLevelText.Text = "현재 무기 Lv. " + sSkillLevel;
                storePointText.Content = "포인트 : " + sPoint;
            }
            else
            {
                warningText.Text = "포인트가 부족합니다!!!";
            }
        }
    }
}
