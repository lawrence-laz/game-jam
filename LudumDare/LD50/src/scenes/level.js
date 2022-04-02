import helloWorld from "../hello-world.js";
import createSpawner from "../objects/spawner.js";
import Hero from '../objects/hero.js';
import Box from "../objects/box.js";
import Grid from "../objects/grid.js";
import Spawner from "../objects/spawner.js";
import firstOrDefault from "../utils/first-or-default.js";

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

        // this.add.image(400, 300, 'sky');

        // var particles = this.add.particles('red');

        // var emitter = particles.createEmitter({
        //     speed: 100,
        //     scale: { start: 1, end: 0 },
        //     blendMode: 'ADD'
        // });

        // var logo = this.physics.add.image(400, 100, 'hero');
        // logo.setVelocity(100, 200);
        // logo.setBounce(1, 1);
        // logo.setCollideWorldBounds(true);

        this.objects = this.add.group();

        this.grid = new Grid(this);
        this.spawner = new Spawner(this.grid, this, 0, 0);

        this.hero = new Hero(this.grid, this);

        let box = new Box(this, 100, 0);

        this.grid.setCellTo(box, 0, 0);

        // let ground = this.physics.add.sprite(0, 16 * 11, 'ground');
        // ground.setName('ground');
        // ground.setCollideWorldBounds(true);
        // this.objects.add(ground);

        this.objects.add(this.hero);
        this.objects.add(box);
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
                            // TODO:
                        }
                    }
                }
            },
            null,
            this
        );

        // this.setUpInputHandlers();

        // emitter.startFollow(logo);

        // var text = this.add.text(190, 136, 'Hello, world!', {
        //     fontFamily: 'font1',
        // });
        // var text = helloWorld(this);

        this.input.on('pointerdown', function (pointer) {
            console.log('down');

            // var text = helloWorld(this, pointer.x, pointer.y);
        }, this);
    }

    // setUpInputHandlers() {

    //     this.input.keyboard.addCapture('UP, DOWN, LEFT, RIGHT')

    //     this.input.keyboard.on('keydown-UP', function (event) {

    //         this.playButton.y -= 4;

    //     }, this);

    //     this.input.keyboard.on('keydown-DOWN', function (event) {

    //         this.playButton.y += 4;

    //     }, this);

    //     this.input.keyboard.on('keydown-LEFT', function (event) {

    //         console.log("Moving left");
    //         this.hero.run(-1);

    //     }, this);

    //     this.input.keyboard.on('keydown-RIGHT', function (event) {

    //         console.log("Moving right");
    //         this.hero.run(1);

    //     }, this);
    // }
}

export default Level;
