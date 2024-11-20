using BarkodluSatis.DLL.IConcrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarkodluSatisProgrami1.Models;

namespace BarkodluSatis.BLL.Concrete
{
    public class BarkodRepository :RepositoryBase<Barkod>, IBarkod<Barkod>
    {
        public void Add(Barkod entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Barkod entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Barkod> GetAll()
        {
            throw new NotImplementedException();
        }

        public Barkod GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Barkod entity)
        {
            throw new NotImplementedException();
        }
    }
}
