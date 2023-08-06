# AtominaCraftV2
A sort of remake of minecraft but with mods built into it (like atomic science, AE1, thermal expansion, etc) :))
uses OpenTK and C#

# AtominaCraft.cs contains the update and render loop stuff
and is also where chunks are generated. atm, there's only 9 generated
## Mesh generators
BlockMeshGenerator.cs is what turns all the blocks in a chunk into a collection of "CubeMesh"es. it uses the surrounding area to generate a cube mesh and writes it into the "CubeMesh" class, then it's rendered. its quite wasteful, considering there's 1000s of blocks loaded into VRAM but i could just generate a chunk's blocks and then copy all of that data into a "ChunkMesh" class that would contains 100000s of vertices... but i tried that and it didnt work very well so rip :( 

# blocks n stuff
Block.cs is a block... you place Block instances.

The world is split up into chunks using a dictionary key'd to a ChunkLocation, and the chunks are split up into blocks using a dictionary key'd to a BlockLocation, 

then there's the WorldMeshMap.cs class, which contains a dictionary where the key is a chunk, and the value is another dictionary, and that dictionary's key is a block, and the value is a CubeMesh. this is where generated cube meshes are stored

im only really doing this for fun lol... idk if it will ever be anywhere near as good at minecraft, and it will probabyl never support multiplayer but maybe it will...

probably wont be continuing this, mainly because i build it around blocks being instances, rather than singletons (like in actual java minecraft), which uses way less memory. and also because i'm loading all textures into the GPU, rather than stiching them together into a giant texture atlas thing (like that java minecraft does) and then just drawing textures with an offset in that atlas
