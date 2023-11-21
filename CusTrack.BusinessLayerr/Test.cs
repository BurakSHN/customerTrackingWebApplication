using CusTrack.DataAccessLayer.EntityFramework;
using CusTrack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.BusinessLayerr
{
    public class Test
    {
        private Repository<User> repo_user = new Repository<User>();

        public void InsertTest() {
            int result = repo_user.Insert(new User() {
                Username="test",
                Password="123"
            });
        }
    }
}
