using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SindautoHub.Application.Dtos;
using SindautoHub.Application.Interface;

using SindautoHub.Domain.Entities.Models;
using SindautoHub.Domain.Interface;


namespace SindautoHub.Application.Services;

public class NotificacaoService : INotificacaoServices
{
    private readonly IAnnouncementsRepository _notificacaoRepository;
    private readonly IunitOfwork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificacaoService(
        IAnnouncementsRepository notificacaoRepository,
        IunitOfwork unitOfWork,
        IMapper mapper)
    {
        _notificacaoRepository = notificacaoRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<NotificacaoResponse> CreateAsync(CreateNotificacaoRequest createRequest)
    {
        var notificacaoEntity = _mapper.Map<Announcements>(createRequest);
        await _notificacaoRepository.CreateAsync(notificacaoEntity);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<NotificacaoResponse>(notificacaoEntity);
    }

    public async Task<bool> DeleteAsync(Guid notificacaoId)
    {
        var notificacao = await _notificacaoRepository.GetByIdAsync(notificacaoId);
        if (notificacao is null)
        {
            return false;
        }

        await _notificacaoRepository.DeleteAsync(notificacao);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<NotificacaoResponse>> GetAllByUsuarioIdAsync(Guid usuarioId)
    {
        var notificacoes = await _notificacaoRepository.GetAllAsync(usuarioId);
        return _mapper.Map<IEnumerable<NotificacaoResponse>>(notificacoes);
    }

    public async Task<NotificacaoResponse> GetByIdAsync(Guid notificacaoId)
    {
         var notificacao = await _notificacaoRepository.GetByIdAsync(notificacaoId);
            return _mapper.Map<NotificacaoResponse>(notificacao);
    }

    public async Task<bool> MarkAllAsReadAsync(Guid usuarioId)
    {
        var notificacoes = await _notificacaoRepository.GetAllAsync(usuarioId);
        if (!notificacoes.Any())
        {
            return true;
        }

        foreach (var notificacao in notificacoes)
        {
            if (!notificacao.Lida)
            {
                notificacao.Lida = true;
            }
        }

        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkAsReadAsync(Guid notificacaoId)
    {
        var notificacao = await _notificacaoRepository.GetByIdAsync(notificacaoId);
        if (notificacao is null)
        {
            return false;
        }

        notificacao.Lida = true;
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}