using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Employer_Repository : IEmployer_Repository
    {
        private readonly AppDbContext _appDbContext;
        private readonly FirebaseAuth firebaseAuth;

        public Employer_Repository(AppDbContext appDbContext, FirebaseApp firebaseApp)
        {
            _appDbContext = appDbContext;
            firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        public async Task<EmployerDTO> Get(string userId)
        {
            var employerDomain = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            var employerFB = await firebaseAuth.GetUserAsync(userId);
            if (employerDomain == null) { return null!; }
            var employer = new EmployerDTO
            {
                employer_name = employerDomain.employer_name,
                contact_phone = employerDomain.contact_phone,
                employer_website = employerDomain.employer_website,
                employer_address = employerDomain.employer_address,
                employer_about = employerDomain.employer_about,
                employer_image = employerDomain.employer_image ?? "https://i.ibb.co/qdz9N2N/FJ.png",
                image_cover = employerDomain.employer_image_cover ?? "https://i.ibb.co/d0mPWbw/Untitled-design.png"
            };
            if (employerDomain.email == null)
                employer.email = employerFB.Email;
            else
                employer.email = employerDomain.email;
            return employer;
        }

        public async Task<Image> Image(string userId, Image image)
        {
            var employerImage = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if (employerImage == null) { return null!; }
            if (!string.IsNullOrEmpty(image.image))
                employerImage.employer_image = image.image;
            await _appDbContext.SaveChangesAsync();
            return image;
        }

        public async Task<Cover> ImageCover(string userId, Cover cover)
        {
            var employerImage = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if (employerImage == null) { return null!; }
            if (!string.IsNullOrEmpty(cover.imageCover))
                employerImage.employer_image_cover = cover.imageCover;
            await _appDbContext.SaveChangesAsync();
            return cover;
        }

        public async Task<InforEmployer> Infor(string userId, InforEmployer inforEmployer)
        {
            var employerInfor = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if(employerInfor == null) { return null!; }
            if (!string.IsNullOrEmpty(inforEmployer.Name))
                employerInfor.employer_name = inforEmployer.Name;
            if (!string.IsNullOrEmpty(inforEmployer.Email))
                employerInfor.email = inforEmployer.Email;
            if (!string.IsNullOrEmpty(inforEmployer.Phone))
                employerInfor.contact_phone = inforEmployer.Phone;
            if (!string.IsNullOrEmpty(inforEmployer.Website))
                employerInfor.employer_website = inforEmployer.Website;
            if (!string.IsNullOrEmpty(inforEmployer.Address))
                employerInfor.employer_address = inforEmployer.Address;
            if (!string.IsNullOrEmpty(inforEmployer.About))
                employerInfor.employer_about = inforEmployer.About;
            await _appDbContext.SaveChangesAsync();
            return inforEmployer;
        }        
    }
}
