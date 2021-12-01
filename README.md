# ohrwurm
Github Game Off 2021 entry


https://itch.io/jam/game-off-2021

https://philparzer.itch.io/ohrwurm

## DESCRIPTION
- hotseat multiplayer
- player 1 needs to place obstacles
- if player 2 makes it to the ear they win

## BUGS
Spraycan, oil and lotion sadly don't work due to a deprecated prefab being instantiated
- oil speeds the player up (outdated prefab)
- lotion slows the player down (outdated prefab)
- spraycan particles freeze the player for 5secs (outdated prefab, serializefield on player gameobject unpopulated)
