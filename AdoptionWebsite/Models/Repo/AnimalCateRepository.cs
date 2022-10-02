using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptionWebsite.Models.Repo
{
    public class AnimalCateRepository : IAnimalCateRepository
    {
        public Animal_AdoptionContext _db = new Animal_AdoptionContext();

        public void DB_Dispose()
        {
            _db.Dispose();
        }
        public IQueryable<AnimalCate> ListAllCate()
        {
            IQueryable<AnimalCate> itemList = _db.AnimalCate.Select(s => s);
            return itemList;
        }

    }
}
