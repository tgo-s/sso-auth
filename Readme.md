### Guia SSOAuth - Keycloak Single Sign-On MVC .NET Framework 4.7 ###

Biblioteca criada para facilitar a implementação da autenticação Single Sign-On utilizando Keycloak

### Configuração ###

Criar as seguintes entradas no web.config da aplicação dentro de <appSettings> substituindo os dados do atributo value com dados válidos.

```xml
<appSettings>
	<add key="SSO.Realm" value="exemplo-realm" />
    <add key="SSO.ClientID" value="exemplo-app" />
    <add key="SSO.KeycloakUrl" value="https://servidor.com.br:8543/auth/" />
    <add key="SSO.PersistentAuthType" value="keycloak_cookie" />
</appSettings>
```

Em seguida deve-se adicionar o atributo [Authorize] sob o controller ou actions que se deseja autenticar.

É aconselhado criar sua propria impelentação do atributo de autorização extendendo a classe System.Web.Mvc.AuthorizeAttribute e implementando no mínimo os métodos:

- protected bool AuthorizeCore(HttpContextBase httpContext);
- protected void HandleUnauthorizedRequest(AuthorizationContext filterContext);

Desta forma, é feita a verificação correta das roles criadas para o sistema e o correto redirecionamento para a página de acesso negado.

### Extra ###

A biblioteca oferece ainda a Classe CustomAuthorizationRoles, contendo os métodos básicos:

- PossuiPermissoes(string[] permissoes, string pattern, EnumTipoAcesso tipoAcesso)
-- Procura as roles do sistema de um determinado tipo de acesso que correspondam a um padrão (pattern) Regex

- public IEnumerable<int> GetCodUnidadesUsuario(string matchPattern, string replacePattern)
-- Semelhante ao método acima, retorna um IEnumerable de inteiro com o código das unidades. Deve-se entrar com o padrão de procura e o padrão para replace do Regex.