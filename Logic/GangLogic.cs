using System.Collections.Generic;
using Data.Repositorys;
using Model;

namespace Logic
{
    public class GangLogic
    {
        private static GangRepository _gangRepo = new GangRepository();

        public List<Gang> GetGangs()
        {
            return _gangRepo.Gangs;
        }

        public IEnumerable<Player> GetPlayerInGang(int gangId)
        {
            return _gangRepo.GetPlayerInGang(gangId);
        }

        public void AddPlayerToGang(int playerId, int gangId)
        {
            _gangRepo.AddPlayerToGang(playerId, gangId);
        }

        public void RemoverPlayerFromGang(int playerId, int gangId)
        {
            _gangRepo.RemovePlayerFromGang(playerId, gangId);
        }

        public List<Player> GetPlayersWithoutGang()
        {
            return _gangRepo.GetPlayersWithoutGang();
        }

        public Gang GetGangWithId(int id)
        {
            Gang gang = _gangRepo.GetGangWithId(id);
            return gang;
        }

        public void UpdateGang(Gang gang)
        {
            _gangRepo.UpdateGang(gang);
        }

        public void RemoveGang(int id)
        {
            _gangRepo.RemoveGang(id);
        }

        public void CreateGang(Gang gang)
        {
            _gangRepo.CreateGang(gang);
        }
    }
}