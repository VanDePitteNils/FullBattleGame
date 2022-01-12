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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wel.Battle.Game.Core.Services;
using Wel.Battle.Game.Core.Entities;
using Wel.Battle.Game.Core.Entities.Classes;
using Wel.Battle.Game.Core.Entities.Weapons;
using Wel.Battle.Game.Core.Interfaces;

namespace Wel.Battle.Game.Wpf
{
    public partial class MainWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Loaded += ToolWindow_Loaded;
        }

        #region ---- Other ----

        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Home venster = new Home();
            this.Visibility = Visibility.Hidden;
            venster.ShowDialog();
        }

        #endregion

        #region ---- Global Variables ----

        BattleGameService bgs = new BattleGameService();
        private readonly List<Weapon> weapons = new List<Weapon>();
        private readonly Random rnd = new Random();
        int battleCounter = 0;

        #endregion

        #region ---- Event Handlers ----

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(UpdateMessage(),"Patch Notes",MessageBoxButton.OK,MessageBoxImage.Information);
            DoSeeding();
            btnEquip.IsEnabled = false;
            btnUnequip.IsEnabled = false;
            btnItem.IsEnabled = false;
        }

        private void LstAttackers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsAttackerAndDefenderSelected();
            ButtonCheck();
            if(lstAttackers.SelectedItem != null)
            {
                lblPlayerDetail.Content = bgs.ShowPlayerInfo((Player)lstAttackers.SelectedItem);
            }
        }

        private void LstDefenders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsAttackerAndDefenderSelected();
            ButtonCheck();
        }

        private void BtnEquip_Click(object sender, RoutedEventArgs e)
        {
            Player player = (Player)lstDefenders.SelectedItem;
            player.Equip((Weapon)lstItems.SelectedItem);
            RefreshLists();
        }

        private void BtnUnequip_Click(object sender, RoutedEventArgs e)
        {
            //
        }

        private void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            PlayerAttack();
            ComputerAttack();
            if (battleCounter >= 5)
            {
                RefreshBattleChat();
                battleCounter = 0;
            }
            RefreshLists();
        }

        private void BtnAbility_Click(object sender, RoutedEventArgs e)
        {
            Player defender = (Player)lstDefenders.SelectedItem;

            if (battleCounter >= 4)
            {
                RefreshBattleChat();
                battleCounter = 0;
            }

            if (IsMage((Player)lstAttackers.SelectedItem))
            {
                MageFireballAbility();
                battleCounter++;
            }
            else if (IsTank((Player)lstAttackers.SelectedItem))
            {
                Tank tank = (Tank)lstAttackers.SelectedItem;
                tank.Beserk(defender);
                battleCounter += 2;
            }
            else if (IsAssassin((Player)lstAttackers.SelectedItem))
            {
                Assassin assassin = (Assassin)lstAttackers.SelectedItem;
                assassin.Leach(defender);
                battleCounter += 2;
            }
            ShowAbilityBattleChat();
            ComputerAttack();
            RefreshLists();
            RefreshPlayerInfo();
        }
        #endregion

        #region ---- Methods ----

        public string UpdateMessage()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("########## NEW IN GAME ##########\n\n");
            sb.Append("* Attackers can now use a class ability that changes depending on the class\n");
            sb.Append("* Defenders are now classified as enemies and not longer as playable characters\n");
            sb.Append("* BattleChat is now a feature. when you attack or use an ability it will show what that character is doing\n");

            return sb.ToString();
        }

        public void DoSeeding()
        {
            SeedPlayers();
            SeedTeams();
        }

        public void SeedWeapons()
        {
            weapons.Add(new Katana("Katana"));
            weapons.Add(new Knife("Knife"));
        }

        public void SeedPlayers()
        {
            bgs.AddMage("Alice");
            bgs.AddTank("Bob");
            bgs.AddAssassin("Carol");
            bgs.AddMage("Dave");
            bgs.AddMage("Eve");
            bgs.AddTank("Freddy");
        }

        public void SeedTeams()
        {
            bgs.AddAttacker(bgs.players[0]);
            bgs.AddAttacker(bgs.players[1]);
            bgs.AddAttacker(bgs.players[2]);
            bgs.AddDefender(bgs.players[3]);
            bgs.AddDefender(bgs.players[4]);
            bgs.AddDefender(bgs.players[5]);
            FillLists();
        }

        public void FillLists()
        {
            lstAttackers.ItemsSource = bgs.attackers;
            lstDefenders.ItemsSource = bgs.defenders;
            lstItems.ItemsSource = weapons;
        }

        public void RefreshLists()
        {
            lstAttackers.Items.Refresh();
            lstDefenders.Items.Refresh();
        }

        public void RefreshPlayerInfo()
        {
            Player player = (Player)lstAttackers.SelectedItem;
            lblPlayerDetail.Content = "";
            lblPlayerDetail.Content = bgs.ShowPlayerInfo(player);
        }

        public void RefreshBattleChat()
        {
            lblBattleChat.Content = "";
        }

        public void IsAttackerAndDefenderSelected()
        {
            if (lstDefenders.SelectedItem == null && lstAttackers.SelectedItem == null)
            {
                btnAbility.IsEnabled = false;
            }
            else
            {
                btnAbility.IsEnabled = true;
            }
        }

        public void ShowFriendlyAttackBattleChat()
        {
            lblBattleChat.Content += bgs.FriendlyAttackBattleChat((Player)lstAttackers.SelectedItem, (Player)lstDefenders.SelectedItem);
            battleCounter++;
        }

        public void ShowEnemyAttackBattleChat(Player enemy, Player friendly)
        {
            lblBattleChat.Content += bgs.EnemyAttackBattleChat(enemy, friendly);
            battleCounter++;
        }

        public void ShowAbilityBattleChat()
        {
            lblBattleChat.Content += bgs.FriendlyAbilityBattleChat((Player)lstAttackers.SelectedItem, (Player)lstDefenders.SelectedItem);
        }

        public void ButtonCheck()
        {
            Player attacker = (Player)lstAttackers.SelectedItem;
            Player defender = (Player)lstDefenders.SelectedItem;

            if(attacker != null && defender != null)
            {
                if (attacker.IsAlive && defender.IsAlive)
                {
                    btnAttack.IsEnabled = true;
                    btnAbility.IsEnabled = true;
                }
                else
                {
                    btnAttack.IsEnabled = false;
                    btnAbility.IsEnabled = false;
                }
            }

        }

        public static bool IsMage(Player player)
        {
            return player is Mage;
        }

        public static bool IsTank(Player player)
        {
            return player is Tank;
        }

        public static bool IsAssassin(Player player)
        {
            return player is Assassin;
        }

        #region ---- Abilities ----

        public void MageFireballAbility()
        {
            Player defender = (Player)lstDefenders.SelectedItem;
            int findPlayerPos = bgs.players.IndexOf(defender);

            Mage.FireBall(bgs.players[findPlayerPos]);
            if (findPlayerPos == bgs.players.Count - 1)
            {
                Mage.FireBall(bgs.players[findPlayerPos - 1]);
            }
            else if (findPlayerPos == 0)
            {
                Mage.FireBall(bgs.players[findPlayerPos + 1]);
            }
            else
            {
                Mage.FireBall(bgs.players[findPlayerPos + 1]);
                Mage.FireBall(bgs.players[findPlayerPos - 1]);
            }
        }

        #endregion

        #region ---- Playable methods ---- 

        public void PlayerAttack()
        {
            ButtonCheck();
            Player attacker = (Player)lstAttackers.SelectedItem;
            Player defender = (Player)lstDefenders.SelectedItem;
            bgs.AttackEnemy(attacker, defender);
            ShowFriendlyAttackBattleChat();
        }

        public void ComputerAttack() 
        {

            Player randomDefender = bgs.defenders[rnd.Next(0, bgs.defenders.Count)];
            Player randomAttacker = bgs.attackers[rnd.Next(0, bgs.attackers.Count)];
            do
            {
                randomAttacker = bgs.attackers[rnd.Next(0, bgs.attackers.Count)];
                randomDefender = bgs.defenders[rnd.Next(0, bgs.defenders.Count)];

                if (randomAttacker.IsAlive && randomDefender.IsAlive)
                {
                    bgs.EnemyAttack(randomDefender, randomAttacker);
                    ShowEnemyAttackBattleChat(randomDefender, randomAttacker);
                    break;
                }
            } while (!randomAttacker.IsAlive && !randomDefender.IsAlive);
        }
        #endregion

        #endregion
    }
}