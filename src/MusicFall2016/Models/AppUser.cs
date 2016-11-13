using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicFall2016.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime DateJoined { get; set; }
    }
}
