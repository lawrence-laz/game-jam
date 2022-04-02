# Ludum Dare 50

## Aseprite pipeline
```bash
fswatch spritesheet.ase | xargs -I{} bash -c 'debounce aseprite -b spritesheet.ase --ignore-layer Background --save-as {slice}.png'
```
