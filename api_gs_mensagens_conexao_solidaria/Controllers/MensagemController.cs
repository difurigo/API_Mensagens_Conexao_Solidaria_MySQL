using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_gs_mensagens_conexao_solidaria.Domain.Entities;
using api_gs_mensagens_conexao_solidaria.Infrastructure.Data;

namespace api_gs_mensagens_conexao_solidaria.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MensagemController : ControllerBase
{
    private readonly ApplicationContext _context;

    public MensagemController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<Mensagem>>> GetMensagensPorUsuario(string usuarioId)
    {
        return await _context.Mensagens.Where(m => m.UsuarioId == usuarioId).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Mensagem>> CriarMensagem(Mensagem mensagem)
    {
        _context.Mensagens.Add(mensagem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMensagensPorUsuario), new { usuarioId = mensagem.UsuarioId }, mensagem);
    }

    [HttpPut("{uuid}")]
    public async Task<IActionResult> EditarMensagem(string uuid, Mensagem mensagem)
    {
        var mensagemExistente = await _context.Mensagens.FirstOrDefaultAsync(m => m.UUID == uuid);
        if (mensagemExistente == null) return NotFound();

        // Atualiza os campos permitidos
        mensagemExistente.Titulo = mensagem.Titulo;
        mensagemExistente.Conteudo = mensagem.Conteudo;
        mensagemExistente.Prioridade = mensagem.Prioridade;
        mensagemExistente.Localizacao = mensagem.Localizacao;
        mensagemExistente.TTL = mensagem.TTL;
        mensagemExistente.Status = mensagem.Status;
        mensagemExistente.UsuarioId = mensagem.UsuarioId;

        _context.Mensagens.Update(mensagemExistente);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpDelete("{uuid}")]
    public async Task<IActionResult> ApagarMensagem(string uuid)
    {
        var msg = await _context.Mensagens.FirstOrDefaultAsync(m => m.UUID == uuid);
        if (msg == null) return NotFound();

        _context.Mensagens.Remove(msg);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("usuario/{usuarioId}")]
    public async Task<IActionResult> ApagarTodasDoUsuario(string usuarioId)
    {
        var mensagens = _context.Mensagens.Where(m => m.UsuarioId == usuarioId);
        _context.Mensagens.RemoveRange(mensagens);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}