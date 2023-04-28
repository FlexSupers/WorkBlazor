using System.ComponentModel.DataAnnotations;
using HousingDB.Models;

namespace BlazorAppTestTask.PageModels
{
    public class ObshagaItemViewModel
    {
        private Obshaga item;
        public Obshaga Item => item;

        public ObshagaItemViewModel()
        {
            item = new Obshaga();
        }

        public ObshagaItemViewModel(Obshaga modelObshaga)
        {
            item = modelObshaga;
        }

        [Key]
        public int ObsId
        {
            get => item.ObsId;
            set => item.ObsId = value;
        }

        [Required(ErrorMessage = "Please enter a Address")]
        public string Address
        {
            get => item.Address;
            set => item.Address = value;
        }

        public int? NumRooms
        {
            get => item.NumRooms;
            set => item.NumRooms = value;
        }
    }
}
