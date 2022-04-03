class LoadingScene extends Phaser.Scene {

    constructor() {
        super('loading-scene');
        // this.playButton;
    }

    preload() {
        // this.sound.pauseOnBlur = false;
        this.load.image('play-button', 'res/images/play.png');
        this.load.image('dust1', 'res/images/dust1.png');
        this.load.image('empty', 'res/images/empty.png');
        this.load.image('hero-corpse', 'res/images/hero-corpse.png');
        this.load.image('hero1', 'res/images/hero1.png');
        this.load.image('hero2', 'res/images/hero2.png');
        this.load.image('hero-swing1', 'res/images/hero-swing1.png');
        this.load.image('hero-swing2', 'res/images/hero-swing2.png');
        this.load.image('imp-corpse', 'res/images/imp-corpse.png');
        this.load.image('imp1', 'res/images/imp1.png');
        this.load.image('imp2', 'res/images/imp2.png');
        this.load.image('flying-imp-corpse', 'res/images/flying-imp-corpse.png');
        this.load.image('flying-imp1', 'res/images/flying-imp1.png');
        this.load.image('flying-imp2', 'res/images/flying-imp2.png');
        this.load.image('imp-archer', 'res/images/imp-archer.png');
        this.load.image('arrow', 'res/images/arrow.png');
        this.load.image('box', 'res/images/box.png');
        this.load.image('target', 'res/images/target.png');
        this.load.image('ground', 'res/images/ground.png');
        this.load.image('grid', 'res/images/grid.png');
        this.load.image('woosh1', 'res/images/woosh1.png');
        this.load.image('woosh2', 'res/images/woosh2.png');
        this.load.image('left-gate', 'res/images/left-gate.png');
        this.load.image('left-gate-damaged', 'res/images/left-gate-damaged.png');
        this.load.image('right-gate', 'res/images/right-gate.png');
        this.load.image('right-gate-damaged', 'res/images/right-gate-damaged.png');
        this.load.image('fence', 'res/images/fence.png');
        this.load.image('fence-damaged', 'res/images/fence-damaged.png');
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
        this.load.image('chubby-imp-corpse', 'res/images/chubby-imp-corpse.png');
        this.load.image('chubby-imp1', 'res/images/chubby-imp1.png');
        this.load.image('chubby-imp2', 'res/images/chubby-imp2.png');
        this.load.image('chubby-imp-inverted', 'res/images/chubby-imp-inverted.png');
        this.load.image('left-gate-open', 'res/images/left-gate-open.png');
        this.load.image('right-gate-open', 'res/images/right-gate-open.png');
        this.load.audio('music', './res/sounds/music.wav');
        this.load.bitmapFont('font', 'res/fonts/pixel.png', '/res/fonts/pixel.xml');
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
                { key: 'spikes2', duration: 140 },
                { key: 'spikes1' },
                { key: 'spikes3' },
                { key: 'spikes4', duration: 1000 },
            ],
            frameRate: 8,
            repeat: 0,
        });

        this.anims.create({
            key: 'imp-idle',
            frames: [
                { key: 'imp1' },
                { key: 'imp2' },
            ],
            frameRate: 4,
            repeat: -1,
        });

        this.anims.create({
            key: 'chubby-imp-idle',
            frames: [
                { key: 'chubby-imp1' },
                { key: 'chubby-imp2' },
            ],
            frameRate: 4,
            repeat: -1,
        });

        this.anims.create({
            key: 'flying-imp-idle',
            frames: [
                { key: 'flying-imp1' },
                { key: 'flying-imp2' },
            ],
            frameRate: 4,
            repeat: -1,
        });

        this.anims.create({
            key: 'hero-idle',
            frames: [
                { key: 'hero1' },
                { key: 'hero2' },
            ],
            frameRate: 4,
            repeat: -1,
        });

        this.anims.create({
            key: 'hero-swing',
            frames: [
                { key: 'hero1', duration: 3 },
                { key: 'hero-swing2' },
                // { key: 'hero-swing1' },
                { key: 'hero2' },
            ],
            frameRate: 16,
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
            key: 'chubby-imp-trigger',
            frames: [
                { key: 'chubby-imp2' },
                { key: 'chubby-imp-inverted' },
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
        this.scene.start('level');
    }

    update() {
    }

}

export default LoadingScene;
