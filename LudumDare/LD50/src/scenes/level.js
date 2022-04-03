import createSpawner from "../objects/spawner.js";
import Hero from '../objects/hero.js';
import Box from "../objects/box.js";
import Grid from "../objects/grid.js";
import Spawner from "../objects/spawner.js";
import firstOrDefault from "../utils/first-or-default.js";
import Spikes from "../objects/spikes.js";
import Arrow from "../objects/arrow.js";
import FlyingImp from "../objects/flying-imp.js";

var hero = null;

class Level extends Phaser.Scene {

    constructor() {
        super('level');

        this.hero = null;
    }

    preload() {

    }

    create() {

        let music = this.sound.add('music');
        // music.play();

        this.objects = this.add.group();

        this.grid = new Grid(this);
        this.grid.setDepth(-10);
        this.spawner = new Spawner(this.grid, this, 0, 0);
        this.score = 0;

        this.hero = new Hero(this.grid, this);
        hero = this.hero;

        this.objects.add(this.hero);
        this.physics.add.collider(
            this.objects,
            this.objects,
            function (a, b) {
                let hero = firstOrDefault([a, b], (x) => x instanceof Hero);
                let pushable = firstOrDefault(
                    [a, b],
                    (x) => {
                        return !(x instanceof Hero) && x.name != 'ground'
                    }
                );
                let spikes = firstOrDefault([a, b], (x) => x instanceof Spikes);
                let arrow = firstOrDefault([a, b], (x) => x instanceof Arrow);
                let flyingImp = firstOrDefault([a, b], (x) => x instanceof FlyingImp);

                if (arrow && flyingImp) {
                    return;
                }

                if (arrow) {
                    let theOther = a == arrow ? b : a;
                    arrow.stickTo(theOther);
                }

                if (hero && spikes) {
                    let spikesAlignedPosition = this.grid.getAlignedPosition(spikes.x, spikes.y);
                    let heroAlignedPosition = this.grid.getAlignedPosition(hero.x, hero.y);
                    if (heroAlignedPosition.x == spikesAlignedPosition.x
                        && heroAlignedPosition.y < spikesAlignedPosition.y
                        && spikes.open) {
                        // You got impaled son.
                        hero.convertToCorpse();
                        this.gameOver("Trapped by spikes", hero);
                        return;
                    }
                }

                if (hero && pushable) {

                    let verticalDistance = pushable.y - hero.y;
                    let isHeroWithinVerticalThreshold = verticalDistance < 9 && verticalDistance > -9;
                    let doesHeroesMovementAlignWithPush =
                        Math.sign(hero.body.velocity.x) == Math.sign(pushable.x - hero.x)
                        || Math.sign(hero.body.newVelocity.x) == Math.sign(pushable.x - hero.x);
                    if (isHeroWithinVerticalThreshold && doesHeroesMovementAlignWithPush) {
                        // Try to push.
                        let horizontalDirection = Math.sign(pushable.x - hero.x);
                        this.grid.tryMoveOffset(pushable, horizontalDirection);
                    } else {
                        let heroCell = this.grid.getCellForPosition(hero.x, hero.y);
                        let pushableCell = this.grid.getCellForPosition(pushable.x, pushable.y);
                        if (heroCell == pushableCell) {
                            // Get squished.
                            // ???
                        }
                    }
                }
            },
            null,
            this
        );

        this.input.on('pointerdown', function (pointer) {
            // var text = helloWorld(this, pointer.x, pointer.y);
        }, this);
    }

    gameOver(reason, focusObject) {

        this.hero.doGameOver();

        let camera = this.cameras.main;

        camera.setBounds(0, 0, 10 * 16, 12 * 16);
        camera.useBounds = true;

        this.cameras.main.startFollow(focusObject, true, 0.09, 0.09);

        this.tweens.add({
            targets: this.cameras.main,
            zoom: 2,
            duration: 100,
            ease: 'Sine.easeInOut',
        });

        this.time.delayedCall(500, () => {
            this.scene.start('game-over', { reason, score: this.spawner.currentWaveIndex });
        });

    }
}

export default Level;
