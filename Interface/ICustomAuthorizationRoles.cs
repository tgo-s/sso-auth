using SSOAuth.Enum;
using System;
using System.Collections.Generic;
using System.Text;


namespace SSOAuth.Interface
{
    public interface ICustomAuthorizationRoles
    {
        IEnumerable<int> GetCodUnidadesUsuario(string matchPattern, string replacePattern);
        bool PossuiPermissoes(string[] permissoes, string regexPattern);
    }
}
