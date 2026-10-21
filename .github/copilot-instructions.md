# GitHub Copilot Instructions for "Different Egg Sizes" Mod

## Overview and Purpose

**Mod Name**: Different Egg Sizes  
**Author**: Mlie  
**PackageId**: Mlie.DifferentEggSizes  

"Different Egg Sizes" is a RimWorld mod that dynamically adjusts the size and nutritional value of all eggs based on the body size of the animal that lays them. This mod is inspired by the original "Egg Size Matters" mod by Simmins, requested by Kayedon. The primary goal is to achieve universal compatibility with all mods and vanilla content without requiring specific patches for each animal type. The chicken is used as the baseline for calculations regarding nutrient content, mass, and maximum hit points of eggs.

## Key Features and Systems

- **Automatic Scaling**: All eggs are scaled according to the body size of their respective egg-laying animals relative to a chicken, without additional patches required.
- **Compatibility**: Works with all types of eggs, supporting both vanilla and modded animals seamlessly.
- **Mod Settings**: Includes customization options where players can set the maximum and minimum values for egg attributes.
- **Post-Def Loading**: Modifications occur after all definitions have been fully loaded, ensuring compatibility irrespective of mod load order.

## Coding Patterns and Conventions

- **C# Principles**: The mod is coded following object-oriented practices, utilizing classes and methods appropriate for encapsulating mod functionalities.
- **Naming Conventions**: The codebase uses PascalCase for class names and methods, while following standard C# conventions such as camelCase for local variables and parameters.
- **File Structure**: Each major functionality is separated into its own file:  
  - `DifferentEggSizesMod.cs`: Primary mod logic and settings interface  
  - `DifferentEggSizesSettings.cs`: Handles mod settings persistence  
  - `Main.cs`: Core logic for dynamic egg size adjustments

## XML Integration

- While this mod currently does not directly alter XML files, it is designed to work in conjunction with XML definitions by dynamically altering values programmatically after definitions are loaded.
- Ensure any XML-based mods or definitions that potentially influence egg attributes load before this mod, although it should naturally resolve load order through post-definition updates.

## Harmony Patching

- Harmony is not explicitly used in the current iteration of this mod; however, it is a recommended approach for future enhancements or potential conflicts with other mods that might alter egg-related functionalities.
- Consider employing Harmony to patch specific methods or classes in the RimWorld assembly to expand or modify behaviors directly rather than through deferred dynamic adjustments.

## Suggestions for Copilot

Here are some helpful suggestions to ensure smooth usage of GitHub Copilot in this modding project:

1. **Dynamic Calculations**: Leverage Copilot's ability to suggest arithmetic operations and logic for calculations related to body size and scaling of egg attributes.
2. **Robust Testing Hooks**: Implement testing and debugging stubs for settings and calculations that Copilot can expand or refine with ease.
3. **Settings UI Code**: Utilize Copilot for generating UI-related code snippets to improve interaction interfaces within the mod settings menu.
4. **Simplifying Updates**: Allow Copilot to assist in refactoring code blocks that update egg definitions, ensuring clean and maintainable code logic.
5. **Exploring Harmony**: Use Copilot to explore adding Harmony methods for potential future modifications or integration where necessary.

For further improvements or if any feature seems complex, Copilot's contextual code suggestions can be invaluable for accelerating development and maintaining high code quality.

---

By following these instructions, developers can enhance functionality, maintain compatibility, and further develop the "Different Egg Sizes" mod for RimWorld.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

