using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SindautoHub.Domain.Entities.Models;

namespace SindautoHub.Application.Interface
{
    public interface ITokenService
    {
        string  GenerateToken(User funcionario);

    }
}
