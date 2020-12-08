using MatrizConhecimento.Context;
using MatrizConhecimento.DTO;
using MatrizConhecimento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace MatrizConhecimento.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AccountController(IConfiguration configuration, AppDbContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_context.Users.Any(p => p.UserName == userDTO.UserName))
                    return BadRequest("Usuário já existe.");

                User user = _mapper.Map<User>(userDTO);

                _context.Users.Add(user);
                _ = await _context.SaveChangesAsync();
                CreateTopics(user);

                return Ok(GetToken(user));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Signin")]
        public ActionResult Signin([FromBody] SignInDTO signIn)
        {
            var result = _context.Users.Where(p => p.UserName == signIn.UserName && p.Password == signIn.Password);
            if (result.Count() == 1)
            {
                return Ok(GetToken(result.FirstOrDefault()));
            }
            else
                return Unauthorized("Usuário inválido.");
        }

        private UserToken GetToken(User user)
        {
            var _claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim("System", "user"),
                new Claim("UserName", user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            ///gera chave com base em algoritimo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["jwt:key"]));

            ///gera assinatura digital do token usandoo algoritimo Hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //define a expiração do token
            string expiracao = _configuration["TokenConfiguration:ExpireHours"];
            DateTime dtExpiracao = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: _claims,
                expires: dtExpiracao,
                signingCredentials: credenciais);

            var usuario = new UserToken();
            usuario.Authenticated = true;
            usuario.Token = new JwtSecurityTokenHandler().WriteToken(token);
            usuario.Expiration = dtExpiracao;
            usuario.Message = "Token JWT OK";
            usuario.UserId = user.Id;

            return usuario;
        }

        private async void CreateTopics(User user)
        {
            List<Topic> topics = new List<Topic>();
            topics.Add(new Topic()
            {
                Name = "Banco de Dados",
                UserId = user.Id,
                Active = true,
                Matters = new List<Matter>()
                {
                    new Matter(){
                        Name = "Mongo DB",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = "PostegreSQL",
                        UserId = user.Id,
                        Active = true
                    }

                }
            });

            topics.Add(new Topic()
            {
                Name = "BI",
                UserId = user.Id,
                Active = true,
                Matters = new List<Matter>()
                {
                    new Matter(){
                        Name = "PowerBI",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = "Tableau",
                        UserId = user.Id,
                        Active = true
                    }
                }
            });

            topics.Add(new Topic()
            {
                Name = "Arquitetura",
                UserId = user.Id,
                Active = true,
                Matters = new List<Matter>()
                {
                    new Matter(){
                        Name = "MVC",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = "SOA",
                        UserId = user.Id,
                        Active = true
                    }
                }
            });

            topics.Add(new Topic()
            {
                Name = "Análise de sistemas",
                UserId = user.Id,
                Active = true,
                Matters = new List<Matter>()
                {
                    new Matter(){
                        Name = "Levantamento de Requisitos",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = "Fluxogramas",
                        UserId = user.Id,
                        Active = true
                    }
                }
            });


            topics.Add(new Topic()
            {
                Name = "Desenvolvimento de Sistemas",
                UserId = user.Id,
                Active = true,
                Matters = new List<Matter>()
                {
                    new Matter(){
                        Name = "Javascript",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = ".Net Core",
                        UserId = user.Id,
                        Active = true
                    }
                }
            });


            topics.Add(new Topic()
            {
                Name = "Testes",
                UserId = user.Id,
                Active = true,
                Matters = new List<Matter>()
                {
                    new Matter(){
                        Name = "Testes Unitários",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = "Automação de Testes",
                        UserId = user.Id,
                        Active = true
                    }
                }
            });

            topics.Add(new Topic()
            {
                Name = "Infraestrutura",
                UserId = user.Id,
                Active = true,
                Matters = new List<Matter>()
                {
                    new Matter(){
                        Name = "Cloud",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = "Windows Server",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = "Linux",
                        UserId = user.Id,
                        Active = true
                    }
                }
            });

            topics.Add(new Topic()
            {
                Name = "Rede",
                UserId = user.Id,
                Active = true,
                Matters = new List<Matter>()
                {
                    new Matter(){
                        Name = "Firewall",
                        UserId = user.Id,
                        Active = true
                    },
                    new Matter(){
                        Name = "VPN",
                        UserId = user.Id,
                        Active = true
                    }
                }
            });

            _context.Topics.AddRange(topics);
            _ = await _context.SaveChangesAsync();

            foreach (Topic topic in topics)
            {
                foreach (Matter matter in topic.Matters)
                {
                    Rating rating = new Rating()
                    {
                        UserId = user.Id,
                        MatterId = matter.Id,
                        TopicId = topic.Id,
                        RatingDate = null,
                        RatingHistoryId = null,
                        Score = null
                    };
                    _context.Ratings.Add(rating);
                }
            }
            _ = await _context.SaveChangesAsync();
        }
    }
}
