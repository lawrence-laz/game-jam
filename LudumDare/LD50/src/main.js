import Level from './scenes/level.js';
import MainMenu from './scenes/main-menu.js';
import LoadingScene from './scenes/loading-scene.js'
import GameOver from './scenes/game-over.js';

var config = {
    type: Phaser.AUTO,
    width: 160,
    height: 196,
    physics: {
        default: 'arcade',
        arcade: {
            gravity: { y: 200 },
            // debug: 'true'
        }
    },
    pixelArt: true,
    zoom: 4,
    scene: [LoadingScene, MainMenu, Level, GameOver]
};

var game = new Phaser.Game(config);
