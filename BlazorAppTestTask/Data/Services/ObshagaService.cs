using BlazorAppTestTask.PageModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HousingDB;
using HousingDB.Models;

namespace BlazorAppTestTask.Data.Services
{
    public class ObshagaService
    {
        private HousingDBContext db;
        private HousingRepos<Obshaga> repos;


        public ObshagaService(HousingDBContext context)
        {
            db = context;
            repos = new HousingRepos<Obshaga>(context);
        }
        public List<ObshagaItemViewModel> GetAll()
        {
            var list = repos.Get().ToList();
            var result = list.Select(x => Convert(x)).ToList();
            return result;
        }

        private static ObshagaItemViewModel Convert(Obshaga model)
        {
            var item = new ObshagaItemViewModel(model);
            return item;
        }

        public ObshagaItemViewModel Update(ObshagaItemViewModel modelObshaga)
        {
            var x = repos.FindById(modelObshaga.ObsId);
            x.Address = modelObshaga.Address;
            x.NumRooms = modelObshaga.NumRooms;
            

            var result = Convert(repos.Update(x, modelObshaga.Item.RowVersion));
            return result;
        }

        public ObshagaItemViewModel Create(ObshagaItemViewModel modelObshaga)
        {
            var result = repos.Create(modelObshaga.Item);
            return Convert(result);
        }
        public void Delete(ObshagaItemViewModel modelObshaga)
        {
            var y = repos.FindById(modelObshaga.ObsId);
            var result = repos.Delete(y);
        }
        public List<ObshagaItemViewModel> GetFiltering(string name)
        {
            var list = db.GetFilteringObshaga(name);
            var result = list.Select(Convert).ToList();
            return result;
        }
        public ObshagaItemViewModel ReloadItem(ObshagaItemViewModel item)
        {
            var x = repos.Reload(item.ObsId);
            if (x == null)
            {
                return null;
            }
            return Convert(x);
        }
    }
}
