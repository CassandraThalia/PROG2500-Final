using DnDSpellsApp.Repositories;
using DnDSpellsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DnDSpellsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public SpellViewModel SpellViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            ApiHelper.InitializeClient();
            this.SpellViewModel = new SpellViewModel();
        }

        private async Task LoadData(string spellName = "aid")
        {
            var spell = await SpellProcessor.LoadSpell(SpellViewModel);

            //var uriSource = new Uri(spell.Url, UriKind.Absolute);
            var uriSource = new Uri("https://www.dndbeyond.com/spells/" + spell.Index, UriKind.Absolute);

            SpellNameTextBox.Text = spell.Name;
            SpellLevelTextBox.Text = spell.Level;

            for(int i = 0; i < spell.Desc.Length; i++)
            {
                SpellDescriptionTextBox.Text += spell.Desc[i] + " ";
            }
            //SpellDescriptionTextBox.Text = spell.Desc[0];


            SpellDurationTextBox.Text = spell.Duration;
            SpellRangeTextBox.Text = spell.Range;

            WebView.Source = uriSource;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private void DetailsButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutAppPage));
        }

        private void TeamMembersButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutTeamPage));
        }
    }
}
