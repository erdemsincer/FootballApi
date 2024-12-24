using FootballApi.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    internal interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetPlayersFromApiAsync();
    }
}
