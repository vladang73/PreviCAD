using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class InstructionRepository:RepositoryBase<Instruction>
    {
        public InstructionRepository(DbContext context) : base(context) { }

        public override Instruction Get(int id)
        {
            return dbSet.Include(b => b.Accessoire).Include(b=>b.Dispositif)
             .FirstOrDefault(b => b.InstructionId == id);
        }

        public override List<Instruction> GetAll()
        {
            return dbSet.Include(b => b.Accessoire).Include(b => b.Dispositif).ToList();
           
        }

      
    }
}