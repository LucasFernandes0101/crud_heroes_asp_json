using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HeroController : ApiController
    {
        public IEnumerable<Hero> Get()
        {
            Hero heroi = new Hero();

            return heroi.listarHerois();
        }

        public Hero Get(int id)
        {
            Hero heroi = new Hero();

            return heroi.listarHerois().Where(x => x.id == id).FirstOrDefault();
        }

        public List<Hero> Post(Hero heroi)
        {
            heroi.Inserir(heroi);

            return heroi.listarHerois();
        }

        public List<Hero> Put(int id, [FromBody]Hero heroi)
        {
            heroi.Atualizar(id, heroi);

            return heroi.listarHerois();
        }

        public List<Hero> Delete(int id)
        {
            Hero heroi = new Hero();

            heroi.Deletar(id);

            return heroi.listarHerois();
        }
    }
}
