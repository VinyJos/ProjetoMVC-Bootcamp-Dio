using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Context;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers
{
    public class ContatoController : Controller
    {   
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }
        // Pagina que vai listar todos os contatos do banco.
        public IActionResult Index()
        {   
            var contatos = _context.Contatos.ToList();
            return View(contatos);
        }

        // aqui ele está como [HttpGet] mas é opcional colocar
        public IActionResult Criar()  
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Contato contato)
        {   
            // verificar se as informações se são válidas
            if(ModelState.IsValid)
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
        }
        // Método da View
        public IActionResult Editar(int id)
        {   
            // Buscar no banco esse contato, para ser editado
            var contato = _context.Contatos.Find(id);
            if (contato == null)
            {   
                // Redireciona para a página de listagem
                return RedirectToAction(nameof(Index));
            }
            // passar o contato para que ele possa aparecer na tela de edição
            return View(contato);
        }
        
        // Método para ação de editar.
        [HttpPost]
        public IActionResult Editar(Contato contato)
        {   
            // buscando no banco
            var contatoBanco = _context.Contatos.Find(contato.Id);

            // inputs editados
            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            // Fazendo o Update
            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Detalhes(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
        }

        public IActionResult Deletar(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
        }

        [HttpPost]
        public IActionResult Deletar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);
            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}