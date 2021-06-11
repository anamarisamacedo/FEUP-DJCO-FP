using UnityEngine;

namespace utils   
{
    public static class TerrainUtils{

        private enum CURRENT_TERRAIN { GRASS, DIRT, WOOD };

        private static CURRENT_TERRAIN DetermineTerrain(Vector3 pos){
            RaycastHit[] hit;

            hit = Physics.RaycastAll(pos, Vector3.down, 10.0f);

            foreach (RaycastHit rayhit in hit)
            {
                if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Gravel"))
                {
                    return CURRENT_TERRAIN.DIRT;
                }
                else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Wood"))
                {
                    return CURRENT_TERRAIN.WOOD;
                }
                else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
                {
                    return CURRENT_TERRAIN.GRASS;
                }
            }
            return CURRENT_TERRAIN.GRASS;
        }

        public static int SelectFootstep(Vector3 pos)
        {     
            CURRENT_TERRAIN currentTerrain = DetermineTerrain(pos);
            switch (currentTerrain)
            {
                case CURRENT_TERRAIN.DIRT:
                    return 0;

                case CURRENT_TERRAIN.GRASS:
                    return 1;

                case CURRENT_TERRAIN.WOOD:
                    return 2;

                default:
                    return 0;
            }
        }
    }
}