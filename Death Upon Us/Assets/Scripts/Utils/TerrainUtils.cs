using UnityEngine;

namespace utils   
{
    public class TerrainUtils{
        private enum CURRENT_TERRAIN { GRASS, DIRT, WOOD };

        public int surfaceIndex = 0;
        
        private Terrain terrain;
        private TerrainData terrainData;
        private Vector3 terrainPos;
    
        // Use this for initialization
        void Start () {
        
            terrain = Terrain.activeTerrain;
            terrainData = terrain.terrainData;
            terrainPos = terrain.transform.position;
        }
        
        
        void OnGUI () {
            GUI.Box(new Rect( 100, 100, 200, 25 ), "index: "+surfaceIndex.ToString()+", name: "+terrainData.splatPrototypes[surfaceIndex].texture.name);
        }

        int GetClosestCurrentTerrain(Vector3 playerPos, Terrain[] terrains)
        {
            //Get the closest one to the player
            var center = new Vector3(terrains[0].transform.position.x + terrains[0].terrainData.size.x / 2, playerPos.y, terrains[0].transform.position.z + terrains[0].terrainData.size.z / 2);
            float lowDist = (center - playerPos).sqrMagnitude;
            var terrainIndex = 0;

            for (int i = 0; i < terrains.Length; i++)
            {
                center = new Vector3(terrains[i].transform.position.x + terrains[i].terrainData.size.x / 2, playerPos.y, terrains[i].transform.position.z + terrains[i].terrainData.size.z / 2);

                //Find the distance and check if it is lower than the last one then store it
                var dist = (center - playerPos).sqrMagnitude;
                if (dist < lowDist)
                {
                    lowDist = dist;
                    terrainIndex = i;
                }
            }
            return terrainIndex;
        }
        
        private float[] GetTextureMix(Vector3 WorldPos){
            Terrain[] terrains = Terrain.activeTerrains;
            terrain = terrains[GetClosestCurrentTerrain(WorldPos, terrains)];
            terrainData = terrain.terrainData;
            terrainPos = terrain.transform.position;

            int mapX = (int)(((WorldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
            int mapZ = (int)(((WorldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

            // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
            float[,,] splatmapData = terrainData.GetAlphamaps( mapX, mapZ, 1, 1 );
            
            // extract the 3D array data to a 1D array:
            float[] cellMix = new float[ splatmapData.GetUpperBound(2) + 1 ];
            
            for(int n=0; n<cellMix.Length; n++){
                cellMix[n] = splatmapData[ 0, 0, n ];
            }
            return cellMix;
        }
        
        private int GetMainTexture(Vector3 WorldPos){
            // returns the zero-based index of the most dominant texture
            // on the main terrain at this world position.
            float[] mix = GetTextureMix(WorldPos);
            
            float maxMix = 0;
            int maxIndex = 0;
            
            // loop through each mix value and find the maximum
            for(int n=0; n<mix.Length; n++){
                if ( mix[n] > maxMix ){
                    maxIndex = n;
                    maxMix = mix[n];
                    }
            }
            return maxIndex;
        }

        private bool insideHouse(Vector3 pos){
            Collider[] housesInRange = Physics.OverlapSphere(pos, 50, LayerMask.GetMask("Houses"));
            foreach (Collider house in housesInRange){
                if (house.bounds.Contains(pos))
                    return true;
            }
            return false;
        }

        public int SelectFootstep(Vector3 pos)
        {     
            FMOD.Studio.EventInstance snapshot;
            if (insideHouse(pos)){
                //update snapshot for indoors
                snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Indoors");
                snapshot.start();
                //wood
                return 2;
            }
            snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Outdoors");
            snapshot.start();

            int texture = GetMainTexture(pos);
            switch (texture)
            {
                //grass
                case 0:
                    return 1;
                //dirt
                case 1:
                    return 0;
                default:
                    return 0;
            }
        }
    }
}