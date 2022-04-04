class LoadingScene extends Phaser.Scene {

    constructor() {
        super('loading-scene');
        // this.playButton;
    }

    preload() {

        this.createLoadingAnimation();

        this.sound.pauseOnBlur = false;
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
        this.load.image('main-menu', 'res/images/main-menu.png');

        this.load.audio('music', './res/sounds/music.wav');
        this.load.audio('push', './res/sounds/push.wav');
        this.load.audio('swing', './res/sounds/swing.wav');
        this.load.audio('arrow-shot', './res/sounds/arrow-shot.wav');
        this.load.audio('trigger', './res/sounds/trigger.wav');
        this.load.audio('hit', './res/sounds/hit.wav');
        this.load.audio('final-hit', './res/sounds/final-hit.wav');
        this.load.audio('new-wave', './res/sounds/new-wave.wav');
        this.load.audio('hero-death', './res/sounds/hero-death.wav');
        this.load.audio('hero-hurt', './res/sounds/hero-hurt.wav');
        this.load.audio('hurt', './res/sounds/hurt.wav');
        this.load.audio('jump', './res/sounds/jump.wav');
        this.load.audio('hero-death', './res/sounds/hero-death.wav');
        this.load.audio('game-over', './res/sounds/game-over.wav');
        this.load.audio('explosion', './res/sounds/explosion.wav');
        this.load.audio('enemy-voice1', './res/sounds/enemy-voice1.wav');
        this.load.audio('enemy-voice2', './res/sounds/enemy-voice2.wav');
        this.load.audio('click', './res/sounds/click.wav');

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

        this.scene.start('main-menu');
    }

    createLoadingAnimation() {

        const bars = []
        const radius = 32
        const height = radius * 0.5
        const width = 3
        const cx = 5 * 16;
        const cy = 6 * 16;
        let angle = -90

        for (let i = 0; i < 12; ++i) {

            const { x, y } = Phaser.Math.RotateAround({ x: cx, y: cy - (radius - (height * 0.5)) }, cx, cy, Phaser.Math.DEG_TO_RAD * angle)
            const bar = this.add.rectangle(x, y, width, height, 0xffffff, 1)
                .setAngle(angle)
                .setAlpha(0.2)

            bars.push(bar)

            angle += 30
        }

        let index = 0
        const tweens = []
        this.time.addEvent({
            delay: 70,
            loop: true,
            callback: () => {
                if (index < tweens.length) {
                    const tween = tweens[index]
                    tween.restart()
                }
                else {
                    const bar = bars[index]
                    const tween = this.tweens.add({
                        targets: bar,
                        alpha: 0.2,
                        duration: 400,
                        onStart: () => {
                            bar.alpha = 1
                        }
                    })

                    tweens.push(tween)
                }
                ++index
                if (index >= bars.length) {
                    index = 0
                }
            }
        })
    }

    update() {
    }
}

export default LoadingScene;
