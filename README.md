# Roguelike Dungeon Crawler
## Overview
This project is a Roguelike Dungeon Crawler developed as part of a Software Architecture course. The focus of the project is not only gameplay, but also the design and implementation of a scalable, modular, and maintainable architecture.
The game features procedurally generated dungeons, turn-based combat, and permadeath, following classic roguelike principles.

## Gameplay
- Fight enemies in real-time combat
- Collect loot, upgrades, and resources
- Manage health and inventory carefully
- Death is permanent (permadeath)

## Architecture Goals
This project emphasizes strong architectural design:
- Modularity – Components are loosely coupled and reusable
- Separation of Concerns – Clear division between systems (UI, logic, data)
- Scalability – Easy to extend with new features (enemies, items, levels)
- Maintainability – Clean, readable, and testable code
- Flexibility – Systems designed for future changes

## Core Systems
1. Game Loop -> Handles progression, Processes player and enemy actions
2. Procedural Generation -> Enemy and loot distribution
4. Combat System -> Damage calculation, Health and status tracking
5. Entity System -> Player and enemies share a common structure, Supports extensibility for new entity types
6. Inventory System -> Item collection and usage, Equipment and consumables

## Tech Stack
Language: C#
Engine/Framework: Unity
Architecture Style:
- Layered Architecture
- Component-Based Design

## Learning Outcomes
- Applying software architecture principles in a game context
- Designing modular systems
- Managing complexity in a growing codebase
- Understanding trade-offs in architectural decisions
