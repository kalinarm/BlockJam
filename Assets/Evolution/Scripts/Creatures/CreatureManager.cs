using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class CreatureManager : MonoBehaviour
    {
        static List<Creature> creatures = new List<Creature>();
        static Dictionary<int, List<Creature>> creaturesByTeam = new Dictionary<int, List<Creature>>();

        public static List<Creature> Creatures { get => creatures; }
        public static Dictionary<int, List<Creature>> CreaturesByTeam { get => creaturesByTeam; }

        public GameData gameData;

        public static void RegisterCreature(Creature c)
        {
            creatures.Add(c);

            List<Creature> list;
            int teamIndex = c.team.teamIndex;
            if (!creaturesByTeam.TryGetValue(teamIndex, out list))
            {
                list = new List<Creature>(10);
                creaturesByTeam.Add(teamIndex, list);
            }
            list.Add(c);
        }
        public static void UnregisterCreature(Creature c)
        {
            creatures.Remove(c);

            if (creaturesByTeam.TryGetValue(c.team.teamIndex, out List<Creature> list))
            {
                list.Remove(c);
                if (list.Count == 0) creaturesByTeam.Remove(c.team.teamIndex);
            }
        }

        public static List<Creature> GetCreaturesNear(Vector3 position, float range)
        {
            return creatures.Where(x => (x.transform.position - position).sqrMagnitude < range * range).ToList();
        }
        public static List<Creature> GetEnemyNear(Creature c, float range)
        {
            return creatures
                .Where(x => x != c && !x.IsDead() && !x.IsSameTeam(c))
                .Where(x => (x.transform.position - c.transform.position).sqrMagnitude < range * range)
                .ToList();
        }
        public static List<Creature> GetEnemyNearSorted(Creature c, float range)
        {
            return GetEnemyNear(c, range)
                .OrderBy(x => (x.transform.position - c.transform.position).sqrMagnitude)
                .ToList();

        }
    }
}

