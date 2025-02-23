﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevnotMentor.Api.Entities;
using DevnotMentor.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevnotMentor.Api.Repositories
{
    public class MentorRepository : BaseRepository<Mentor>, IMentorRepository
    {
        public MentorRepository(MentorDBContext context) : base(context)
        {
        }

        public async Task<int> GetIdByUserId(int userId)
        {
            return await DbContext.Mentor.Where(i => i.UserId == userId).Select(i => i.Id).FirstOrDefaultAsync();
        }

        public async Task<Mentor> GetByUserName(string userName)
        {
            return await DbContext.Mentor.Include(x => x.User).Where(i => i.User.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<Mentor> GetByUserId(int userId)
        {
            return await DbContext.Mentor.Where(i => i.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistsByUserId(int userId)
        {
            return await DbContext.Mentor.AnyAsync(i => i.UserId == userId);
        }

        public async Task<IEnumerable<Mentee>> GetPairedMenteesByMentorId(int mentorId)
        {
            return await DbContext.MentorMenteePairs.Where(x => x.MentorId == mentorId)
                .Include(x => x.Mentee)
                .ThenInclude(x => x.User)
                .Select(x => x.Mentee)
                .ToListAsync();
        }
    }
}
