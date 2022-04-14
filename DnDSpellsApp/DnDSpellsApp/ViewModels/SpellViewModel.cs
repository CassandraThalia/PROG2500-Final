using DnDSpellsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDSpellsApp.ViewModels
{
    public class SpellViewModel
    {
        // Create a Property Changed Event Handler object
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SpellModel> SpellModels { get; set; }

        private List<SpellModel> _allSpells = new List<SpellModel>();

        private SpellModel _spellSelected;

        public string SpellName { get; set; }

        public SpellViewModel()
        {
            SpellModels = new ObservableCollection<SpellModel>();

            CreateListOfSpells();
        }

        public SpellModel SelectedSpell
        {
            get { return _spellSelected; }
            set
            {
                _spellSelected = value;

                if (value != null)
                {
                    SpellName = value.Name;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SpellName"));
            }
        }

        public async void CreateListOfSpells()
        {
            _allSpells.Clear();
            SpellModels.Clear();

            
        }

    }
}
