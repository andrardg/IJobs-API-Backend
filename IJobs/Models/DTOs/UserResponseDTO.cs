using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Residence { get; set; }
        public string Occupation { get; set; }
        public string Studies { get; set; }
        public string CV { get; set; }
        public string Photo { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        public UserResponseDTO()
        {
        }
        public UserResponseDTO(User user)//, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PasswordHash = user.PasswordHash;
            Residence = user.Residence;
            Occupation = user.Occupation;
            Studies = user.Studies;
            CV = user.CV;
            Photo = user.Photo;

            //MemoryStream target = new MemoryStream(user.Photo);
            //IFormFile file = new FormFile(target, 0, user.Photo.Length, "Photo", "Photo" + user.Id.ToString());
            //Image img = Image.FromStream(target);
            //Console.WriteLine(file);

            //IFormFile file;
            //Image img = Image.FromStream(new MemoryStream(user.Photo));
            //IFormFile file;
            //var target = new MemoryStream(user.CV);
            //file = (IFormFile)System.Drawing.Image.FromStream(target);
            //Console.WriteLine(file);
            //if (user.CV != null)
            //{
            //    var target = new MemoryStream(user.CV);
            //    CVfile = new FormFile(target, 0, user.CV.Length, "CV", "CV" + user.Id.ToString());
            //}
            //if (user.Photo != null)
            //{
            //    var target = new MemoryStream(user.Photo);
            //    Photofile = new FormFile(target, 0, user.Photo.Length, "Photo", "Photo" + user.Id.ToString());
            //}
            Role = user.Role;
            //Token = token;
        }
    }
}
