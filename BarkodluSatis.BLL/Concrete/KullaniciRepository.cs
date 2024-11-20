using BarkodluSatis.DLL.IConcrete;
using BarkodluSatisProgrami1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatis.BLL.Concrete
{
    public class KullaniciRepository :RepositoryBase<Kullanici>, IKullanici<Kullanici>
    {
        public void Add(Kullanici entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Kullanici entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Kullanici> GetAll()
        {
            throw new NotImplementedException();
        }

        public Kullanici GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Kullanici entity)
        {
            throw new NotImplementedException();
        }
    }
}
