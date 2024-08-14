using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }
        public List<Note> GetAll()
        {
            using (SqlConnection conn = Connection)
            {

            }
        }

        public Note Get(int id)
        {
            return null;
        }

        public void Insert (Note note)
        {
            return;
        }

        public void Update (Note note)
        {
            return;
        }

        public void Delete(int id)
        {
            return;
        }
    }
}
