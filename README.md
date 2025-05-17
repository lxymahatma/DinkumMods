# Mods Made for Dinkum

## Mod List

| Mod Name                               | Simple Description                                | Author | Version |
|----------------------------------------|---------------------------------------------------|--------|---------|
| [SaveLoadPosition](#saveload-position) | Save and load your position in game using command | lxy    | 1.0.0   |
| [Sprint](#sprint)                      | Allows you to sprint (run faster)                 | lxy    | 1.1.0   |
| [Teleport](#teleport)                  | Many teleport functions using command             | lxy    | 1.0.0   |
| [TeleportHome](#teleport-home)         | Teleport to your home location using shortcut key | lxy    | 1.2.0   |

## SaveLoad Position

### Usage

**Commands (Chat Trigger: sl)**

1. s(ave) `slotname` - Save your current position to a slot.
2. l(oad) `slotname` - Load your position from a slot.
3. list - List all saved slots.

## Sprint

### Configurations

1. **SprintBoostMultiplier**: The multiplier for the sprint speed. Default is `2.0`.
2. **SprintKey**: The key to sprint. Default is `LeftControl`.

## Teleport

### Configurations

**TeleportInsideHouse**: Teleport inside the house when teleporting to home and mine. Default is `true`.

### Usage

**Commands (Chat Trigger: tp)**

1. f(riend) `friendname` - Teleport to a friend.
2. p(osition) `x y [z]` - Teleport to a specific position (z is optional and means the height, default z is 5f).
3. h(ome) - Teleport to home.
4. m(ine) - Teleport to mine.

## Teleport Home

### Configurations

**TeleportHomeKey**: The key to teleport home. Default is `H`.
