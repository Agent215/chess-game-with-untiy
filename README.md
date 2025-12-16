# Chess Game (Unity)

A work-in-progress chess prototype built in Unity 2020.3.8f1. The project currently focuses on board setup, piece movement/validation, and simple turn handling without a complete player UI.

## Project overview
- **Board generation & setup**: `GameBoard` builds an 8×8 board, names squares (e.g., A1–H8), and spawns pieces from `Assets/src/Config/defaultGame.txt` using the shared `GamePiece` prefab and individual 3D models. Pieces are parented to their squares and track occupancy via `GameSquare`.【F:Assets/src/GameBoard.cs†L27-L128】【F:Assets/src/Config/defaultGame.txt†L1-L32】
- **Piece logic**: `GamePiece` delegates move validation to handlers in `PieceEventHandlers`, removes captured pieces through `GameBoard`, and advances turns via `GameControl.turnOver()`. A per-piece outline selection helper lives in `Assets/src/UI/OutlineSelection.cs`.【F:Assets/src/GamePiece.cs†L17-L120】
- **Turn handling**: `GameControl` flips between white/black turns, toggles piece selectability tags, and checks for checkmate-like conditions by verifying opponent king mobility and threats. This logic currently runs without a full in-game UI flow.【F:Assets/src/GameControl.cs†L12-L96】
- **Scenes**: `Assets/Scenes/SampleScene.unity` wires together the board pivot, control scripts, prefabs, and outline selection.

## Getting started
1. Install Git LFS and pull large assets:
   ```bash
   git lfs install
   git lfs checkout
   ```
2. Open the project in **Unity 2020.3.8f1** (matching `ProjectSettings/ProjectVersion.txt`).【F:ProjectSettings/ProjectVersion.txt†L1-L2】
3. Open `Assets/Scenes/SampleScene.unity`, press **Play**, and use the existing test scripts/inspector to trigger moves (UI is not yet implemented).

## Key folders
- `Assets/ChessSet` and `Assets/prefabs`: models and prefabs for board/pieces.
- `Assets/src`: gameplay code.
  - `GameBoard.cs`, `GamePiece.cs`, `GameControl.cs`: core game state and turn flow.
  - `GameSquare/`: square state + collision tagging.
  - `UI/OutlineSelection.cs`: highlights and tracks the currently selected object.
  - `Tests/GamePieceTestScript.cs`: quick play-mode helper to exercise move logic.
  - `Config/defaultGame.txt`: starting layout.
- `Assets/QuickOutline`: third-party outline shader used by selection visuals.

## Current limitations / next steps
- Player UI for selecting pieces and issuing moves is minimal; interactions rely on inspector/test scripts.
- Special rules (en passant, castling, pawn promotion, check detection edge cases, multiplayer/AI) are incomplete or marked TODO.
- Scene wiring and prefabs are evolving; expect to adjust tags (`Selectable`, `UnSelectable`) and references when adding UI.

Contributions are welcome—feel free to branch from `main` and open a PR.
