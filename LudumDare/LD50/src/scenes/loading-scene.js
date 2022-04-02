class LoadingScene extends Phaser.Scene {

    constructor() {
        super('loading-scene');
        // this.playButton;
    }

    preload() {
        // this.sound.pauseOnBlur = false;
        this.load.image('play-button', 'res/images/play.png');
        this.load.image('empty', 'res/images/empty.png');
        this.load.image('hero', 'res/images/hero.png');
        this.load.image('imp', 'res/images/imp.png');
        this.load.image('box', 'res/images/box.png');
        this.load.image('target', 'res/images/target.png');
        this.load.image('ground', 'res/images/ground.png');
        this.load.image('grid', 'res/images/grid.png');
        this.load.image('woosh1', 'res/images/woosh1.png');
        this.load.image('woosh2', 'res/images/woosh2.png');
        this.load.image('left-gate', 'res/images/left-gate.png');
        this.load.image('right-gate', 'res/images/right-gate.png');
        this.load.image('fence', 'res/images/fence.png');
        this.load.image('spikes', 'res/images/spikes.png');
        this.load.image('left-gate-open', 'res/images/left-gate-open.png');
        this.load.image('right-gate-open', 'res/images/right-gate-open.png');
        this.load.audio('music', './res/sounds/music.wav');
        this.load.image('sky', 'https://labs.phaser.io/assets/skies/space3.png');
        this.load.image('logo', 'https://labs.phaser.io/assets/sprites/phaser3-logo.png');
        this.load.image('red', 'https://labs.phaser.io/assets/particles/red.png');
    }

    create() {
        this.anims.create({
            key: 'woosh',
            frames: [
                { key: 'woosh1' },
                { key: 'woosh2' }
            ],
            frameRate: 8,
            repeat: 0,
            hideOnComplete: true,
        });

        // this.playButton = this.add.image(400, 300, 'play-button');
        // TODO: Change to main-menu before release
        this.scene.transition({ target: 'level', duration: 200 });
    }

    update() {
    }

}

export default LoadingScene;
