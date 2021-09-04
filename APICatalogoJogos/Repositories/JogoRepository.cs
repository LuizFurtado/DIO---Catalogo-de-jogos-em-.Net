using APICatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("c7a93550-c39d-44fd-9156-10b9db7505d9"), new Jogo{ Id = Guid.Parse("c7a93550-c39d-44fd-9156-10b9db7505d9"), Nome = "Fifa 21", Produtora="EA", Preco = 200 } },
            {Guid.Parse("dceeb80d-0016-4c1d-ac8b-e004c3c93b85"), new Jogo{ Id = Guid.Parse("dceeb80d-0016-4c1d-ac8b-e004c3c93b85"), Nome = "Fifa 20", Produtora = "EA", Preco = 190 } },
            {Guid.Parse("c7574f38-b608-4e82-9224-0ccffbb1da23"), new Jogo{ Id = Guid.Parse("c7574f38-b608-4e82-9224-0ccffbb1da23"), Nome = "Fifa 19", Produtora = "EA", Preco = 180 } },
            {Guid.Parse("0591f8ec-8626-4e77-9752-f4251c72f334"), new Jogo{ Id = Guid.Parse("0591f8ec-8626-4e77-9752-f4251c72f334"), Nome = "Street Fighter V", Produtora = "Capcom", Preco = 80} },
            {Guid.Parse("7f81e4bc-8cab-44da-a44d-860673723cb3"), new Jogo{ Id= Guid.Parse("7f81e4bc-8cab-44da-a44d-860673723cb3"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 190} }
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id)) return null;

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values
                .Where(jogo => jogo.Nome.Equals(nome) 
                && jogo.Produtora.Equals(produtora))
                .ToList());
        }

        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach(var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);

            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Fechar conexão com banco de dados (quando for o caso)
        }
    }
}
