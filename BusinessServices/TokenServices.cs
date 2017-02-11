using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    public class TokenServices : ITokenServices
    {
        private readonly UnitOfWork _unitOfWork;

        public TokenServices(UnitOfWork unitOfWork)
        {
            //_unitOfWork = new UnitOfWork();
            _unitOfWork = unitOfWork;
        }

        IMapper mapper = new MapperConfiguration(cfg => {
            cfg.CreateMap<Token, TokenEntity>();
        }).CreateMapper();
        public TokenEntity GetTokenById(int tokenId)
        {

            var token = _unitOfWork.TokenRepository.GetByID(tokenId);
            if (token != null)
            {
                var tokenModel = mapper.Map<Token, TokenEntity>(token);
                return tokenModel;
            }
            return null;
        }

        public IEnumerable<TokenEntity> GetAllTokens()
        {
            var tokens = _unitOfWork.TokenRepository.GetAll().ToList();
            if (tokens.Any())
            {
                var tokensModel = mapper.Map<List<Token>, List<TokenEntity>>(tokens);
                return tokensModel;
            }
            return null;
        }

        public int CreateToken(TokenEntity tokenEntity)
        {
            using (var scope = new TransactionScope())
            {
                var token = new Token()
                {
                    userId = tokenEntity.userId,
                    authToken = tokenEntity.authToken,
                    issuedOn=tokenEntity.issuedOn,
                    expiresOn=tokenEntity.expiresOn                 
                };
                _unitOfWork.TokenRepository.Insert(token);
                _unitOfWork.Save();
                scope.Complete();
                return token.tokenId;
            }
        }

        public bool UpdateToken(int tokenId, TokenEntity tokenEntity)
        {
            var success = false;
            if (tokenEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var token = _unitOfWork.TokenRepository.GetByID(tokenId);
                    if (token != null)
                    {
                        token.userId = tokenEntity.userId;
                        token.authToken = tokenEntity.authToken;
                        token.issuedOn = tokenEntity.issuedOn;
                        token.expiresOn = tokenEntity.expiresOn;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DeleteToken(int tokenId)
        {
            var success = false;
            if (tokenId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var token = _unitOfWork.TokenRepository.GetByID(tokenId);
                    if (token != null)
                    {
                        _unitOfWork.TokenRepository.Delete(token);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
