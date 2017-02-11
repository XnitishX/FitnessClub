using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessServices
{
    public interface ITokenServices
    {
        TokenEntity GetTokenById(int tokenId);
        IEnumerable<TokenEntity> GetAllTokens();
        int CreateToken(TokenEntity tokenEntity);
        bool UpdateToken(int tokenId, TokenEntity tokenEntity);
        bool DeleteToken(int tokenId);
    }
}
