using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptionWebsite.Models.Repo
{
    interface IAnimalRepository
    {
        IQueryable<Animal_AnimalCateViewModel> ListAllAnimal();
        Animal_FilesViewModel GetAnimalByID(int id);
        int AddAnimal(Animal item);
        int DelAnimal(int id);
        int EditAnimal(Animal item);
        int AddPhoto(List<Files> itemList);
        int DelPhoto(int id);
        void DB_Dispose();
    }
}
