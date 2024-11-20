using BarkodluSatis.DLL;
using BarkodluSatis.DLL.IConcrete;
using BarkodluSatisProgrami1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarkodluSatis.BLL.Concrete
{
    public class UrunRepository :RepositoryBase<Urun>, IUrun<Urun>
    {
        public void Add(Urun entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Urun entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Urun> GetAll()
        {
            throw new NotImplementedException();
        }

        public Urun GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Urun entity)
        {
            throw new NotImplementedException();
        }
    }
}
