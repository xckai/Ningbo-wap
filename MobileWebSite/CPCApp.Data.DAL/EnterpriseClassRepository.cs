using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CPCApp.Data.Model;
using CPCApp.Data.IDAL;

namespace CPCApp.Data.DAL
{
    public class EnterpriseClassRepository : BaseRepository<EnterpriseClass>, IEnterpriseClassRepository
    {
    }
}