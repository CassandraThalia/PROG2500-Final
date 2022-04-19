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
    public class SpellViewModel : INotifyPropertyChanged
    {
        // Create a Property Changed Event Handler object
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SpellModel> SpellModels { get; set; }

        private List<SpellModel> _allSpells = new List<SpellModel>();

        private SpellModel _spellSelected;



        public string SpellName { get; set; }

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter) { return; }
                _filter = value;
                PerformFiltering();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
            }
        }
        private void PerformFiltering()
        {
            if (_filter == null)
            {
                _filter = "";
            }
            //If _filter has a value (ie. user entered something in Filter textbox)
            //Lower-case and trim string
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            //Use LINQ query to get all personmodel names that match filter text, as a list
            var result =
                _allSpells.Where(d => d.Name.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            //Get list of values in current filtered list that we want to remove
            //(ie. don't meet new filter criteria)
            var toRemove = SpellModels.Except(result).ToList();

            //Loop to remove items that fail filter
            foreach (var x in toRemove)
            {
                SpellModels.Remove(x);
            }

            var resultCount = result.Count;
            // Add back in correct order.
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > SpellModels.Count || !SpellModels[i].Equals(resultItem))
                {
                    SpellModels.Insert(i, resultItem);
                }
            }
        }

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

        public void AddSpell(string name, string duration, string range, string url)
        {
            SpellModel sm = new SpellModel(name, duration, range, url);
            _allSpells.Add(sm);
            PerformFiltering();
        }

        public void CreateListOfSpells()
        {
            for (int i = 0; i < 20; i++)
            //for (int i = 0; i < SpellModels.Count; i++)
            {
                this.AddSpell("test", "4 hours", "30 ft", "url");
                this.AddSpell("no thank you", "4 hours", "30 ft", "url");
                //SpellModels.Add(new SpellModel("test", "4 hours", "30 ft", "url"));
                //_allSpells.Add(new SpellModel("test", "4 hours", "30 ft", "url"));
            }
            //_allSpells.Add(await Repositories.SpellProcessor.LoadSpell("acid-arrow"));
            //_allSpells.Clear();
            //SpellModels.Clear();

            
        }

    }
}
