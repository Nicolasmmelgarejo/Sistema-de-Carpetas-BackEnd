using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.data;
using backend.models;
using Microsoft.AspNetCore.Authorization;
using backend.Service;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public UserController(IConfiguration config,DataContext context)
        {
            _config = config;
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getuser()
        {
          if (_context.user == null)
          {
              return NotFound();
          }
          List<User> userList = await _context.user.ToListAsync();
          List<User_Role> userR= await _context.user_Roles.ToListAsync();
            for (int i = 0; i > userList.Count; i++)
            {
                for(int j=0;j>userR.Count;j++)
                {
                    if (userR[j].UserIdUser == userList[i].IdUser)
                    {
                        userList[i].User_Roles.Add(userR[j]);
                    }  

                }
            }
            return userList;
        }


        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.user == null)
          {
              return NotFound();
          }
            var user = await _context.user.FindAsync(id);
            List<User_Role> userR = await _context.user_Roles.ToListAsync();
            

            if (user == null)
            {
                return NotFound();
            }
            for (int i = 0; i > userR.Count; i++)
            {

                if (userR[i].UserIdUser == user.IdUser)
                {
                    user.User_Roles.Add(userR[i]);
                }
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }
            string name="";
            List<User_Role> userR = await _context.user_Roles.ToListAsync();
            List<User> oLsUser = await _context.user.ToListAsync();


            foreach (var oUserR in oLsUser.AsEnumerable())
            {
                if (user.IdUser == oUserR.IdUser)
                {
                    name = oUserR.UserName;
                }
            }
            for (int i = 0; i < userR.Count; i++)
            {

                if (userR[i].UserIdUser == user.IdUser)
                {
                    _context.user_Roles.Remove(userR[i]);
                    _context.SaveChanges();
                }
            }
            List<User> oLsUserList = await _context.user.ToListAsync();
            if (oLsUserList != null)
            {
                for(int i = 0; i < oLsUserList.Count; i++)
                {
                    if (name == oLsUserList[i].UserName)
                    {
                        _context.Remove(oLsUserList[i]);
                        _context.SaveChanges();
                    }
                }
                    
                
            }


            if (oLsUserList != null)
            {
                for (int i = 0; i < oLsUserList.Count; i++)
                {
                    if (user.UserName == oLsUserList[i].UserName)
                    {
                        return Conflict("Fail");
                    }
                }
            }
            user.IdUser = 0;
            _context.Add(user);

            try
            {
               _context.SaveChanges();
                return Ok("Success");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                   return NotFound();
                }
                else
                {
                   throw;
                }
            }
            
            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.user == null)
          {
              return Problem("Entity set 'DataContext.user'  is null.");
          }
          int bandera=0;
            List<User> userList = await _context.user.ToListAsync();
            for(int i = 0; i < userList.Count; i++)
            {
                if (user.UserName == userList[i].UserName){
                    bandera = 1;
                }   
            }
            if (bandera == 0)
            {
                _context.user.Add(user);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetUser", new { id = user.IdUser }, user);
                return Ok("Success");
            }
            else
            {
                
                return NotFound("Fail");
                bandera = 0;
            }
            
        }
        [AllowAnonymous]
        [HttpPost("validar")]
        public async Task<ActionResult<User>> ValidationUser(User user)
        {
            int bandera=0;
            List<User> userList = await _context.user.ToListAsync();
            List<User_Role> userR = await _context.user_Roles.ToListAsync();
            for (int i = 0; i < userList.Count; i++)
            {
                for (int j = 0; j < userR.Count; j++)
                {
                    if (userR[j].UserIdUser == userList[i].IdUser)
                    {
                        userList[i].User_Roles.Add(userR[j]);
                    }

                }
            }
            for(int i =0; i < userList.Count; i++)
            {
                if (user.UserName == userList[i].UserName && user.UserPassword == userList[i].UserPassword)
                {
                    bandera = 1;
                }
            }
            if (bandera == 0)
            {
                return NotFound("Fail");
            }
            else
            {
                bandera = 0;
                return Ok(new JwtService(_config).GenerateToken(
                        user.UserName,
                        user.UserPassword
                    )
                );
            }

        }


        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.user == null)
            {
                return NotFound();
            }
            var user = await _context.user.FindAsync(id);
            List<User_Role> userR = await _context.user_Roles.ToListAsync();
            
            if (user == null)
            {
                return NotFound();
            }

            for (int i = 0; i > userR.Count; i++)
            {

                if (userR[i].UserIdUser == user.IdUser)
                {
                    user.User_Roles.Add(userR[i]);
                }
            }

            _context.user.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.user?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
