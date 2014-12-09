using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPCApp.Data.IDAL;
using CPCApp.Data.Model;


namespace CPCApp.Data.DAL
{
    public class ReviewRepository:BaseRepository<Review>,IReviewRepository
    {
    }
}
