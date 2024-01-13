using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace FindJobAPI.Repository.Queries
{
    public class Seeker_Repository : ISeeker_Repository
    {
        private readonly AppDbContext _appDbContext;
        private readonly FirebaseAuth _firebaseAuth;
        public Seeker_Repository(AppDbContext appDbContext, FirebaseApp firebaseApp)
        {
            _appDbContext = appDbContext;
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        public async Task<CV> CV(string userId)
        {
            var seekerDomain = await _appDbContext.Seeker.FirstOrDefaultAsync(s => s.UID == userId);
            var accountFB = await _firebaseAuth.GetUserAsync(userId);
            if (seekerDomain == null || accountFB == null) return null!;
            var seeker = new CV()
            {
                Address = seekerDomain!.address,
                Experience = seekerDomain.experience,
                Skills = seekerDomain.skills,
                Education = seekerDomain.education,
                Major = seekerDomain.major
            };
            if (seekerDomain.Name == null)
                seeker.Name = accountFB.DisplayName;
            else
                seeker.Name = seekerDomain.Name;
            if (seekerDomain.Email == null)
                seeker.Email = accountFB.Email;
            else
                seeker.Email = seekerDomain.Email;
            if (seekerDomain.PhoneNumber == null)
            {
                seeker.Phone_Number = accountFB.PhoneNumber;
            }
            else
                seeker.Phone_Number = seekerDomain.PhoneNumber;
            if (seekerDomain.birthday.HasValue)
                seeker.Birthday = seekerDomain.birthday.Value.ToString("dd-MM-yyyy");
            return seeker;
        }

        public async Task<CV> CVUpdate(string userId, CV cV)
        {
            var seekerDomain = await _appDbContext.Seeker.FirstOrDefaultAsync(s => s.UID == userId);
            if (seekerDomain == null) return null!;
            if(!string.IsNullOrEmpty(cV.Name))
                seekerDomain.Name = cV.Name;
            if(!string.IsNullOrEmpty(cV.Email))
                seekerDomain.Email = cV.Email;
            if(!string.IsNullOrEmpty(cV.Phone_Number))
                seekerDomain.PhoneNumber = cV.Phone_Number;
            if (!string.IsNullOrEmpty(cV.Birthday))
            {
                seekerDomain.birthday = DateTime.Parse(cV.Birthday);
            }    
            if (!string.IsNullOrEmpty(cV.Address))
                seekerDomain.address = cV.Address;
            if(!string.IsNullOrEmpty(cV.Experience))
                seekerDomain.experience = cV.Experience;
            if(!string.IsNullOrEmpty(cV.Skills))
                seekerDomain.skills = cV.Skills;
            if(!string.IsNullOrEmpty(cV.Education))
                seekerDomain.education = cV.Education;
            if(!string.IsNullOrEmpty(cV.Major))
                seekerDomain.major = cV.Major;
            await _appDbContext.SaveChangesAsync();
            return cV;
        }

        public async Task<InforSeeker> Infor(string userId)
        {
            var seekerDomain = await _appDbContext.Seeker.FirstOrDefaultAsync(s => s.UID == userId);
            var seekerFB = await _firebaseAuth.GetUserAsync(userId);
            if (seekerDomain == null) return null!;
            var seekerInfor = new InforSeeker()
            {
                birthday = seekerDomain.birthday?.ToString("dd-MM-yyyy"),
                address = seekerDomain.address,
                photo = seekerFB.PhotoUrl ?? "https://i.ibb.co/TqjSRg0/th.jpg",
            };
            if (seekerDomain.Name == null)
                seekerInfor.name = seekerFB.DisplayName;
            else
                seekerInfor.name = seekerDomain.Name;
            if (seekerDomain.Email == null)
                seekerInfor.email = seekerFB.Email;
            else
                seekerInfor.email = seekerDomain.Email;
            if (seekerDomain.PhoneNumber == null)
                seekerInfor.phoneNumber = seekerFB.PhoneNumber;
            else
                seekerInfor.phoneNumber = seekerDomain.PhoneNumber;
            return seekerInfor;
        }

        public async Task<CV> InforApply(string userId)
        {
            if (Int32.TryParse(userId, out var temp))
            {
                var seekerDomain = await _appDbContext.Recruitment_No_Accounts.FirstOrDefaultAsync(s => s.recruitment_ID == temp);
                if (seekerDomain == null) return null!;
                return new CV()
                {
                    Name = seekerDomain.fullname,
                    Email = seekerDomain.email,
                    Phone_Number = seekerDomain.phone_number,
                    Birthday = seekerDomain.birthday!.Value.ToString("dd-MM-yyyy"),
                    Address = seekerDomain.address,
                    Education = seekerDomain.education,
                    Experience = seekerDomain.experience,
                    Major = seekerDomain.major,
                    Skills = seekerDomain.skills,
                    photo = "https://i.ibb.co/TqjSRg0/th.jpg"
                };
            }
            else
            {
                var seekerDomain = await _appDbContext.Seeker.Include(s => s.account).FirstOrDefaultAsync(s => s.UID == userId);
                var user = await GetUserDataFromFirebase(userId);
                if (seekerDomain == null) return null!;
                return new CV()
                {
                    Name = seekerDomain.Name,
                    Email = seekerDomain.Email,
                    Phone_Number = seekerDomain.PhoneNumber,
                    Birthday = seekerDomain.birthday!.Value.ToString("dd-MM-yyyy"),
                    Address = seekerDomain.address,
                    Education = seekerDomain.education,
                    Experience = seekerDomain.experience,
                    Major = seekerDomain.major,
                    Skills = seekerDomain.skills,
                    photo = user.PhotoUrl
                };
            }
        }

        private async Task<UserRecord> GetUserDataFromFirebase(string uid)
        {
            try
            {
                var userRecord = await _firebaseAuth.GetUserAsync(uid);
                return userRecord;
            }
            catch
            {
                return null!;
            }
        }
    }
}
