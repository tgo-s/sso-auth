using SSOAuth.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;

namespace SSOAuth.Rules
{
    public class CustomAuthorizationRoles : ICustomAuthorizationRoles
    {
        private ClaimsPrincipal userPrinciple;

        public CustomAuthorizationRoles(HttpContext context)
        {
            ClaimsPrincipal userPrinciple = context.User as ClaimsPrincipal;
            this.userPrinciple = userPrinciple;
        }
        /// <summary>
        /// Função para extração do código das unidades de um padrão de role do SSO
        /// </summary>
        /// <param name="matchPattern">Padrão regex para identificar as roles de unidade</param>
        /// <param name="replacePattern">Padrão regex para remover a parte do texto</param>
        /// <returns>Retorna um IEnumerable de inteiros, códigos das unidades encontrados.</returns>
        public IEnumerable<int> GetCodUnidadesUsuario(string matchPattern, string replacePattern)
        {
            IEnumerable<string> roles = userPrinciple.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            int[] unidades = roles.Where(s => Regex.IsMatch(s, @matchPattern)).Select(c => int.Parse(Regex.Replace(c, @replacePattern, ""))).ToArray();

            return unidades;
        }
        /// <summary>
        /// Método básico para verificação das permissões de acordo com o padrão Sanepar Single Sign-On
        /// </summary>
        /// <param name="permissoes">Array com as permissões a serem validadas</param>
        /// <param name="pattern">Regex do padrão da permissão que vai ser validada (Perfil, Item, etc). Ex.: para procurar as roles de perfis no padrão "sistema-sanepar_p_usuario" utiliza-se "_[Pp]_" como pattern.</param>
        /// <returns>Retorna se verdadeiro se encontrar pelo menos uma permissão</returns>
        public bool PossuiPermissoes(string[] permissoes, string pattern)
        {
            bool temPermissao = false;

            IEnumerable<Claim> roles = this.userPrinciple.Claims.Where(c => c.Type == ClaimTypes.Role && Regex.IsMatch(c.Value, @pattern)).Select(c => c);
            temPermissao = roles.Any(c => permissoes.Contains(c.Value));

            return temPermissao;
        }
    }
}
