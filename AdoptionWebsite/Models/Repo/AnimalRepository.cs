using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptionWebsite.Models.Repo
{
    public class AnimalRepository : IAnimalRepository
    {
        public Animal_AdoptionContext _db = new Animal_AdoptionContext();

        public void DB_Dispose()
        {
            _db.Dispose();
        }

        public int AddAnimal(Animal item)
        {
            _db.Animal.Add(item);
            _db.SaveChanges();
            int id = item.Idno;
            return id;
        }

        public int DelAnimal(int id)
        {
            throw new NotImplementedException();
        }

        public int EditAnimal(Animal item)
        {
            _db.Update(item);
            return _db.SaveChanges();
        }

        public Animal_FilesViewModel GetAnimalByID(int id)
        {
            //將之前的刪除未確認的拉回來
            //應該有更好的寫法
            foreach (var itemfile in _db.Files.Where(w => w.TableId == id && w.XTable == "Animal"))
            {
                itemfile.IsDel = null;
            }
            _db.SaveChanges();

            Animal item_an = _db.Animal.Where(w => w.Idno == id).FirstOrDefault();
            Animal_FilesViewModel item = new Animal_FilesViewModel
            {
                Animals = item_an,
                _AnimalCate = _db.AnimalCate.Find(item_an.CateId),
                _Files = _db.Files.Where(w => w.TableId == id && w.XTable == "Animal").ToList()
            };

            return item;
        }

        public IQueryable<Animal_AnimalCateViewModel> ListAllAnimal()
        {
            var itemList = from an in _db.Animal
                           join cate in _db.AnimalCate on an.CateId equals cate.Idno
                           select new Animal_AnimalCateViewModel { Animals = an, Animal_Cate = cate };
            return itemList;
        }

        public int AddPhoto(List<Files> itemList)
        {
            int chk = 0;
            if (itemList.Count > 0)
            {
                foreach (Files i in itemList)
                {
                    _db.Files.Add(i);
                }
                chk = _db.SaveChanges();
            }

            return chk;
        }

        public int DelPhoto(int id)
        {
            //將之前的刪除刪掉
            //應該有更好的寫法
            foreach (var itemfile in _db.Files.Where(w => w.TableId == id && w.XTable == "Animal" && w.IsDel == 1))
            {
                _db.Files.Remove(itemfile);
            }

            return _db.SaveChanges();
        }
    }
}
