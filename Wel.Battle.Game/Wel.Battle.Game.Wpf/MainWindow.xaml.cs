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

        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        #region ---- Global Variables ----

        BattleGameService bgs = new BattleGameService();
        private readonly List<Weapon> weapons = new List<Weapon>();

        #endregion

        #region ---- Event Handlers ----

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoSeeding();
            btnEquip.IsEnabled = false;
            btnUnequip.IsEnabled = false;
            btnItem.IsEnabled = false;
            
        }

        private void LstAttackers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsAttackerAndDefenderSelected();
            if(lstAttackers != null || lstDefenders != null)
            {
                lblPlayerDetail.Visibility = Visibility.Visible;
                lblPlayerDetail.Content = bgs.ShowPlayerInfo((Player)lstAttackers.SelectedItem);
            } 
            else
            {
                lblPlayerDetail.Visibility = Visibility.Hidden;
            }

        }

        private void LstDefenders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsAttackerAndDefenderSelected();
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
            int dead = 0;
            foreach(Player player in bgs.players)
            {
                if (!player.IsAlive)
                {
                    dead++;
                }
            }
            if(dead == bgs.defenders.Count)
            {
                btnAttack.IsEnabled = false;
                MessageBox.Show("All Enemies are dead");
            }
            bgs.AttackEnemy((Player)lstAttackers.SelectedItem, (Player)lstDefenders.SelectedItem);
            RefreshLists();
        }

        private void BtnAbility_Click(object sender, RoutedEventArgs e)
        {
            Player defender = (Player)lstDefenders.SelectedItem;
            if (IsMage((Player)lstAttackers.SelectedItem))
            {
                Mage.FireBall(defender);
            }
            else if (IsTank((Player)lstAttackers.SelectedItem))
            {
                Tank.Bash(defender);
            }
            else if (IsAssassin((Player)lstAttackers.SelectedItem))
            {
                Assassin.Stab(defender);
            }
            RefreshLists();
        }
        #endregion

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Home venster = new Home();
            this.Visibility = Visibility.Hidden;
            venster.ShowDialog();
        }

        private void shop_Click(object sender, RoutedEventArgs e)
        {
            shop venster = new shop();
            this.Visibility = Visibility.Hidden;
            venster.ShowDialog();
        }
        #region ---- Methods ----

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

        /*
        private List<IPlayer> SelectedItems()
        {
            List<IPlayer> selectedItems = new List<IPlayer>();

            foreach (IPlayer item in lstPreys.SelectedItems)
            {
                selectedItems.Add(item);
            }
            return selectedItems;
        }
        */

        #endregion
    }
}