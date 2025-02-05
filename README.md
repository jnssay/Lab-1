# Lab-1

### Gameplay Concept: Metroidvania

There are certain areas that are out of reach, and certain objects (pipes) which cannot be interacted with at first. Idea is for players to progress by finding key items around the map which unlocks abilities for them to traverse better.

Only one ability (wrench + pipes) is implemented here, and the pipes currently lead nowhere. The unreachable platforms at the start of the game would suggest some sort of double jump ability to get up.

### Player Movement  

I chose to go with snappy movement similar to modern platformers, choosing values that would eliminate drag and momentum physics. Personal preferences.

### Stomp Feedback

Small backwards hop upon successful stomping of the Goomba, as well as the Goomba disappearing. More obvious feedback to players when a successful stomp has occurred.

### Enemies

Multiple enemies with randomised patrol patterns. The reset puts them back to the same starting locations each time, but their movement patterns are always randomised. 

### Map Traversal

Camera movement following the player, and borders on the edge of the map to prevent players from going out of bounds. The map design closely mimics World 1-1.

### One-way Colliders
 I found the pipe platforms to be challenging when implementing the colliders that would allow me to pass through one way when jumping up from below. I kept running into an issue where I would still be stopped by the collider if I went into the pipe at a specific height and a 90 degree angle, and eventually I had to do some tweaks with the arc angle to fix that

### UI

Basic UI. Default scoring and reset systems.

### Pink

It's my favourite colour.

