using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Hero
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }
        public string arma { get; set; }
        public string identidade { get; set; }

        public List<Hero> listarHerois()
        {
            var caminho = HostingEnvironment.MapPath(@"~/App_Data/Base.json");

            var json = File.ReadAllText(caminho);

            List<Hero> listaHerois = JsonConvert.DeserializeObject<List<Hero>>(json);

            return listaHerois;
        }
        public bool reescreverLista(List<Hero> heroes)
        {
            var caminho = HostingEnvironment.MapPath(@"~/App_Data/Base.json");

            var json = JsonConvert.SerializeObject(heroes, Formatting.Indented);

            File.WriteAllText(caminho, json);

            return true;
        }
        public void Inserir(Hero heroi)
        {
            var listaHerois = this.listarHerois();

            var maxId = listaHerois.Max(hero => hero.id);

            heroi.id = (maxId + 1);

            listaHerois.Add(heroi);

            reescreverLista(listaHerois);
        }
        public string Atualizar(int id, Hero hero)
        {
            var listaHerois = this.listarHerois();

            var index = listaHerois.FindIndex(x => x.id == id);

            if (index >= 0)
            {
                hero.id = id;
                listaHerois[id - 1] = hero;
            }
            else
            {
                return "Erro, digite um ID maior ou igual a 0.";
            }
            reescreverLista(listaHerois);

            return hero.nome + " atualizado com sucesso!";
        }
        public string Deletar(int id)
        {
            List<Hero> listaHerois = listarHerois();

            string nomeHeroi;

            if (id <= listaHerois.Max(x => x.id))
            {
                listaHerois.RemoveAt(listaHerois.FindIndex(x => x.id == id));
                nomeHeroi = listaHerois[id].nome;
            }
            else
            {
                return "Id inexistente!";
            }
            reescreverLista(listaHerois);                                                                          

            return nomeHeroi + " deletado com sucesso!";
        }
    }
}