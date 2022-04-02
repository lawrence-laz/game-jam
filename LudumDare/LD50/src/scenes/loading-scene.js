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
        this.load.image('imp-archer', 'res/images/imp-archer.png');
        this.load.image('arrow', 'res/images/arrow.png');
        this.load.image('box', 'res/images/box.png');
        this.load.image('target', 'res/images/target.png');
        this.load.image('ground', 'res/images/ground.png');
        this.load.image('grid', 'res/images/grid.png');
        this.load.image('woosh1', 'res/images/woosh1.png');
        this.load.image('woosh2', 'res/images/woosh2.png');
        this.load.image('left-gate', 'res/images/left-gate.png');
        this.load.image('right-gate', 'res/images/right-gate.png');
        this.load.image('fence', 'res/images/fence.png');
        this.load.image('spikes1', 'res/images/spikes1.png');
        this.load.image('spikes2', 'res/images/spikes2.png');
        this.load.image('spikes3', 'res/images/spikes3.png');
        this.load.image('spikes4', 'res/images/spikes4.png');
        this.load.image('bat1', 'res/images/bat1.png');
        this.load.image('bat2', 'res/images/bat2.png');
        this.load.image('blue-pill', 'res/images/blue-pill.png');
        this.load.image('bomb1', 'res/images/bomb1.png');
        this.load.image('bomb2', 'res/images/bomb2.png');
        this.load.image('explosion1', 'res/images/explosion1.png');
        this.load.image('explosion2', 'res/images/explosion2.png');
        this.load.image('chubby-imp', 'res/images/chubby-imp.png');
        this.load.image('left-gate-open', 'res/images/left-gate-open.png');
        this.load.image('right-gate-open', 'res/images/right-gate-open.png');
        this.load.audio('music', './res/sounds/music.wav');
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

        this.anims.create({
            key: 'bat-fly',
            frames: [
                { key: 'bat1' },
                { key: 'bat2' }
            ],
            frameRate: 8,
            repeat: -1,
        });

        this.anims.create({
            key: 'spikes-open',
            frames: [
                { key: 'spikes1' },
                { key: 'spikes2' },
                { key: 'spikes3' },
                { key: 'spikes4', duration: 1000 },
            ],
            frameRate: 8,
            repeat: 0,
        });

        this.anims.create({
            key: 'bomb-trigger',
            frames: [
                { key: 'bomb1' },
                { key: 'bomb2' },
            ],
            frameRate: 16,
            repeat: -1,
        });

        this.anims.create({
            key: 'explode',
            frames: [
                { key: 'explosion1' },
                { key: 'explosion2' }
            ],
            frameRate: 8,
            repeat: 0,
            hideOnComplete: true,
        });

        this.anims.create({
            key: 'spikes-close',
            frames: [
                { key: 'spikes4' },
                { key: 'spikes2' },
                { key: 'spikes1', duration: 1000 },
            ],
            frameRate: 8,
            repeat: 0,
        });

        // this.playButton = this.add.image(400, 300, 'play-button');
        // TODO: Change to main-menu before release
        this.scene.transition({ target: 'level', duration: 200 });
    }

    update() {
    }

}

export default LoadingScene;
