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
using System.Configuration;
using System.Collections.Specialized;

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

        public TokenEntity GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            var tokendomain = new Token
            {
                userId = userId,
                authToken = token,
                issuedOn = issuedOn,
                expiresOn = expiredOn
            };

            _unitOfWork.TokenRepository.Insert(tokendomain);
            _unitOfWork.Save();
            var tokenModel = new TokenEntity()
            {
                userId = userId,
                issuedOn = issuedOn,
                expiresOn = expiredOn,
                authToken = token
            };

            return tokenModel;
        }

        public bool ValidateToken(string tokenId)
        {
            var token = _unitOfWork.TokenRepository.Get(t => t.authToken == tokenId && t.expiresOn > DateTime.Now);
            if (token != null && !(DateTime.Now > token.expiresOn))
            {
                token.expiresOn = token.expiresOn.AddSeconds(
                                              Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
                _unitOfWork.TokenRepository.Update(token);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public bool Kill(string tokenId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.authToken == tokenId);
            _unitOfWork.Save();
            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.authToken == tokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }

        public bool DeleteByUserId(int userId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.userId == userId);
            _unitOfWork.Save();

            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.userId == userId).Any();
            return !isNotDeleted;
        }



        /*
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
        }*/
    }
}
